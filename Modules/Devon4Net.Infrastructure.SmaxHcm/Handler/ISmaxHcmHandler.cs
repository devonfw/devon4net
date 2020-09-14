using System.Collections.Generic;
using System.Threading.Tasks;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Designer;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Designer.CreateDesignContainer;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Designer.CreateDesignVersion;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Designer.GetDesignTags;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Designer.ServiceDesigner;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Designer.ServiceDesigner.ApplyComponentTemplateToComponent;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Designer.ServiceDesigner.CreateComponentsAndRelations;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Designer.ServiceDesigner.UpdateComponent;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Designer.ServiceDesigner.UpdatePropertyFromComponent;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Offering;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Providers;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Request;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Request.AbandonRequest;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Request.CreateRequest;
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
        Task<SwitchActivationOfferingResponse> SwitchActivationOffering(string offeringId, bool activate = true);
        Task<GetAllRequestsResponseDto> GetAllRequest();
        Task<CreateRequestResponse> CreateRequest(CreateRequestPropertiesDto createNewRequestDto);
        Task<AbandonRequestResponseDto> AbandonRequest(string idRequest);
        Task<CreateOfferingResponseDto> CreateOffering(CreateOfferingRequestDto createOfferingRequestDto);
        Task<GetDesignTagsResponseDto> GetDesignTags();
        Task<GetIconsResponseDto> GetIcons();
        Task<CreateDesignContainerResponseDto> CreateDesignContainer(CreateDesignContainerDto createDesignContainerDto);
        Task<CreateDesignVersionResponseDto> CreateDesignVersion(CreateDesignVersionDto createVersionDto);
        Task DeleteDesignContainer(string containerId);
        Task DeleteDesignVersion(string versionId);
        Task<PublishDesignResponseDto> PublishDesignVersion(string versionId);
        Task<GetServiceDesignerMetamodelResponseDto> GetServiceDesignerMetamodel(string versionId);
        Task<GetComponentTemplatesFromComponentTypeResponseDto> GetComponentTemplatesFromComponentType(string componentTypeId);
        Task<CreateComponentsAndRelationsResponseDto> CreateComponentsAndRelations(string versionId, CreateComponentsAndRelationsDto createComponentsAndRelationsDto);
        Task<ApplyComponentTemplateToComponentResponseDto> ApplyComponentTemplateToComponent(ApplyComponentTemplateToComponentDto applyComponentTemplateToComponentDto);
        Task<GetComponentsResponseDto> GetComponentsFromServiceDesigner(string versionId);
        Task<GetPropertiesFromComponentResponseDto> GetPropertiesFromComponent(string versionId, string componentId);
        Task<UpdatePropertyFromComponentResponseDto> UpdatePropertyFromComponent(string propertyId, string value);
        Task<UpdatePropertyFromComponentResponseDto> UpdatePropertyFromComponent(string propertyId, int value);
        Task<UpdatePropertyFromComponentResponseDto> UpdatePropertyFromComponent(string propertyId, bool value);
        Task<UpdatePropertyFromComponentResponseDto> UpdatePropertyFromComponent(string propertyId, List<UpdateListPropertyFromComponentDto> value);
        Task UpdateComponent(UpdateComponentDto updateComponentDto);
        Task<AddAgregatedOfferingResponseDto> AddAggregatedOffering(AddAgregatedOfferingDto addAgregatedOfferingDto);
        Task<GetOfferingProvidersResponseDto> GetOfferingProviders(string searchText = null, string[] tags = null);
        Task<GetUsersByUserNameResponse> GetUsersByUserName(string username);
        Task<GetRequestResponseDto> GetRequestById(string requestId);
        Task<GetOverviewFromComponentResponseDto> GetOverviewFromComponent(string componentId);
        Task<GetRestUserResponseDto> GetRestUser(string userName);
        Task<byte[]> ExportDesign(string designId, string restUserId);
    }
}