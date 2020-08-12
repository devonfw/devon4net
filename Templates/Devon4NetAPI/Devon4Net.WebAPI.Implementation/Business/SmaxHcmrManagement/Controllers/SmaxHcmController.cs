using System.Threading.Tasks;
using Devon4Net.Infrastructure.Log;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Designer;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Designer.CreateDesignContainer;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Designer.CreateDesignVersion;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Offering;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Providers;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Request.CreateRequest;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Request.GetRequest;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Tenants;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Users;
using Devon4Net.Infrastructure.SMAXHCM.Handler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Devon4Net.WebAPI.Implementation.Business.SmaxHcmrManagement.Controllers
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
        [Route("/v1/smaxhcm/activateOffering")]
        public async Task<IActionResult> ActivateOffering(string offeringId)
        {
            var activateOfferingDto = new ActivateOfferingDto
            {
                offeringId = offeringId
            };

            return Ok(await SmaxHcmHandler.ActivateOffering(activateOfferingDto));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/request")]
        [ProducesResponseType(typeof(GetAllRequestDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllRequest()
        {
            Devon4NetLogger.Debug("Executing GetAllRequest from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.GetAllRequest());
        }

        /// <summary>
        /// sample: { "entity": { "entity_type": "Request", "properties": { "ImpactScope": "Enterprise", "Urgency": "NoDisruption", "RequestedByPerson": "10016", "RequestsOffering": "11428", "DisplayLabel": "Request from Postman", "Description": "<p>This is a request created from Postman</p>", "UserOptions": "{"complexTypeProperties":[{"properties":{"OptionSet5FCCC5624DDDBA17DA397224C5D4ED0B_c":{"Option55F063755603608CB8287224C5D426DE_c":true},"OptionSetB9C2FB0D304A86418C651492D37A5E72_c":{"Option90AB77B8232585150B951492D37AE403_c":true},"changedUserOptionsForSimulation":"EndDate&","PropertyamazonResourceProvider55F063755603608CB8287224C5D426DE_c":"8a809efe73146d590173147b4a954cfc","Propertyregion55F063755603608CB8287224C5D426DE_c":"eu-west-1","PropertyPlatformType55F063755603608CB8287224C5D426DE_c":"Amazon Linux#eu-west-1","Propertykeypair55F063755603608CB8287224C5D426DE_c":"FARM-1-0d7bdb2a","PropertyvpcId55F063755603608CB8287224C5D426DE_c":"eu-west-1#vpc-017f010990ed442ce","Propertyimage55F063755603608CB8287224C5D426DE_c":"ami-0093757e056f6fe96","PropertysubnetId55F063755603608CB8287224C5D426DE_c":"subnet-0dcd276d74eb84059"}}]}", "DataDomains": [ "Public" ], "StartDate": 1596664800000, "EndDate": 1599170400000, "RequestAttachments": "{"complexTypeProperties":[]}" } }, "operation": "CREATE" } 
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
    }
}
