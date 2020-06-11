using Devon4Net.Infrastructure.CyberArk.Handler;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Application.WebAPI.Configuration
{
    public static class CyberArkConfiguration
    {
        public static void SetupCyberArk(this IServiceCollection services)
        {
            services.AddSingleton(typeof(ICyberArkHandler), typeof(CyberArkHandler));
        }
    }
}
