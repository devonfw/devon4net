using Devon4Net.Infrastructure.AnsibleTower.Common;
using Devon4Net.Infrastructure.AnsibleTower.Dto;
using Devon4Net.Infrastructure.AnsibleTower.Handler;
using Devon4Net.Infrastructure.CircuitBreaker.Common.Enums;
using Devon4Net.Infrastructure.CircuitBreaker.Handlers;
using Devon4Net.Infrastructure.Common.Handlers;
using Devon4Net.Infrastructure.Common.Options.AnsibleTower;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Application.WebAPI.Configuration
{
    public static class AnsibleTowerConfiguration
    {
        private static IHttpClientHandler HttpClientHandler { get; set; }
        private static AnsibleTowerOptions AnsibleTowerOptions { get; set; }

        public static void SetupAnsibleTower(this IServiceCollection services, IConfiguration configuration)
        {
            AnsibleTowerOptions = services.GetTypedOptions<AnsibleTowerOptions>(configuration, "AnsibleTower");

            if (AnsibleTowerOptions == null || string.IsNullOrEmpty(AnsibleTowerOptions.ApiUrlBase)) return;

            using var serviceProvider = services.BuildServiceProvider();
            HttpClientHandler = serviceProvider.GetService<IHttpClientHandler>();

            var ansibleTowerInstance = GetAnsibleTowerInstances();

            if (ansibleTowerInstance == null ) return;

            services.AddSingleton(typeof(IAnsibleTowerInstance), ansibleTowerInstance);
            services.AddTransient(typeof(IAnsibleTowerHandler), typeof(AnsibleTowerHandler));
        }

        private static IAnsibleTowerInstance GetAnsibleTowerInstances()
        {
            if (AnsibleTowerOptions == null || AnsibleTowerOptions?.EnableAnsible == false || string.IsNullOrEmpty(AnsibleTowerOptions.ApiUrlBase)) return null;
            var apiRequestDto = HttpClientHandler.Send<ApiRequestDto>(HttpMethod.Get, AnsibleTowerOptions.CircuitBreakerName, AnsibleTowerOptions.ApiUrlBase,null, MediaType.ApplicationJson).Result;
            return new AnsibleTowerInstance(AnsibleTowerOptions.Name, AnsibleTowerOptions.CircuitBreakerName, AnsibleTowerOptions.ApiUrlBase, AnsibleTowerOptions.Version, apiRequestDto, AnsibleTowerOptions.Username, AnsibleTowerOptions.Password);
        }
    }
}
