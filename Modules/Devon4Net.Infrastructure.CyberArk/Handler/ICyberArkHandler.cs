using System.Threading.Tasks;
using Devon4Net.Infrastructure.CyberArk.Dto.Account;
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
        Task<GetAccountsResponseDto> GetAccounts(string authToken = null);
        Task<AccountDetail> GetAccount(string idAccount, string authToken = null);
        Task<AddAccountResponseDto> AddAccount(AddAccountRequestDto addAccountRequest, string authToken = null);
        Task<AddAccountResponseDto> DeleteAccount(string accountName, string authToken = null);
        Task<string> RetrieveAccount(string idAccount, string authToken = null);
        Task<GetUserResponseDto> GetUser(string userName, string authToken = null);
        Task<GetUserResponseDto> AddUser(AddUserRequestDto userRequest, string authToken = null);
        Task<DeletedUser> DeleteUser(string userName, string authToken = null);
        Task<string> UpdateSafeMember(string safeName, string memberName, AddSafeMemberRequestDto updateSafeMember, string authToken = null);
        Task<string> ResetUserPassword(string idUser, string newPassword, string authToken = null);
        Task<GetGroupsResponseDto> GetUserGroups(string authToken = null);
        Task<string> CreateUserGroup(CreateGroupRequestDto createGroupRequest, string authToken = null);
        Task<string> AddUserToGroup(AddUserToGroupRequestDto addUserToGroupRequest, string authToken = null);
        Task<string> AddUserToGroup(AddUserToGroupOldRequestDto addUserToGroupOldRequest, string authToken = null);
    }
}