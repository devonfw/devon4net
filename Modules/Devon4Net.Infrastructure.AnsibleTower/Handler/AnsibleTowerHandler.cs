using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Devon4Net.Infrastructure.AnsibleTower.Common;
using Devon4Net.Infrastructure.AnsibleTower.Const;
using Devon4Net.Infrastructure.CircuitBreaker.Common.Enums;
using Devon4Net.Infrastructure.CircuitBreaker.Handler;

namespace Devon4Net.Infrastructure.AnsibleTower.Handler
{
    public class AnsibleTowerHandler : IAnsibleTowerHandler
    {
        private ICircuitBreakerHttpClient CircuitBreakerHttpClient { get; set; }
        private IAnsibleTowerInstance AnsibleTowerInstance { get; set; }

        private string CsrfToken { get; set; }
        private string Path { get; set; }

        public AnsibleTowerHandler(IAnsibleTowerInstance ansibleTowerInstance, ICircuitBreakerHttpClient circuitBreakerHttpClient)
        {
            CircuitBreakerHttpClient = circuitBreakerHttpClient;
            AnsibleTowerInstance = ansibleTowerInstance;
        }

        public async Task<string> Login(string userName, string password)
        {
            var result = await CircuitBreakerHttpClient.GetResponseMessage(AnsibleTowerInstance.CircuitBreakerName, AnsibleConst.LoginUrl);

            var cookie = result.Headers.FirstOrDefault(header => header.Key == "Set-Cookie").Value.FirstOrDefault();
            var cookieParams = cookie.Split(';').ToList();
            var ansibleParams = new Dictionary<string, string>();

            foreach (var value in cookieParams)
            {
                var pair = value.Split('=').ToList();
                if (pair.Any() && pair.Count > 0)
                {
                    ansibleParams.Add(pair.FirstOrDefault(), pair.LastOrDefault());
                }
            }

            CsrfToken = ansibleParams.FirstOrDefault(x => x.Key == AnsibleConst.LoginTokenName).Value;
            Path = ansibleParams.FirstOrDefault(x => x.Key == AnsibleConst.LoginTokenPath).Value;

            var headers = new Dictionary<string,string>{  };
            //headers.Add("Content-Type", " application/x-www-form-urlencoded");

            var login = await CircuitBreakerHttpClient.Post<string>(AnsibleTowerInstance.CircuitBreakerName, AnsibleConst.LoginUrl,$"username={userName}&password={password}&csrfmiddlewaretoken={CsrfToken}&next=%2fapi%2f", "application/x-www-form-urlencoded", headers);


            return "";
        }
    }
}
