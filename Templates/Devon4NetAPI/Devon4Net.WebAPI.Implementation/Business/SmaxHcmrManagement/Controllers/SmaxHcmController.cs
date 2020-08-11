using System.Threading.Tasks;
using Devon4Net.Infrastructure.Log;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Designer;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Offering;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Providers;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Request.CreateRequest;
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
        /// sample: {"entities":[{"entity_type":"Offering","properties":{"NumOfRequests":0,"IsPopularity":false,"Service":"11427","OfferingType":"ServiceOffering","Status":"Active","SubscriptionActionType":"All","RequireAssetInfo":"InfrastructurePeripheral","DisplayLabel":"New offering from SMAX UI","IsBundle":true,"IsDefault":true,"Description":"<p>New offering from SMAX UI Description</p>"}}],"operation":"CREATE"}
        /// </summary>
        /// <param name="createNewRequestDto"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/offering")]
        [ProducesResponseType(typeof(CreateRequestResponse), StatusCodes.Status200OK)]
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
        public async Task<IActionResult> CreateNewOffering(string offeringId)
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
        [ProducesResponseType(typeof(GetOfferingResponseDto), StatusCodes.Status200OK)]
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
        public async Task<IActionResult> CreateRequest(CreateNewRequestDto createNewRequestDto)
        {
            Devon4NetLogger.Debug("Executing CreateRequest from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.CreateRequest(createNewRequestDto));
        }
    }
}
