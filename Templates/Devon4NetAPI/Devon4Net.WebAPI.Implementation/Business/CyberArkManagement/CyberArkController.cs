using System.Threading.Tasks;
using Devon4Net.Infrastructure.AnsibleTower.Dto;
using Devon4Net.Infrastructure.CyberArk.Dto.Safe;
using Devon4Net.Infrastructure.CyberArk.Handler;
using Devon4Net.Infrastructure.Log;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Devon4Net.WebAPI.Implementation.Business.CyberArkManagement
{
    [ApiController]
    [Route("[controller]")]
    public class CyberArkController : ControllerBase
    {
        private ICyberArkHandler CyberArkHandler { get; }

        public CyberArkController(ICyberArkHandler cyberArkHandler)
        {
            CyberArkHandler = cyberArkHandler;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/v1/cyberark/logon")]
        [ProducesResponseType(typeof(LoginRequestDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login(string user, string password)
        {
            Devon4NetLogger.Debug("Executing Login from controller CyberArk");
            return Ok(await CyberArkHandler.Logon(user, password));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/cyberark/safes")]
        [ProducesResponseType(typeof(GetSafesResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSafes()
        {
            Devon4NetLogger.Debug("Executing Login from controller CyberArk");
            return Ok(await CyberArkHandler.GetSafes());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/cyberark/safe")]
        [ProducesResponseType(typeof(GetSafeResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSafe(string idSafe)
        {
            Devon4NetLogger.Debug("Executing Login from controller CyberArk");
            return Ok(await CyberArkHandler.GetSafe(idSafe));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/v1/cyberark/safe")]
        [ProducesResponseType(typeof(AddSafeResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddSafe(AddSafeRequestDto idSafe)
        {
            Devon4NetLogger.Debug("Executing Login from controller CyberArk");
            return Ok(await CyberArkHandler.AddSafe(idSafe));
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("/v1/cyberark/safe")]
        [ProducesResponseType(typeof(UpdateSafeResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateSafe(UpdateSafeRequestDto updateSafeRequest)
        {
            Devon4NetLogger.Debug("Executing Login from controller CyberArk");
            return Ok(await CyberArkHandler.UpdateSafe(updateSafeRequest));
        }
    }
}
