using Devon4Net.Infrastructure.Common.Options;
using Devon4Net.Infrastructure.Common.Options.CyberArk;
using Devon4Net.Infrastructure.CyberArk.Handler;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Application.WebAPI.Configuration
{
    public static class CyberArkConfiguration
    {
        public static void SetupCyberArk(this IServiceCollection services, ref IConfiguration configuration)
        {
            var cyberArkOptions = services.GetTypedOptions<CyberArkOptions>(configuration, "CyberArk");

            if (cyberArkOptions == null || cyberArkOptions.EnableCyberArk == false || string.IsNullOrEmpty(cyberArkOptions.CircuitBreakerName) || string.IsNullOrEmpty(cyberArkOptions.UserName) || string.IsNullOrEmpty(cyberArkOptions.Password)) return;

            services.AddSingleton(typeof(ICyberArkHandler), typeof(CyberArkHandler));
        }
    }
}
