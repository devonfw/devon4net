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

        [HttpPost]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/createoffering")]
        public async Task<IActionResult> CreateNewOffering(CreateOfferingDto offeringRequestDto)
        {
            Devon4NetLogger.Debug("Getting service definitions from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.CreateNewOffering(offeringRequestDto));
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
