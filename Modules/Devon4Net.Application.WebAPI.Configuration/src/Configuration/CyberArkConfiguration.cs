using System;
using System.Collections.Generic;
using System.Text;
using Devon4Net.Infrastructure.CircuitBreaker.Handler;
using Devon4Net.Infrastructure.Common.Options.CyberArk;
using Devon4Net.Infrastructure.CyberArk.Handler;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Application.WebAPI.Configuration
{
    public static class CyberArkConfiguration
    {
        private static ICircuitBreakerHttpClient CircuitBreakerHttpClient { get; set; }
        private static CyberArkOptions AnsibleTowerOptions { get; set; }

        public static void SetupCyberArk(this IServiceCollection services, CyberArkOptions cyberArkOptions)
        {
            services.AddSingleton(typeof(ICyberArkHandler), typeof(CyberArkHandler));
            //var ansibleTowerInstance = GetAnsibleTowerInstances();

            //if (ansibleTowerInstance == null) return;

            //services.AddSingleton(typeof(IAnsibleTowerInstance), ansibleTowerInstance);
            //services.AddTransient(typeof(IAnsibleTowerHandler), typeof(AnsibleTowerHandler));
        }
    }
}
