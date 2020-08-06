using System.Threading.Tasks;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Designer;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Offering;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Providers;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Request;
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
        Task<GetOfferingsResponseDto> GetOfferings(string tenantId, string authToken = null);
        Task<GetOfferingResponseDto> GetOffering(string tenantId, string offeringId, string authToken = null);
        Task<GetProvidersResponseDto> GetProviders(string tenantId, string authToken);
        Task<GetDesignResponseDto> GetDesign(string tenantId, string designId, string authToken = null);
        Task<object> GetCatalogProviders(string category, bool includeArticles, bool includeOfferings, string query, string authToken = null, string tenantId = null);
        Task<GetOfferingsResponseDto> GetServiceDefinitions(string authToken = null, string tenantId = null);
        Task<object> CreateNewOffering(CreateOfferingDto createOfferingDto, string authToken = null, string tenantId = null);
        Task<GetAllRequestDto> GetAllRequest(string authToken = null, string tenantId = null);
        Task<string> CookieLogin(string tenantId, string userName, string password);
    }
}