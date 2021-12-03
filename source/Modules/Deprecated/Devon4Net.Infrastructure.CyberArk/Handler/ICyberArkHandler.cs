﻿using Devon4Net.Infrastructure.CyberArk.Dto.Account;
using Devon4Net.Infrastructure.CyberArk.Dto.Group;
using Devon4Net.Infrastructure.CyberArk.Dto.Safe;
using Devon4Net.Infrastructure.CyberArk.Dto.User;

namespace Devon4Net.Infrastructure.CyberArk.Handler
{
    public interface ICyberArkHandler
    {
        Task<string> Logon(string userName, string password);
        Task<GetSafesResponseDto> GetSafes(string authToken = null);
        Task<GetSafeResponseDto> GetSafe(string idSafe, string authToken = null);
        Task<AddSafeResponseDto> AddSafe(AddSafeRequestDto safeRequest, string authToken = null);
        Task<UpdateSafeResponseDto> UpdateSafe(UpdateSafeRequestDto updateSafeRequest, string authToken = null);
        Task<GetAccountsResponseDto> GetAccounts(string searchCriteria = null, string filterCriteria = null, bool useSafeFilter = true, string authToken = null);
        Task<AccountDetail> GetAccount(string idAccount, string authToken = null);
        Task<AddAccountResponseDto> AddAccount(AddAccountRequestDto addAccountRequest, string authToken = null);
        Task<AddAccountResponseDto> DeleteAccount(string accountName, string authToken = null);
        Task<string> RetrieveAccount(string idAccount, string authToken = null);
        Task<GetUserResponseDto> GetUsers(GetUsersRequestDto usersRequestDto, string authToken = null);
        Task<GetUserResponseDto> GetUser(string userName, string authToken = null);
        Task<GetUserResponseDto> AddUser(AddUserRequestDto userRequest, string authToken = null);
        Task<GetUserResponseDto> UpdateUser(UpdateUserRequestDto userRequest, string userName, string authToken = null);
        Task<DeletedUser> DeleteUser(string userName, string authToken = null);
        Task<string> UpdateSafeMember(string safeName, AddSafeMemberRequestDto updateSafeMember, string authToken = null);
        Task<string> ResetUserPassword(string idUser, string newPassword, string authToken = null);
        Task<GetGroupsResponseDto> GetUserGroups(string authToken = null);
        Task<string> CreateUserGroup(CreateGroupRequestDto createGroupRequest, string authToken = null);
        Task<string> AddUserToGroup(string userName, string groupName, string authToken = null);
        Task<AddSafeMemberRequestDto> AddSafeMember(string safeName, AddSafeMemberRequestDto updateSafeMember, string authToken = null);
    }
}