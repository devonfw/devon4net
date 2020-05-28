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

            var ansibleTowerInstances = GetAnsibleTowerInstances();

            if (ansibleTowerInstances==null || !ansibleTowerInstances.Any()) return;

            services.AddSingleton(typeof(IList<IAnsibleTowerInstance>), ansibleTowerInstances);
            services.AddTransient(typeof(IAnsibleTowerHandler), typeof(AnsibleTowerHandler));
        }

        private static List<IAnsibleTowerInstance> GetAnsibleTowerInstances()
        {
            if (AnsibleTowerOptions?.Instances == null || !AnsibleTowerOptions.Instances.Any()) return new List<IAnsibleTowerInstance>();

            var ansibleInstanceHandlerList = new List<IAnsibleTowerInstance>();

            foreach (var instance in AnsibleTowerOptions.Instances)
            {
                var handler = new AnsibleTowerInstance();

                var apiRequestDto = CircuitBreakerHttpClient.Get<ApiRequestDto>(instance.CircuitBreakerName, instance.ApiUrlBase).Result;

                handler.Setup(apiRequestDto);
                ansibleInstanceHandlerList.Add(handler);
            }

            return ansibleInstanceHandlerList;
        }
    }
}
