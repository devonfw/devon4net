using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Devon4Net.Infrastructure.AnsibleTower.Dto;
using Devon4Net.Infrastructure.CircuitBreaker.Handler;

namespace Devon4Net.Infrastructure.AnsibleTower.Common
{
    /// <summary>
    /// Ansible handler manager
    /// </summary>
    public class AnsibleTowerInstance : IAnsibleTowerInstance
    {
        private ICircuitBreakerHttpClient CircuitBreakerHttpClient { get; set; }
        private ApiRequestDto ApiDefinition { get; set; }
        public string Name { get; }
        public string CircuitBreakerName { get; }
        public string ApiUrlBase { get; }
        public string Version { get; }

        public AnsibleTowerInstance(string name, string circuitBreakerName, string apiUrlBase, string version, ApiRequestDto apiRequestDto)
        {
            ApiDefinition = apiRequestDto;
            Name = name;
            CircuitBreakerName = circuitBreakerName;
            ApiUrlBase = apiUrlBase;
            Version = version;
        }


    }
}
