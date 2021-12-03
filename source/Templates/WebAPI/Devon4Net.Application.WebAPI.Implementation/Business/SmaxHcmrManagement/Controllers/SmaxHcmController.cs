using Devon4Net.Infrastructure.Log;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Designer;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Designer.CreateDesignContainer;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Designer.CreateDesignVersion;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Designer.ServiceDesigner;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Designer.ServiceDesigner.ApplyComponentTemplateToComponent;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Designer.ServiceDesigner.CreateComponentsAndRelations;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Designer.ServiceDesigner.UpdateComponent;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Designer.ServiceDesigner.UpdatePropertyFromComponent;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Offering;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Providers;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Request;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Request.CreateRequest;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Tenants;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Users;
using Devon4Net.Infrastructure.SMAXHCM.Handler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Devon4Net.Application.WebAPI.Implementation.Business.SmaxHcmrManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("CorsPolicy")]
    public class SmaxHcmController : ControllerBase
    {
        private ISmaxHcmHandler SmaxHcmHandler { get; set; }

        public SmaxHcmController(ISmaxHcmHandler mediatRHandler)
        {
            SmaxHcmHandler = mediatRHandler;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/login")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login(string user, string password)
        {
            Devon4NetLogger.Debug("Executing Login from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.Login(user, password));
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/users")]
        [ProducesResponseType(typeof(GetUsersResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUsers()
        {
            Devon4NetLogger.Debug("Executing Login from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.GetUsers());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/user")]
        [ProducesResponseType(typeof(SmaxGetUserResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserById(string userId)
        {
            Devon4NetLogger.Debug("Executing Login from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.GetUserById(userId));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/usertenants")]
        [ProducesResponseType(typeof(GetUserTenantsResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserTenants(string userId)
        {
            Devon4NetLogger.Debug("Executing Login from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.GetUserTenants(userId));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/offerings")]
        [ProducesResponseType(typeof(GetOfferingsResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOfferings()
        {
            Devon4NetLogger.Debug("Executing Login from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.GetOfferings());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/offering")]
        [ProducesResponseType(typeof(GetOfferingResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Offering(string offeringId)
        {
            Devon4NetLogger.Debug("Executing Login from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.GetOffering(offeringId));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/getOfferingProviders")]
        public async Task<IActionResult> GetOfferingProviders(string searchText, string[] tags)
        {
            Devon4NetLogger.Debug("Executing GetOfferingProviders from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.GetOfferingProviders(searchText, tags));
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/providers")]
        [ProducesResponseType(typeof(GetProvidersResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProviders()
        {
            Devon4NetLogger.Debug("Executing Login from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.GetProviders());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/design")]
        [ProducesResponseType(typeof(GetDesignResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDesign(string designId)
        {
            Devon4NetLogger.Debug("Executing Login from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.GetDesign(designId));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/exportDesign")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ExportDesign(string designId, string restUserId)
        {
            Devon4NetLogger.Debug("Executing ExportDesign from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.ExportDesign(designId, restUserId));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/catalogproviders")]
        public async Task<IActionResult> GetCatalogProviders(string category, bool includeArticles, bool includeOfferings, string query)
        {
            Devon4NetLogger.Debug("Getting catalog providers from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.GetCatalogProviders(category, includeArticles, includeOfferings, query));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/servicedefinitions")]
        public async Task<IActionResult> GetServiceDefinitions()
        {
            Devon4NetLogger.Debug("Getting service definitions from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.GetServiceDefinitions());
        }

        /// <summary>
        /// sample: { "NumOfRequests":0, "IsPopularity":false, "Service":"11427", "RequireAssetInfo":"InfrastructurePeripheral", "DisplayLabel":"New offering from SMAX UI", "IsBundle":true, "IsDefault":true, "Description":"<p>New offering from SMAX UI Description</p>"}
        /// </summary>
        /// <param name="createNewRequestDto"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/offering")]
        [ProducesResponseType(typeof(CreateOfferingResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateOffering(CreateOfferingRequestDto createNewRequestDto)
        {
            Devon4NetLogger.Debug("Executing CreateOffering from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.CreateOffering(createNewRequestDto));
        }

        /// <summary>
        /// sample: {"providerId":"11526","offeringId":"d18ae624-d68c-4dd5-b0f6-53c8cba1cb38","service":"12736","offeringDisplayName":"Capgemini Test Design"}
        /// </summary>
        /// <param name="addAgregatedOfferingDto"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/addagregatedoffering")]
        [ProducesResponseType(typeof(CreateOfferingResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAgregatedOffering(AddAgregatedOfferingDto addAgregatedOfferingDto)
        {
            Devon4NetLogger.Debug("Executing CreateOffering from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.AddAggregatedOffering(addAgregatedOfferingDto));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/updateoffering")]
        public async Task<IActionResult> UpdateOffering(UpdateOfferingDto offeringRequestDto)
        {
            Devon4NetLogger.Debug("Getting service definitions from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.UpdateOffering(offeringRequestDto));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/switchActivationOffering")]
        public async Task<IActionResult> SwitchActivationOffering(string offeringId, bool activate)
        {
            return Ok(await SmaxHcmHandler.SwitchActivationOffering(offeringId, activate));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/request")]
        [ProducesResponseType(typeof(GetAllRequestsResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllRequest()
        {
            Devon4NetLogger.Debug("Executing GetAllRequest from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.GetAllRequest());
        }

        /// <summary>
        /// sample: { "impactScope": "Enterprise", "urgency": "NoDisruption", "requestedByPerson": "10016", "requestsOffering": "11428", "displayLabel": "Request from .net API", "description": "This is a request created from .NET API", "userOptions": "", "dataDomains": [ "string" ], "startDate": "2020-08-20T07:28:37.890Z", "endDate": "2020-08-21T07:28:37.890Z", "requestAttachments": "{\"complexTypeProperties\":[]}" } 
        /// </summary>
        /// <param name="createNewRequestDto"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/request")]
        [ProducesResponseType(typeof(CreateRequestResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateRequest(CreateRequestPropertiesDto createNewRequestDto)
        {
            Devon4NetLogger.Debug("Executing CreateRequest from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.CreateRequest(createNewRequestDto));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/getUsersByName")]
        [ProducesResponseType(typeof(GetUsersByUserNameResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUsersByUsername(string username)
        {
            Devon4NetLogger.Debug("Executing GetUsersByUsername from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.GetUsersByUserName(username));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/getRestUserByName")]
        [ProducesResponseType(typeof(GetRestUserResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRestUserByName(string username)
        {
            Devon4NetLogger.Debug("Executing GetRestUserByName from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.GetRestUser(username));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/designIcons")]
        [ProducesResponseType(typeof(GetIconsResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDesignIcons()
        {
            Devon4NetLogger.Debug("Executing GetDesignIcons from controller SmaxHcm");
            var response = await SmaxHcmHandler.GetIcons();
            return Ok(response);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/designerTags")]
        [ProducesResponseType(typeof(CreateRequestResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDesignerTags()
        {
            Devon4NetLogger.Debug("Executing GetDesignerTags from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.GetDesignTags());
        }

        /// <summary>
        /// Sample: {"name":"Test Create Design BE","description":"Test Create Design BE desc","icon":"/903361753/dnd/api/blobstore/amazon_ec2.png?tag=library","tags":["/903361753/dnd/api/tag/8a809efe73146d590173147414bd05e1"]}
        /// </summary>
        /// <param name="createDesignContainerDto"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/createDesignContainer")]
        [ProducesResponseType(typeof(CreateDesignContainerResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateDesignContainer(CreateDesignContainerDto createDesignContainerDto)
        {
            Devon4NetLogger.Debug("Executing CreateDesignContainer from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.CreateDesignContainer(createDesignContainerDto));
        }

        /// <summary>
        /// Sample: {"containerId":"8a808a9c73d970f80173e2fa990b1590","description":"Version description","icon":"/903361753/dnd/api/blobstore/amazon_ec2.png?tag=library","name":"Version name","published":false,"url":"","version":"1.0.0"}
        /// </summary>
        /// <param name="createDesignVersionDto"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/createDesignVersion")]
        [ProducesResponseType(typeof(CreateDesignVersionResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateDesignVersion(CreateDesignVersionDto createDesignVersionDto)
        {
            Devon4NetLogger.Debug("Executing CreateDesignVersion from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.CreateDesignVersion(createDesignVersionDto));
        }

        [HttpDelete]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/deleteDesignVersion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteDesignVersion(string versionId)
        {
            Devon4NetLogger.Debug("Executing DeleteDesignVersion from controller SmaxHcm");
            await SmaxHcmHandler.DeleteDesignVersion(versionId);
            return Ok();
        }

        [HttpDelete]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/deleteDesignContainer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteDesignContainer(string containerId)
        {
            Devon4NetLogger.Debug("Executing DeleteDesignContainer from controller SmaxHcm");
            await SmaxHcmHandler.DeleteDesignContainer(containerId);
            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/publishDesignVersion")]
        [ProducesResponseType(typeof(PublishDesignResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PublishDesignVersion(string versionId)
        {
            Devon4NetLogger.Debug("Executing PublishDesignVersion from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.PublishDesignVersion(versionId));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/getServiceDesignerMetamodel")]
        [ProducesResponseType(typeof(GetServiceDesignerMetamodelResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetServiceDesignerMetamodel(string versionId)
        {
            Devon4NetLogger.Debug("Executing GetServiceDesignerMetamodel from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.GetServiceDesignerMetamodel(versionId));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/getComponentTemplateForComponentType")]
        [ProducesResponseType(typeof(GetComponentTemplatesFromComponentTypeResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetComponentTemplatesFromComponentType(string componentTypeId)
        {
            Devon4NetLogger.Debug("Executing GetComponentTemplatesFromComponentType from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.GetComponentTemplatesFromComponentType(componentTypeId));
        }

        /// <summary>
        /// Sample: {"nodes":[{"name":"Test Capgemini server group","displayName":"Test Capgemini server group","description":"Test Capgemini server group description","icon":"/903361753/dnd/api/blobstore/server_group.png?tag=library","orderIndex":1,"typeId":"8a808a9c73d970f80173e345c3ba1811","x":11,"y":80},{"name":"Test Capgemini AWS server","displayName":"Test Capgemini AWS server","description":"Test Capgemini AWS server description","icon":"/903361753/dnd/api/blobstore/amazon_ec2.png?tag=library","orderIndex":1,"typeId":"8a808a9c73d970f80173e36043c61868","x":511,"y":80}],"relationships":[{"name":"Relationship test","displayName":"Child","relationshipTypeId":"8a808a9c73d970f80173e3d42c931cd0","sourceName":"Test Capgemini server group","targetName":"Test Capgemini AWS server"}]}
        /// </summary>
        /// <param name="versionId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/createComponentsAndRelations")]
        [ProducesResponseType(typeof(CreateComponentsAndRelationsResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateComponentsAndRelations(string versionId, CreateComponentsAndRelationsDto createComponentsAndRelationsDto)
        {
            Devon4NetLogger.Debug("Executing CreateComponentsAndRelations from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.CreateComponentsAndRelations(versionId, createComponentsAndRelationsDto));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/getOverviewFromComponent")]
        [ProducesResponseType(typeof(GetOverviewFromComponentResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOverviewFromComponent(string componentId)
        {
            Devon4NetLogger.Debug("Executing GetOverviewFromComponent from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.GetOverviewFromComponent(componentId));
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/updateComponent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateComponent(UpdateComponentDto updateComponentDto)
        {
            Devon4NetLogger.Debug("Executing UpdateComponent from controller SmaxHcm");
            await SmaxHcmHandler.UpdateComponent(updateComponentDto);
            return Ok();
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/applyTemplateToComponent")]
        [ProducesResponseType(typeof(ApplyComponentTemplateToComponentResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ApplyComponentTemplateToComponent(ApplyComponentTemplateToComponentDto applyComponentTemplateToComponentDto)
        {
            Devon4NetLogger.Debug("Executing ApplyComponentTemplateToComponent from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.ApplyComponentTemplateToComponent(applyComponentTemplateToComponentDto));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/getComponentsFromServiceDesigner")]
        [ProducesResponseType(typeof(GetComponentsResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetComponentsFromServiceDesigner(string versionId)
        {
            Devon4NetLogger.Debug("Executing GetComponentsFromServiceDesigner from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.GetComponentsFromServiceDesigner(versionId));
        }

        
        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/getPropertiesFromComponent")]
        [ProducesResponseType(typeof(GetPropertiesFromComponentResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPropertiesFromComponent(string versionId, string componentId)
        {
            Devon4NetLogger.Debug("Executing GetPropertiesFromComponent from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.GetPropertiesFromComponent(versionId, componentId));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/updateStringPropertyFromComponent")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateStringPropertyFromComponent(string propertyId, string value)
        {
            Devon4NetLogger.Debug("Executing UpdateStringPropertyFromComponent from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.UpdatePropertyFromComponent(propertyId, value));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/updateNumberPropertyFromComponent")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateNumberPropertyFromComponent(string propertyId, int value)
        {
            Devon4NetLogger.Debug("Executing UpdateNumberPropertyFromComponent from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.UpdatePropertyFromComponent(propertyId, value));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/updateBoolPropertyFromComponent")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateBoolPropertyFromComponent(string propertyId, bool value)
        {
            Devon4NetLogger.Debug("Executing UpdateBoolPropertyFromComponent from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.UpdatePropertyFromComponent(propertyId, value));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/updateListPropertyFromComponent")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateListPropertyFromComponent(string propertyId, List<UpdateListPropertyFromComponentDto> updateListPropertyFromComponentDtos)
        {
            Devon4NetLogger.Debug("Executing UpdateListPropertyFromComponent from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.UpdatePropertyFromComponent(propertyId, updateListPropertyFromComponentDtos));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/getRequestById")]
        [ProducesResponseType(typeof(GetRequestResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRequestById(string requestId)
        {
            Devon4NetLogger.Debug("Executing GetRequestById from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.GetRequestById(requestId));
        }
    }
}
