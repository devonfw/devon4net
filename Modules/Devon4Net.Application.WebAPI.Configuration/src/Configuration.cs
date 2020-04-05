using Devon4Net.Domain.UnitOfWork.Repository;
using Devon4Net.Domain.UnitOfWork.UnitOfWork;
using Devon4Net.Infrastructure.Common.Options.CircuitBreaker;
using Devon4Net.Infrastructure.Common.Options.Cors;
using Devon4Net.Infrastructure.Common.Options.Devon;
using Devon4Net.Infrastructure.Common.Options.JWT;
using Devon4Net.Infrastructure.Common.Options.KillSwitch;
using Devon4Net.Infrastructure.Common.Options.Swagger;
using Devon4Net.Infrastructure.Middleware.Headers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Devon4Net.Infrastructure.Common.Options.Log;
using Devon4Net.Application.WebAPI.Configuration.Application;

namespace Devon4Net.Application.WebAPI.Configuration
{
    public static class Configuration
    {
        private static ServiceProvider ServiceProvider { get; set; }
        private static  DevonfwOptions DevonfwOptions { get; set; }
        private static  SwaggerOptions SwaggerOptions { get; set; }
        private static  JwtOptions JwtOptions { get; set; }
        private static  CorsOptions CorsOptions { get; set; }
        private static  CircuitBreakerOptions CircuitBreakerOptions { get; set; }
        private static LogOptions LogOptions { get; set; }

        public static void ConfigureDevonFw(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureIIS(configuration);
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

            services.Configure<DevonfwOptions>(configuration.GetSection("devonfw"));
            services.Configure<SwaggerOptions>(configuration.GetSection("Swagger"));
            services.Configure<JwtOptions>(configuration.GetSection("JWT"));
            services.Configure<CorsOptions>(configuration.GetSection("Cors"));
            services.Configure<CircuitBreakerOptions>(configuration.GetSection("CircuitBreaker"));
            services.SetupKillSwitch(ref configuration);
            services.Configure<KillSwitchOptions>(configuration.GetSection("KillSwitchConfiguration"));
            services.Configure<LogOptions>(configuration.GetSection("Log"));

            ServiceProvider = services.BuildServiceProvider();
            DevonfwOptions = ServiceProvider.GetService<IOptions<DevonfwOptions>>()?.Value;
            
            SetupSwagger(ref services);
            SetupCors(ref services);
            SetupCircuitBreaker(ref services);
            configuration.SetupHeaders();
            SetupLog(ref services);
            SetupJwt(ref services);
        }

        public static void ConfigureDevonFw(this IApplicationBuilder app)
        {
            app.UseRequestLocalization();
            app.SetupDevonfwMiddleware();
            if (DevonfwOptions.UseSwagger && SwaggerOptions!=null && SwaggerOptions.Endpoint!=null) app.ConfigureSwaggerApplication(SwaggerOptions);
        }

        private static void SetupSwagger(ref IServiceCollection services)
        {
            if (DevonfwOptions != null && !DevonfwOptions.UseSwagger) return;
            SwaggerOptions = ServiceProvider.GetService<IOptions<SwaggerOptions>>()?.Value;
            if (SwaggerOptions == null || SwaggerOptions.Endpoint == null) return;
            services.SetupSwaggerService(SwaggerOptions);
        }
        
        private static void SetupJwt(ref IServiceCollection services)
        {
            JwtOptions = ServiceProvider.GetService<IOptions<JwtOptions>>()?.Value;
            if (JwtOptions == null) return;
            services.SetupJwt(JwtOptions);
        }
        
        private static void SetupCors(ref IServiceCollection services)
        {
            CorsOptions = ServiceProvider.GetService<IOptions<CorsOptions>>()?.Value;
            if (CorsOptions?.Origins == null)
            {
                services.SetCorsAnyOriginAllowed();
            }
            else
            {
                services.SetupCorsOrigins(CorsOptions);
            }
        }

        private static void SetupCircuitBreaker(ref IServiceCollection services)
        {
            CircuitBreakerOptions = ServiceProvider.GetService<IOptions<CircuitBreakerOptions>>()?.Value;
            if (CircuitBreakerOptions == null) return;
            services.SetupCircuitBreaker(CircuitBreakerOptions);
        }

        private static void SetupLog(ref IServiceCollection services)
        {
            LogOptions = ServiceProvider.GetService<IOptions<LogOptions>>()?.Value;
            if (LogOptions == null) return;
            services.SetupLog(LogOptions, ServiceProvider);
        }
    }
}