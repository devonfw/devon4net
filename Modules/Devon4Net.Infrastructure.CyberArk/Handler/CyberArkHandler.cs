using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Devon4Net.Infrastructure.CircuitBreaker.Common.Enums;
using Devon4Net.Infrastructure.CircuitBreaker.Handler;
using Devon4Net.Infrastructure.Common.Options.CyberArk;
using Devon4Net.Infrastructure.CyberArk.Common.Const;
using Devon4Net.Infrastructure.CyberArk.Dto.Account;
using Devon4Net.Infrastructure.CyberArk.Dto.Logon;
using Devon4Net.Infrastructure.CyberArk.Dto.Safe;
using Devon4Net.Infrastructure.CyberArk.Dto.User;
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

        public Task<GetSafesResponseDto> GetSafes()
        {
            return GetCyberArk<GetSafesResponseDto>(CyberArkEndpointConst.Safes, false);
        }

        public Task<GetSafeResponseDto> GetSafe(string idSafe)
        {
            if (string.IsNullOrEmpty(idSafe))
            {
                throw new ArgumentException("The idSafe cannot be null");
            }

            var searchCriteria = string.IsNullOrEmpty(idSafe) ? string.Empty : $"/{idSafe}";
            return GetCyberArk<GetSafeResponseDto>($"{CyberArkEndpointConst.Safes}{searchCriteria}", false);
        }

        public Task<AddSafeResponseDto> AddSafe(AddSafeRequestDto safeRequest)
        {
            if (safeRequest?.safe == null)
            {
                throw new ArgumentException("The safeRequest can not be null");
            }

            return PostCyberArk<AddSafeResponseDto>(CyberArkEndpointConst.Safes, safeRequest, false);
        }


        public Task<UpdateSafeResponseDto> UpdateSafe(UpdateSafeRequestDto updateSafeRequest)
        {
            if (updateSafeRequest?.GetSafeResult == null || string.IsNullOrEmpty(updateSafeRequest.GetSafeResult.SafeName))
            {
                throw new ArgumentException("The updateSafeRequest is not correct");
            }

            return PutCyberArk<UpdateSafeResponseDto>($"{CyberArkEndpointConst.Safes}/{updateSafeRequest.GetSafeResult.SafeName}", updateSafeRequest, false);
        }

        #endregion

        #region Account
        public Task<GetAccountsResponseDto> GetAccounts()
        {
            return GetCyberArk<GetAccountsResponseDto>(CyberArkEndpointConst.Accounts, false);
        }

        public Task<AccountDetail> GetAccount(string idAccount)
        {
            if (string.IsNullOrEmpty(idAccount))
            {
                throw new ArgumentException("The idAccount can not be null");
            }

            return GetCyberArk<AccountDetail>($"{CyberArkEndpointConst.Accounts}/{idAccount}", false);
        }

        public Task<string> RetrieveAccount(string idAccount)
        {
            if (string.IsNullOrEmpty(idAccount))
            {
                throw new ArgumentException("The idAccount can not be null");
            }

            return PostCyberArk<string>($"{CyberArkEndpointConst.Accounts}/{idAccount}/{CyberArkEndpointConst.AccountRetrieveSuffix}",null, false);
        }

        public Task<AddAccountResponseDto> AddAccount(AddAccountRequestDto addAccountRequest)
        {
            if (addAccountRequest == null)
            {
                throw new ArgumentException("The addAccountRequest can not be null");
            }

            return PostCyberArk<AddAccountResponseDto>(CyberArkEndpointConst.Accounts, addAccountRequest, false);
        }

        public Task<AddAccountResponseDto> DeleteAccount(string accountName)
        {
            if (string.IsNullOrEmpty(accountName))
            {
                throw new ArgumentException("The accountName can not be null");
            }

            return DeleteCyberArk<AddAccountResponseDto>($"{CyberArkEndpointConst.Accounts}/{accountName}", false);
        }

        #endregion

        #region User

        public Task<GetUserResponseDto> GetUsers()
        {
            return GetCyberArk<GetUserResponseDto>($"{CyberArkEndpointConst.Users}", false);
        }

        public Task<GetUserResponseDto> GetUser(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("The userName can not be null");
            }

            return GetCyberArk<GetUserResponseDto>($"{CyberArkEndpointConst.Users}/{userName}", false);
        }

        public Task<GetUserResponseDto> AddUser(AddUserRequestDto userRequest)
        {
            if (userRequest == null || string.IsNullOrEmpty(userRequest.UserName))
            {
                throw new ArgumentException("The userName can not be null");
            }

            return PostCyberArk<GetUserResponseDto>(CyberArkEndpointConst.Users, userRequest, false);
        }


        public Task<DeletedUser> DeleteUser(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("The userName can not be null");
            }

            return DeleteCyberArk<DeletedUser>($"{CyberArkEndpointConst.Users}/{userName}", false);
        }
        #endregion

        #region HttpMethods

        private async Task<T> GetCyberArk<T>(string endpoint, bool useCamelCase)
        {
            await Logon();
            return await CircuitBreakerHttpClient.Get<T>(CyberArkOptions.CircuitBreakerName, endpoint, GetAuthorizationHeaders(), useCamelCase);
        }

        private async Task<T> PostCyberArk<T>(string endpoint, object dataToSend, bool useCamelCase)
        {
            await Logon();
            return await CircuitBreakerHttpClient.Post<T>(CyberArkOptions.CircuitBreakerName, endpoint, dataToSend, MediaType.ApplicationJson, GetAuthorizationHeaders(), useCamelCase);
        }

        private async Task<T> PutCyberArk<T>(string endpoint, object dataToSend, bool useCamelCase)
        {
            await Logon();
            return await CircuitBreakerHttpClient.Put<T>(CyberArkOptions.CircuitBreakerName, endpoint, dataToSend, MediaType.ApplicationJson, GetAuthorizationHeaders(), useCamelCase);
        }

        private async Task<T> DeleteCyberArk<T>(string endpoint,  bool useCamelCase)
        {
            await Logon();
            return await CircuitBreakerHttpClient.Delete<T>(CyberArkOptions.CircuitBreakerName, endpoint, GetAuthorizationHeaders(), useCamelCase);
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
