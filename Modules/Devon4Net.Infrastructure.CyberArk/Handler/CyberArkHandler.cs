using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Devon4Net.Infrastructure.CircuitBreaker.Common.Enums;
using Devon4Net.Infrastructure.CircuitBreaker.Handler;
using Devon4Net.Infrastructure.Common.Options.CyberArk;
using Devon4Net.Infrastructure.CyberArk.Common.Const;
using Devon4Net.Infrastructure.CyberArk.Dto.Logon;
using Devon4Net.Infrastructure.CyberArk.Dto.Safe;
using Devon4Net.Infrastructure.CyberArk.Exceptions;
using Microsoft.Extensions.Options;

namespace Devon4Net.Infrastructure.CyberArk.Handler
{
    public class CyberArkHandler : ICyberArkHandler
    {
        private ICircuitBreakerHttpClient CircuitBreakerHttpClient { get; }
        private CyberArkOptions CyberArkOptions { get; }
        private string AuthToken { get; set; }

        public CyberArkHandler(ICircuitBreakerHttpClient circuitBreakerHttpClient, IOptions<CyberArkOptions> cyberArkOptions)
        {
            CircuitBreakerHttpClient = circuitBreakerHttpClient;
            CyberArkOptions = cyberArkOptions?.Value ?? throw new ArgumentException("No CyberArk options provided");
        }

        #region Safe

        public async Task<GetSafesResponseDto> GetSafes()
        {
            await Logon();

            return await GetCyberArk<GetSafesResponseDto>(AuthToken, CyberArkEndpointConst.Safes, false).ConfigureAwait(false);
        }

        public async Task<GetSafeResponseDto> GetSafe(string idSafe)
        {
            await Logon();

            var searchCriteria = string.IsNullOrEmpty(idSafe) ? string.Empty : $"/{idSafe}";
            return await GetCyberArk<GetSafeResponseDto>(AuthToken, $"{CyberArkEndpointConst.Safes}{searchCriteria}", false).ConfigureAwait(false);
        }

        public async Task<AddSafeResponseDto> AddSafe(AddSafeRequestDto safeRequest)
        {
            await Logon();
            return await PostCyberArk<AddSafeResponseDto>(AuthToken, CyberArkEndpointConst.Safes, safeRequest, false);;
        }


        public async Task<UpdateSafeResponseDto> UpdateSafe(UpdateSafeRequestDto updateSafeRequest)
        {
            if (updateSafeRequest?.GetSafeResult == null || string.IsNullOrEmpty(updateSafeRequest.GetSafeResult.SafeName))
            {
                throw new ArgumentException("The updateSafeRequest is not correct");
            }

            await Logon();
            return await PutCyberArk<UpdateSafeResponseDto>(AuthToken, $"{CyberArkEndpointConst.Safes}/{updateSafeRequest.GetSafeResult.SafeName}", updateSafeRequest, false); ;
        }

        #endregion

        #region HttpMethods

        private Task<T> GetCyberArk<T>(string authenticationToken, string endpoint, bool useCamelCase)
        {
            SetAutehnticationToken(authenticationToken);
            return CircuitBreakerHttpClient.Get<T>(CyberArkOptions.CircuitBreakerName, endpoint, GetAuthorizationHeaders(), useCamelCase);
        }

        private Task<T> PostCyberArk<T>(string authenticationToken, string endpoint, object dataToSend, bool useCamelCase)
        {
            SetAutehnticationToken(authenticationToken);
            return CircuitBreakerHttpClient.Post<T>(CyberArkOptions.CircuitBreakerName, endpoint, dataToSend, MediaType.ApplicationJson, GetAuthorizationHeaders(), useCamelCase);
        }

        private Task<T> PutCyberArk<T>(string authenticationToken, string endpoint, object dataToSend, bool useCamelCase)
        {
            SetAutehnticationToken(authenticationToken);
            return CircuitBreakerHttpClient.Put<T>(CyberArkOptions.CircuitBreakerName, endpoint, dataToSend, MediaType.ApplicationJson, GetAuthorizationHeaders(), useCamelCase);
        }

        private void SetAutehnticationToken(string authenticationToken)
        {
            if (string.IsNullOrEmpty(authenticationToken))
            {
                throw new CyberArkUnauthorizedException("No authorization token provided");
            }

            AuthToken = authenticationToken;
        }

        public async Task<string> Logon(string userName, string password)
        {
            AuthToken = await CircuitBreakerHttpClient.Post<string>(CyberArkOptions.CircuitBreakerName,
                CyberArkEndpointConst.Logon,
                new LogonRequestDto{Username = userName, Password = password},MediaType.ApplicationJson, null, true);

            return AuthToken;
        }

        public async Task<string> Logon()
        {
            if (CyberArkOptions == null || string.IsNullOrEmpty(CyberArkOptions.UserName) || string.IsNullOrEmpty(CyberArkOptions.Password))
            {
                throw new CyberArkUnauthorizedException("No CyberArk authorization credentials provided");
            }

            AuthToken = await CircuitBreakerHttpClient.Post<string>(CyberArkOptions.CircuitBreakerName,
                CyberArkEndpointConst.Logon,
                new LogonRequestDto { Username = CyberArkOptions.UserName, Password = CyberArkOptions.Password}, MediaType.ApplicationJson, null, true);

            return AuthToken;
        }

        private Dictionary<string, string> GetAuthorizationHeaders()
        {
            return new Dictionary<string, string> {{ "Authorization", AuthToken}};
        }

        #endregion

    }
}
