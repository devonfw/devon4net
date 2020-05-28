using System;
using System.Collections.Generic;
using System.Text;
using Devon4Net.Infrastructure.AnsibleTower.Common;
using Devon4Net.Infrastructure.CircuitBreaker.Handler;

namespace Devon4Net.Infrastructure.AnsibleTower.Handler
{
    public class AnsibleTowerHandler : IAnsibleTowerHandler
    {
        private ICircuitBreakerHttpClient CircuitBreakerHttpClient { get; set; }
        private IList<IAnsibleTowerInstance> AnsibleTowerInstances { get; set; }

        public AnsibleTowerHandler(IList<IAnsibleTowerInstance> ansibleTowerInstances, ICircuitBreakerHttpClient circuitBreakerHttpClient)
        {
            CircuitBreakerHttpClient = circuitBreakerHttpClient;
            AnsibleTowerInstances = AnsibleTowerInstances;
        }
    }
}
