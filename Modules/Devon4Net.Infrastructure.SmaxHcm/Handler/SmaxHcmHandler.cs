using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Devon4Net.Infrastructure.CircuitBreaker.Common.Enums;
using Devon4Net.Infrastructure.CircuitBreaker.Handler;
using Devon4Net.Infrastructure.Common.Options.SmaxHcm;
using Devon4Net.Infrastructure.SmaxHcm.Common;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Login;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Tenants;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Users;
using Devon4Net.Infrastructure.SmaxHcm.Exceptions;
using Microsoft.Extensions.Options;

namespace Devon4Net.Infrastructure.SMAXHCM.Handler
{
    public class SmaxHcmHandler : ISmaxHcmHandler
    {
        private ICircuitBreakerHttpClient CircuitBreakerHttpClient { get; }
        private SmaxHcmOptions SmaxHcmOptions { get; }
        private string AuthToken { get; set; }

        public SmaxHcmHandler(ICircuitBreakerHttpClient circuitBreakerHttpClient, IOptions<SmaxHcmOptions> smaxHcmOptions)
        {
            CircuitBreakerHttpClient = circuitBreakerHttpClient;
            SmaxHcmOptions = smaxHcmOptions?.Value ?? throw new ArgumentException("No SmaxHcm options provided");
        }

        #region Users

        public async Task<GetUsersResponseDto> GetUsers(string authToken = null)
        {
            await Login(AuthToken);

            return await CircuitBreakerHttpClient.Get<GetUsersResponseDto>(SmaxHcmOptions.CircuitBreakerName, SmaxHcmEndpointConst.Users, GetAuthorizationHeaders());
        }

        public async Task<SmaxGetUserResponseDto> GetUserById(string userId, string authToken = null)
        {
            await Login(authToken);

            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("The userId can not be null");
            }

            
            return await CircuitBreakerHttpClient.Get<SmaxGetUserResponseDto>(SmaxHcmOptions.CircuitBreakerName, string.Format(SmaxHcmEndpointConst.User,userId,DateTime.Now.Ticks.ToString()), GetAuthorizationHeaders());
        }

        #endregion

        #region Tenants

        public async Task<GetUserTenantsResponseDto> GetUserTenants(string userId, string authToken = null)
        {
            await Login(AuthToken);

            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("The userId can not be null");
            }

            return await CircuitBreakerHttpClient.Get<GetUserTenantsResponseDto>(SmaxHcmOptions.CircuitBreakerName, string.Format(SmaxHcmEndpointConst.UserTenants,userId, DateTime.Now.Ticks.ToString()), GetAuthorizationHeaders());
        }

        #endregion

        #region Http

        private async Task Login(string authToken = null)
        {
            if (!string.IsNullOrEmpty(authToken))
            {
                AuthToken = authToken;
                return;
            }

            if (SmaxHcmOptions == null || string.IsNullOrEmpty(SmaxHcmOptions.UserName) || string.IsNullOrEmpty(SmaxHcmOptions.Password))
            {
                throw new SmaxHcmUnauthorizedException("No CyberArk authorization credentials provided");
            }

            AuthToken = await Login(SmaxHcmOptions.UserName, SmaxHcmOptions.Password).ConfigureAwait(false);
        }

        public async Task<string> Login(string userName, string password)
        {
            AuthToken = await CircuitBreakerHttpClient.Post(SmaxHcmOptions.CircuitBreakerName, SmaxHcmEndpointConst.Logon, new LoginRequestDto { Login = userName, Password = password }, MediaType.ApplicationJson, null, true);
            return AuthToken;
        }

        #endregion

        #region HttpMethods
        private async Task<T> GetSmaxHcm<T>(string endpoint, string tenantId, bool useCamelCase, string authToken = null)
        {
            await Login(authToken);
            return await CircuitBreakerHttpClient.Get<T>(SmaxHcmOptions.CircuitBreakerName, GetUrlWithTenant(endpoint, tenantId), GetAuthorizationHeaders(), useCamelCase);
        }

        private async Task<T> PostSmaxHcm<T>(string endpoint, string tenantId, object dataToSend, bool useCamelCase, string authToken = null)
        {
            await Login(authToken);
            return await CircuitBreakerHttpClient.Post<T>(SmaxHcmOptions.CircuitBreakerName, GetUrlWithTenant(endpoint, tenantId), dataToSend, MediaType.ApplicationJson, GetAuthorizationHeaders(), useCamelCase);
        }

        private async Task<T> PutSmaxHcm<T>(string endpoint, string tenantId, object dataToSend, bool useCamelCase, string authToken = null)
        {
            await Login(authToken);
            return await CircuitBreakerHttpClient.Put<T>(SmaxHcmOptions.CircuitBreakerName, GetUrlWithTenant(endpoint, tenantId), dataToSend, MediaType.ApplicationJson, GetAuthorizationHeaders(), useCamelCase);
        }

        private async Task<T> DeleteSmaxHcm<T>(string endpoint, string tenantId, bool useCamelCase, string authToken = null)
        {
            await Login(authToken);
            return await CircuitBreakerHttpClient.Delete<T>(SmaxHcmOptions.CircuitBreakerName, GetUrlWithTenant(endpoint, tenantId), GetAuthorizationHeaders(), useCamelCase);
        }

        private string GetUrlWithTenant(string originalUrl, string tenantId)
        {
            return string.IsNullOrEmpty(tenantId) ? originalUrl : $"{originalUrl}&TENANTID={tenantId}";
        }

        private Dictionary<string, string> GetAuthorizationHeaders()
        {
            return new Dictionary<string, string>
            {
                {"Cookie", $"{SmaxHcmEndpointConst.AuthorizationHeaderTokenkey}={AuthToken}"}
            };
        }

        /// <summary>
        /// Sets the internal Auth token variable value
        /// </summary>
        /// <param name="authenticationToken"></param>
        private void SetAutehnticationToken(string authenticationToken)
        {
            if (string.IsNullOrEmpty(authenticationToken))
            {
                throw new SmaxHcmUnauthorizedException("No authorization token provided");
            }

            AuthToken = authenticationToken;
        }
        #endregion
    }
}
