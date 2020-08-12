using System.Threading.Tasks;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Designer;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Offering;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Providers;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Request.CreateRequest;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Request.GetRequest;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Tenants;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Users;

namespace Devon4Net.Infrastructure.SMAXHCM.Handler
{
    public interface ISmaxHcmHandler
    {
        Task<string> Login(string userName, string password);
        Task<GetUsersResponseDto> GetUsers();
        Task<SmaxGetUserResponseDto> GetUserById(string userId);
        Task<GetUserTenantsResponseDto> GetUserTenants(string userId);
        Task<GetOfferingsResponseDto> GetOfferings();
        Task<GetOfferingResponseDto> GetOffering(string offeringId);
        Task<GetProvidersResponseDto> GetProviders();
        Task<GetDesignResponseDto> GetDesign(string designId);
        Task<object> GetCatalogProviders(string category, bool includeArticles, bool includeOfferings, string query);
        Task<GetOfferingsResponseDto> GetServiceDefinitions();
        Task<object> UpdateOffering(UpdateOfferingDto updateOfferingDto);
        Task<ActivateOfferingResponse> ActivateOffering(ActivateOfferingDto activateOfferingDto);
        Task<GetAllRequestDto> GetAllRequest();
        Task<CreateRequestResponse> CreateRequest(CreateRequestPropertiesDto createNewRequestDto);
        Task<CreateOfferingResponseDto> CreateOffering(CreateOfferingRequestDto createOfferingRequestDto);
        Task<GetDesignTagsResponseDto> GetDesignTags();
        Task<GetIconsResponseDto> GetIcons();
        Task<CreateDesignContainerResponseDto> CreateDesignContainer(CreateDesignContainerDto createDesignContainerDto);
        Task<CreateDesignVersionResponseDto> CreateDesignVersion(CreateDesignVersionDto createVersionDto);
    }
}