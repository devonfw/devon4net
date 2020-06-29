using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Devon4Net.Infrastructure.CircuitBreaker.Common.Enums;
using Devon4Net.Infrastructure.CircuitBreaker.Handler;
using Devon4Net.Infrastructure.Common.Options.CyberArk;
using Devon4Net.Infrastructure.CyberArk.Common.Const;
using Devon4Net.Infrastructure.CyberArk.Dto.Account;
using Devon4Net.Infrastructure.CyberArk.Dto.Group;
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

        public Task<GetSafesResponseDto> GetSafes(string authToken = null)
        {
            return GetCyberArk<GetSafesResponseDto>(CyberArkEndpointConst.Safes, false, authToken);
        }

        public Task<GetSafeResponseDto> GetSafe(string idSafe, string authToken = null)
        {
            if (string.IsNullOrEmpty(idSafe))
            {
                throw new ArgumentException("The idSafe cannot be null");
            }

            var searchCriteria = string.IsNullOrEmpty(idSafe) ? string.Empty : $"/{idSafe}";
            return GetCyberArk<GetSafeResponseDto>($"{CyberArkEndpointConst.Safes}{searchCriteria}", false, authToken);
        }

        public Task<AddSafeResponseDto> AddSafe(AddSafeRequestDto safeRequest, string authToken = null)
        {
            if (safeRequest?.safe == null)
            {
                throw new ArgumentException("The safeRequest can not be null");
            }

            return PostCyberArk<AddSafeResponseDto>(CyberArkEndpointConst.Safes, safeRequest, false, authToken);
        }


        public Task<UpdateSafeResponseDto> UpdateSafe(UpdateSafeRequestDto updateSafeRequest, string authToken = null)
        {
            if (updateSafeRequest?.GetSafeResult == null || string.IsNullOrEmpty(updateSafeRequest.GetSafeResult.SafeName))
            {
                throw new ArgumentException("The updateSafeRequest is not correct");
            }

            return PutCyberArk<UpdateSafeResponseDto>($"{CyberArkEndpointConst.Safes}/{updateSafeRequest.GetSafeResult.SafeName}", updateSafeRequest, false, authToken);
        }

        public Task<string> UpdateSafeMember(string safeName, AddSafeMemberRequestDto updateSafeMember, string authToken = null)
        {
            if (updateSafeMember?.member == null || string.IsNullOrEmpty(safeName) || string.IsNullOrEmpty(updateSafeMember.member.MemberName))
            {
                throw new ArgumentException("Please check the updateSafeMember, safe name and the member name");
            }
            return PutCyberArk<string>(string.Format(CyberArkEndpointConst.UpdateSafes, safeName, updateSafeMember.member.MemberName), updateSafeMember, false, authToken);
        }

        public Task<AddSafeMemberRequestDto> AddSafeMember(string safeName, AddSafeMemberRequestDto updateSafeMember, string authToken = null)
        {
            if (updateSafeMember?.member == null || string.IsNullOrEmpty(safeName))
            {
                throw new ArgumentException("Please check the updateSafeMember and safe name");
            }
            return PostCyberArk<AddSafeMemberRequestDto>(string.Format(CyberArkEndpointConst.UpdateSafes, safeName, string.Empty), updateSafeMember, false, authToken);
        }

        #endregion

        #region Account
        public Task<GetAccountsResponseDto> GetAccounts(string searchCriteria = null, string filterCriteria = null, bool useSafeFilter = true, string authToken = null)
        {
            var urlFilterSearch = useSafeFilter ? CyberArkEndpointConst.AccountsSafeNameFilter : CyberArkEndpointConst.AccountsFilter;
            var search = string.IsNullOrEmpty(searchCriteria) ? string.Empty : $"?{string.Format(CyberArkEndpointConst.AccountsSearch, searchCriteria)}";
            var filter = string.IsNullOrEmpty(filterCriteria) ? string.Empty : $"{(string.IsNullOrEmpty(search) ? '?' : '&')}{string.Format(urlFilterSearch, filterCriteria)}";
            var url = $"{CyberArkEndpointConst.Accounts}{search}{filter}";

            return GetCyberArk<GetAccountsResponseDto>(url, false, authToken);
        }

        public Task<AccountDetail> GetAccount(string idAccount, string authToken = null)
        {
            if (string.IsNullOrEmpty(idAccount))
            {
                throw new ArgumentException("The idAccount can not be null");
            }

            return GetCyberArk<AccountDetail>($"{CyberArkEndpointConst.Accounts}/{idAccount}", false, authToken);
        }

        public Task<string> RetrieveAccount(string idAccount, string authToken = null)
        {
            if (string.IsNullOrEmpty(idAccount))
            {
                throw new ArgumentException("The idAccount can not be null");
            }

            return PostCyberArk<string>($"{CyberArkEndpointConst.Accounts}/{idAccount}/{CyberArkEndpointConst.AccountRetrieveSuffix}",null, false, authToken);
        }

        public Task<AddAccountResponseDto> AddAccount(AddAccountRequestDto addAccountRequest, string authToken = null)
        {
            if (addAccountRequest == null)
            {
                throw new ArgumentException("The addAccountRequest can not be null");
            }

            return PostCyberArk<AddAccountResponseDto>(CyberArkEndpointConst.Accounts, addAccountRequest, false, authToken);
        }

        public Task<AddAccountResponseDto> DeleteAccount(string accountName, string authToken = null)
        {
            if (string.IsNullOrEmpty(accountName))
            {
                throw new ArgumentException("The accountName can not be null");
            }

            return DeleteCyberArk<AddAccountResponseDto>($"{CyberArkEndpointConst.Accounts}/{accountName}", false, authToken);
        }

        #endregion

        #region User
        public Task<GetUserResponseDto> GetUser(string userName, string authToken = null)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("The userName can not be null");
            }

            return GetCyberArk<GetUserResponseDto>($"{CyberArkEndpointConst.Users}/{userName}", false, authToken);
        }

        public Task<GetUserResponseDto> GetUsers(GetUsersRequestDto usersRequestDto, string authToken = null)
        {
            return GetCyberArk<GetUserResponseDto>($"{CyberArkEndpointConst.GetUsers}/", usersRequestDto, false, authToken);
        }

        public Task<GetUserResponseDto> AddUser(AddUserRequestDto userRequest, string authToken = null)
        {
            if (userRequest == null || string.IsNullOrEmpty(userRequest.UserName))
            {
                throw new ArgumentException("The userName can not be null");
            }

            return PostCyberArk<GetUserResponseDto>(CyberArkEndpointConst.Users, userRequest, false, authToken);
        }

        public Task<GetUserResponseDto> UpdateUser(UpdateUserRequestDto userRequest, string userName, string authToken = null)
        {
            if (userRequest == null || string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("The userName can not be null");
            }

            return PutCyberArk<GetUserResponseDto>($"{CyberArkEndpointConst.Users}/{userName}", userRequest, false, authToken);
        }



        public Task<DeletedUser> DeleteUser(string userName, string authToken = null)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("The userName can not be null");
            }

            return DeleteCyberArk<DeletedUser>($"{CyberArkEndpointConst.Users}/{userName}", false, authToken);
        }

        public Task<string> ResetUserPassword(string idUser, string newPassword, string authToken = null)
        {
            if (string.IsNullOrEmpty(idUser) || string.IsNullOrEmpty(newPassword) ||!int.TryParse(idUser, out _))
            {
                throw new ArgumentException("The newPassword can not be null or the id user is not correct");
            }

            return PostCyberArk<string>(CyberArkEndpointConst.ResetPassword, new RestePasswordRequestDto{Id = idUser, Password = newPassword}, false, authToken);
        }

        #endregion

        #region Group

        public Task<GetGroupsResponseDto> GetUserGroups(string authToken = null)
        {
            return GetCyberArk<GetGroupsResponseDto>(CyberArkEndpointConst.GetUserGroups, false, authToken);
        }

        /// <summary>
        /// This method is only available in version 11+
        /// </summary>
        /// <param name="createGroupRequest"></param>
        /// <param name="authToken"></param>
        /// <returns></returns>
        public Task<string> CreateUserGroup(CreateGroupRequestDto createGroupRequest, string authToken = null)
        {
            return PostCyberArk<string>(CyberArkEndpointConst.GetUserGroups, createGroupRequest, false, authToken);
        }

        public Task<string> AddUserToGroup(string userName, string groupName, string authToken = null)
        {
            return PostCyberArk<string>(string.Format(CyberArkEndpointConst.AddUserToGroup, groupName), new AddUserToGroupRequestDto{ UserName = userName } , false, authToken);
        }
        #endregion

        #region HttpMethods

        private async Task<T> GetCyberArk<T>(string endpoint, bool useCamelCase, string authToken = null)
        {
            if (!CyberArkOptions.EnableCyberArk) return default;
            await Logon(authToken);
            return await CircuitBreakerHttpClient.Get<T>(CyberArkOptions.CircuitBreakerName, endpoint, GetAuthorizationHeaders(), useCamelCase);
        }

        private async Task<T> GetCyberArk<T>(string endpoint, object content, bool useCamelCase, string authToken = null)
        {
            if (!CyberArkOptions.EnableCyberArk) return default;
            await Logon(authToken);
            return await CircuitBreakerHttpClient.Get<T>(CyberArkOptions.CircuitBreakerName, endpoint, content, GetAuthorizationHeaders(), useCamelCase);
        }

        private async Task<T> PostCyberArk<T>(string endpoint, object dataToSend, bool useCamelCase, string authToken = null)
        {
            if (!CyberArkOptions.EnableCyberArk) return default;
            await Logon(authToken);
            return await CircuitBreakerHttpClient.Post<T>(CyberArkOptions.CircuitBreakerName, endpoint, dataToSend, MediaType.ApplicationJson, GetAuthorizationHeaders(), useCamelCase);
        }

        private async Task<T> PutCyberArk<T>(string endpoint, object dataToSend, bool useCamelCase, string authToken = null)
        {
            if (!CyberArkOptions.EnableCyberArk) return default;
            await Logon(authToken);
            return await CircuitBreakerHttpClient.Put<T>(CyberArkOptions.CircuitBreakerName, endpoint, dataToSend, MediaType.ApplicationJson, GetAuthorizationHeaders(), useCamelCase);
        }

        private async Task<T> DeleteCyberArk<T>(string endpoint,  bool useCamelCase, string authToken = null)
        {
            if (!CyberArkOptions.EnableCyberArk) return default;
            await Logon(authToken);
            return await CircuitBreakerHttpClient.Delete<T>(CyberArkOptions.CircuitBreakerName, endpoint, GetAuthorizationHeaders(), useCamelCase);
        }

        private async Task Logon(string authToken = null)
        {
            if (!string.IsNullOrEmpty(authToken))
            {
                AuthToken = authToken;
                return;
            }

            if (CyberArkOptions == null || string.IsNullOrEmpty(CyberArkOptions.UserName) || string.IsNullOrEmpty(CyberArkOptions.Password))
            {
                throw new CyberArkUnauthorizedException("No CyberArk authorization credentials provided");
            }

            await Logon(CyberArkOptions.UserName, CyberArkOptions.Password).ConfigureAwait(false);
        }

        public async Task<string> Logon(string userName, string password)
        {
            AuthToken = await CircuitBreakerHttpClient.Post<string>(CyberArkOptions.CircuitBreakerName, CyberArkEndpointConst.Logon, new LogonRequestDto { Username = userName, Password = password }, MediaType.ApplicationJson, null, true);

            return AuthToken;
        }

        private Dictionary<string, string> GetAuthorizationHeaders()
        {
            return new Dictionary<string, string> {{ "Authorization", AuthToken}};
        }

        #endregion

    }
}
