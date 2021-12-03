using Devon4Net.Infrastructure.Common.Handlers;
using Devon4Net.Infrastructure.Common.Options.SmaxHcm;
using Devon4Net.Infrastructure.SMAXHCM.Handler;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Application.WebAPI.Configuration
{
    public static class SmaxHcmConfiguration
    {
        public static void SetupSmaxHcm(this IServiceCollection services, IConfiguration configuration)
        {
            var smaxHcmOptions = services.GetTypedOptions<SmaxHcmOptions>(configuration, "SmaxHcm");

            if (smaxHcmOptions == null || smaxHcmOptions.EnableSmax == false || string.IsNullOrEmpty(smaxHcmOptions.CircuitBreakerName) || string.IsNullOrEmpty(smaxHcmOptions.UserName) || string.IsNullOrEmpty(smaxHcmOptions.Password)) return;
            
            services.AddSingleton(typeof(ISmaxHcmHandler), typeof(SmaxHcmHandler));
        }
    }
}
