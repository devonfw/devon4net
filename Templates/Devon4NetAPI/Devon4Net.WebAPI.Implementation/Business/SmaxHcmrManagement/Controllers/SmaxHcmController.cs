using System.Threading.Tasks;
using Devon4Net.Infrastructure.Log;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Designer;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Offering;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Providers;
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


        [HttpPost]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/cookielogin")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CookieLogin(string tenantId, string user, string password)
        {
            Devon4NetLogger.Debug("Executing Login from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.CookieLogin(tenantId, user, password));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/users")]
        [ProducesResponseType(typeof(GetUsersResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUsers(string authToken = null)
        {
            Devon4NetLogger.Debug("Executing Login from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.GetUsers(authToken));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/user")]
        [ProducesResponseType(typeof(SmaxGetUserResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserById(string userId, string authToken = null)
        {
            Devon4NetLogger.Debug("Executing Login from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.GetUserById(userId, authToken));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/usertenants")]
        [ProducesResponseType(typeof(GetUserTenantsResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserTenants(string userId, string authToken = null)
        {
            Devon4NetLogger.Debug("Executing Login from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.GetUserTenants(userId, authToken));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/offerings")]
        [ProducesResponseType(typeof(GetOfferingsResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOfferings(string tenantId, string authToken = null)
        {
            Devon4NetLogger.Debug("Executing Login from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.GetOfferings(tenantId, authToken));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/offering")]
        [ProducesResponseType(typeof(GetOfferingResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Offering(string tenantId, string offeringId, string authToken = null)
        {
            Devon4NetLogger.Debug("Executing Login from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.GetOffering(tenantId, offeringId, authToken));
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/providers")]
        [ProducesResponseType(typeof(GetProvidersResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProviders(string tenantId, string authToken = null)
        {
            Devon4NetLogger.Debug("Executing Login from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.GetProviders(tenantId,  authToken));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/design")]
        [ProducesResponseType(typeof(GetDesignResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDesign(string tenantId, string designId, string authToken = null)
        {
            Devon4NetLogger.Debug("Executing Login from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.GetDesign(tenantId, designId, authToken));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/catalogproviders")]
        public async Task<IActionResult> GetCatalogProviders(string category, bool includeArticles, bool includeOfferings, string query, string tenantId, string authToken = null)
        {
            Devon4NetLogger.Debug("Getting catalog providers from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.GetCatalogProviders(category, includeArticles, includeOfferings, query, authToken, tenantId));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/servicedefinitions")]
        public async Task<IActionResult> GetServiceDefinitions(string tenantId, string authToken = null)
        {
            Devon4NetLogger.Debug("Getting service definitions from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.GetServiceDefinitions(authToken, tenantId));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/createoffering")]
        public async Task<IActionResult> CreateNewOffering(CreateOfferingDto offeringRequestDto, string tenantId, string authToken = null)
        {
            Devon4NetLogger.Debug("Getting service definitions from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.CreateNewOffering(offeringRequestDto, authToken, tenantId));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/smaxhcm/request")]
        [ProducesResponseType(typeof(GetOfferingResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllRequest(string tenantId, string authToken = null)
        {
            Devon4NetLogger.Debug("Executing Login from controller SmaxHcm");
            return Ok(await SmaxHcmHandler.GetAllRequest( authToken, tenantId));
        }
    }
}
