using System.Threading.Tasks;
using Devon4Net.Infrastructure.Log;
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
            Devon4NetLogger.Debug("Executing Login from controller CyberArk");
            return Ok(await SmaxHcmHandler.Login(user, password));
        }

    }
}
