using System.Threading.Tasks;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Tenants;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Users;

namespace Devon4Net.Infrastructure.SMAXHCM.Handler
{
    public interface ISmaxHcmHandler
    {
        Task<string> Login(string userName, string password);
        Task<GetUsersResponseDto> GetUsers(string authToken = null);
        Task<SmaxGetUserResponseDto> GetUserById(string userId, string authToken = null);
        Task<GetUserTenantsResponseDto> GetUserTenants(string userId, string authToken = null);
    }
}