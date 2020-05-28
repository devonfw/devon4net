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

        public AnsibleTowerInstance()
        {
            
        }

        public void Setup(ApiRequestDto apiRequestDto)
        {
            ApiDefinition = apiRequestDto;
        }
    }
}
