using System.Threading.Tasks;
using Devon4Net.Infrastructure.CyberArk.Dto.Account;
using Devon4Net.Infrastructure.CyberArk.Dto.Safe;
using Devon4Net.Infrastructure.CyberArk.Dto.User;

namespace Devon4Net.Infrastructure.CyberArk.Handler
{
    public interface ICyberArkHandler
    {
        Task<string> Logon(string userName, string password);
        Task<string> Logon();
        Task<GetSafesResponseDto> GetSafes();
        Task<GetSafeResponseDto> GetSafe(string idSafe);
        Task<AddSafeResponseDto> AddSafe(AddSafeRequestDto safeRequest);
        Task<UpdateSafeResponseDto> UpdateSafe(UpdateSafeRequestDto updateSafeRequest);
        Task<GetAccountsResponseDto> GetAccounts();
        Task<AccountDetail> GetAccount(string idAccount);
        Task<AddAccountResponseDto> AddAccount(AddAccountRequestDto addAccountRequest);
        Task<AddAccountResponseDto> DeleteAccount(string accountName);
        Task<string> RetrieveAccount(string idAccount);
        Task<GetUserResponseDto> GetUser(string userName);
        Task<GetUserResponseDto> AddUser(AddUserRequestDto userRequest);
        Task<DeletedUser> DeleteUser(string userName);
    }
}