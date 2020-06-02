using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devon4Net.Infrastructure.AnsibleTower.Common;
using Devon4Net.Infrastructure.AnsibleTower.Const;
using Devon4Net.Infrastructure.AnsibleTower.Dto;
using Devon4Net.Infrastructure.CircuitBreaker.Common.Enums;
using Devon4Net.Infrastructure.CircuitBreaker.Handler;

namespace Devon4Net.Infrastructure.AnsibleTower.Handler
{
    public class AnsibleTowerHandler : IAnsibleTowerHandler
    {
        private ICircuitBreakerHttpClient CircuitBreakerHttpClient { get; set; }
        private IAnsibleTowerInstance AnsibleTowerInstance { get; set; }

        private string AuthToken { get; set; }

        public AnsibleTowerHandler(IAnsibleTowerInstance ansibleTowerInstance, ICircuitBreakerHttpClient circuitBreakerHttpClient)
        {
            CircuitBreakerHttpClient = circuitBreakerHttpClient;
            AnsibleTowerInstance = ansibleTowerInstance;
        }

        /// <summary>
        /// Performs the basic login authentication
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<string> Login(string userName, string password)
        {
            var authCredential = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{userName}:{password}"));
            var headers = new Dictionary<string, string> {{ "Authorization", $"Basic {authCredential}"}};

            var login = await CircuitBreakerHttpClient.Post<LoginRequestDto>(AnsibleTowerInstance.CircuitBreakerName, AnsibleTowerInstance.ApiDefinition.tokens,null, MediaType.ApplicationJson, headers);

            AuthToken = login.token;
            return login.token;
        }
    }
}
