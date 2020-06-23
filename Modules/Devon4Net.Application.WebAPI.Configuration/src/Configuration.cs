using System.Linq;
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
using Devon4Net.Infrastructure.Common.Options.AnsibleTower;
using Devon4Net.Infrastructure.Common.Options.CyberArk;
using Devon4Net.Infrastructure.Common.Options.LiteDb;
using Devon4Net.Infrastructure.Common.Options.MediatR;
using Devon4Net.Infrastructure.Common.Options.RabbitMq;
using Devon4Net.Infrastructure.Common.Options.SmaxHcm;
using Devon4Net.Infrastructure.Extensions.Helpers;

namespace Devon4Net.Application.WebAPI.Configuration
{
    public static class Configuration
    {
        private static ServiceProvider ServiceProvider { get; set; }
        private static DevonfwOptions DevonfwOptions { get; set; }
        private static SwaggerOptions SwaggerOptions { get; set; }
        private static JwtOptions JwtOptions { get; set; }
        private static CorsOptions CorsOptions { get; set; }
        private static CircuitBreakerOptions CircuitBreakerOptions { get; set; }
        private static LogOptions LogOptions { get; set; }
        private static RabbitMqOptions RabbitMqOptions { get; set; }
        private static MediatROptions MediatROptions { get; set; }
        private static LiteDbOptions LiteDbOptions { get; set; }
        private static AnsibleTowerOptions AnsibleTowerOptions { get; set; }
        private static CyberArkOptions CyberArkOptions { get; set; }
        private static SmaxHcmOptions SmaxHcmOptions { get; set; }


        public static void ConfigureDevonFw(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureIIS(configuration);
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddTransient(typeof(IObjectTypeHelper), typeof(ObjectTypeHelper));
            services.AddTransient(typeof(IJsonHelper), typeof(JsonHelper));

            services.Configure<DevonfwOptions>(configuration.GetSection("devonfw"));
            services.Configure<SwaggerOptions>(configuration.GetSection("Swagger"));
            services.Configure<JwtOptions>(configuration.GetSection("JWT"));
            services.Configure<CorsOptions>(configuration.GetSection("Cors"));
            services.Configure<CircuitBreakerOptions>(configuration.GetSection("CircuitBreaker"));
            services.SetupKillSwitch(ref configuration);
            services.Configure<KillSwitchOptions>(configuration.GetSection("KillSwitchConfiguration"));
            services.Configure<LogOptions>(configuration.GetSection("Log"));
            services.Configure<RabbitMqOptions>(configuration.GetSection("RabbitMq"));
            services.Configure<MediatROptions>(configuration.GetSection("MediatR"));
            services.Configure<LiteDbOptions>(configuration.GetSection("LiteDb"));
            services.Configure<AnsibleTowerOptions>(configuration.GetSection("AnsibleTower"));
            services.Configure<CyberArkOptions>(configuration.GetSection("CyberArk"));
            services.Configure<SmaxHcmOptions>(configuration.GetSection("SmaxHcm"));

            ServiceProvider = services.BuildServiceProvider();
            DevonfwOptions = ServiceProvider.GetService<IOptions<DevonfwOptions>>()?.Value;
            
            SetupSwagger(ref services);
            SetupCors(ref services);
            SetupCircuitBreaker(ref services);
            configuration.SetupHeaders();
            SetupLog(ref services);
            SetupJwt(ref services);
            SetupLiteDb(ref services);
            SetupRabbitMq(ref services);
            SetupMediatR(ref services);
            SetupAnsibleTower(ref services);
            SetupCyberArk(ref services);
            SetupSmaxHcm(ref services);
        }

        public static void ConfigureDevonFw(this IApplicationBuilder app)
        {
            app.UseRequestLocalization();
            app.SetupDevonfwMiddleware();
            if (DevonfwOptions.UseSwagger && SwaggerOptions?.Endpoint != null) app.ConfigureSwaggerApplication(SwaggerOptions);
        }

        private static void SetupSwagger(ref IServiceCollection services)
        {
            if (DevonfwOptions != null && !DevonfwOptions.UseSwagger) return;
            SwaggerOptions = ServiceProvider.GetService<IOptions<SwaggerOptions>>()?.Value;
            if (SwaggerOptions?.Endpoint == null) return;
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

        private static void SetupRabbitMq(ref IServiceCollection services)
        {
            RabbitMqOptions = ServiceProvider.GetService<IOptions<RabbitMqOptions>>()?.Value;
            if (RabbitMqOptions == null || !RabbitMqOptions.EnableRabbitMq || RabbitMqOptions?.Hosts == null || !RabbitMqOptions.Hosts.Any()) return;
            services.SetupRabbitMq(RabbitMqOptions);
        }

        private static void SetupLiteDb(ref IServiceCollection services)
        {
            LiteDbOptions = ServiceProvider.GetService<IOptions<LiteDbOptions>>()?.Value;
            if (LiteDbOptions == null ||  string.IsNullOrEmpty(LiteDbOptions?.DatabaseLocation)) return;
            services.SetupLiteDb();
        }

        private static void SetupMediatR(ref IServiceCollection services)
        {
            MediatROptions = ServiceProvider.GetService<IOptions<MediatROptions>>()?.Value;
            if (MediatROptions == null || !MediatROptions.EnableMediatR) return;
            services.SetupMediatR(MediatROptions);
        }

        private static void SetupAnsibleTower(ref IServiceCollection services)
        {
            AnsibleTowerOptions = ServiceProvider.GetService<IOptions<AnsibleTowerOptions>>()?.Value;
            if (AnsibleTowerOptions == null || string.IsNullOrEmpty(AnsibleTowerOptions.ApiUrlBase)) return;
            services.SetupAnsibleTower(AnsibleTowerOptions);
        }

        private static void SetupCyberArk(ref IServiceCollection services)
        {
            CyberArkOptions = ServiceProvider.GetService<IOptions<CyberArkOptions>>()?.Value;
            if (CyberArkOptions == null || string.IsNullOrEmpty(CyberArkOptions.CircuitBreakerName) || string.IsNullOrEmpty(CyberArkOptions.UserName) || string.IsNullOrEmpty(CyberArkOptions.Password)) return;
            services.SetupCyberArk();
        }

        private static void SetupSmaxHcm(ref IServiceCollection services)
        {
            SmaxHcmOptions = ServiceProvider.GetService<IOptions<SmaxHcmOptions>>()?.Value;
            if (SmaxHcmOptions == null || string.IsNullOrEmpty(SmaxHcmOptions.CircuitBreakerName) || string.IsNullOrEmpty(SmaxHcmOptions.UserName) || string.IsNullOrEmpty(SmaxHcmOptions.Password)) return;
            services.SetupSmaxHcm();
        }
    }
}