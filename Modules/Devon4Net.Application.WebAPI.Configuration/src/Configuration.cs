using Devon4Net.Infrastructure.Middleware.Headers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Devon4Net.Application.WebAPI.Configuration.Application;
using Devon4Net.Application.WebAPI.Configuration.Common;
using Devon4Net.Domain.UnitOfWork;
using Microsoft.AspNetCore.Builder;

namespace Devon4Net.Application.WebAPI.Configuration
{
    public static class Configuration
    {
        public static void ConfigureDevonFw(this IServiceCollection services, IConfiguration configuration)
        {
            services.SetupDevonfw(ref configuration);
            services.SetupUnitOfWork();
            SetupSwagger(ref services, ref configuration);
            services.SetupCors(ref configuration);
            services.SetupCircuitBreaker(ref configuration);
            services.SetupHeaders(ref configuration);
            services.SetupLog(ref configuration);
            services.SetupJwtConf(ref configuration);
            services.SetupLiteDb(ref configuration);
            services.SetupRabbitMq(ref configuration);
            services.SetupMediatR(ref configuration);
            services.SetupAnsibleTower(ref configuration);
            services.SetupCyberArk(ref configuration);
            services.SetupSmaxHcm(ref configuration);
        }

        public static void ConfigureDevonFw(this IApplicationBuilder app)
        {
            app.InitializeDevonFw();
        }

        private static void SetupSwagger(ref IServiceCollection services, ref IConfiguration configuration)
        {
            bool.TryParse(configuration[$"{DevonFwConst.DevonFwAppSettingsNodeName}:UseSwagger"], out var useSwagger);
            if (useSwagger) services.SetupSwagger(ref configuration);
        }
    }
}