using System.Collections.Generic;
using System.Linq;
using Devon4Net.Infrastructure.AnsibleTower.Common;
using Devon4Net.Infrastructure.AnsibleTower.Dto;
using Devon4Net.Infrastructure.AnsibleTower.Handler;
using Devon4Net.Infrastructure.CircuitBreaker.Handler;
using Devon4Net.Infrastructure.Common.Options.AnsibleTower;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Application.WebAPI.Configuration
{
    public static class AnsibleTowerConfiguration
    {
        private static ICircuitBreakerHttpClient CircuitBreakerHttpClient { get; set; }
        private static AnsibleTowerOptions AnsibleTowerOptions { get; set; }

        public static void SetupAnsibleTower(this IServiceCollection services, AnsibleTowerOptions ansibleTowerOptions)
        {
            var serviceProvider = services.BuildServiceProvider();
            CircuitBreakerHttpClient = serviceProvider.GetService<ICircuitBreakerHttpClient>();
            AnsibleTowerOptions = ansibleTowerOptions;

            var ansibleTowerInstance = GetAnsibleTowerInstances();

            if (ansibleTowerInstance == null ) return;

            services.AddSingleton(typeof(IAnsibleTowerInstance), ansibleTowerInstance);
            services.AddTransient(typeof(IAnsibleTowerHandler), typeof(AnsibleTowerHandler));
        }

        private static IAnsibleTowerInstance GetAnsibleTowerInstances()
        {
            if (AnsibleTowerOptions == null || !AnsibleTowerOptions.EnableAnsible || string.IsNullOrEmpty(AnsibleTowerOptions.ApiUrlBase)) return null;
            var apiRequestDto = CircuitBreakerHttpClient.Get<ApiRequestDto>(AnsibleTowerOptions.CircuitBreakerName, AnsibleTowerOptions.ApiUrlBase).Result;
            return new AnsibleTowerInstance(AnsibleTowerOptions.Name, AnsibleTowerOptions.CircuitBreakerName, AnsibleTowerOptions.ApiUrlBase, AnsibleTowerOptions.Version, apiRequestDto, AnsibleTowerOptions.Username, AnsibleTowerOptions.Password);
        }
    }
}
