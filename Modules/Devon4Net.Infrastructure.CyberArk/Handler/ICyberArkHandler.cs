using System.Threading.Tasks;
using Devon4Net.Infrastructure.CyberArk.Dto.Safe;

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
    }
}