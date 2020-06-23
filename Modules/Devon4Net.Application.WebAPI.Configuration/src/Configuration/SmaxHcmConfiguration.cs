using Devon4Net.Infrastructure.SMAXHCM.Handler;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Application.WebAPI.Configuration
{
    public static class SmaxHcmConfiguration
    {
        public static void SetupSmaxHcm(this IServiceCollection services)
        {
            services.AddSingleton(typeof(ISmaxHcmHandler), typeof(SmaxHcmHandler));
        }
    }
}
