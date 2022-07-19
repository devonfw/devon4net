using Devon4Net.Infrastructure.Common.Handlers;
using Devon4Net.Infrastructure.CyberArk.Handler;
using Devon4Net.Infrastructure.CyberArk.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Application.WebAPI.Configuration
{
    public static class CyberArkConfiguration
    {
        public static void SetupCyberArk(this IServiceCollection services, IConfiguration configuration)
        {
            var cyberArkOptions = services.GetTypedOptions<CyberArkOptions>(configuration, "CyberArk");

            if (cyberArkOptions?.EnableCyberArk != true || string.IsNullOrEmpty(cyberArkOptions.CircuitBreakerName) || string.IsNullOrEmpty(cyberArkOptions.UserName) || string.IsNullOrEmpty(cyberArkOptions.Password)) return;

            services.AddSingleton(typeof(ICyberArkHandler), typeof(CyberArkHandler));
        }
    }
}
