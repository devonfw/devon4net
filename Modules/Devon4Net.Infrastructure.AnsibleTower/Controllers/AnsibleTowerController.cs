using System.Threading.Tasks;
using Devon4Net.Infrastructure.AnsibleTower.Dto;
using Devon4Net.Infrastructure.AnsibleTower.Dto.Applications;
using Devon4Net.Infrastructure.AnsibleTower.Dto.Organizations;
using Devon4Net.Infrastructure.AnsibleTower.Handler;
using Devon4Net.Infrastructure.Log;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Devon4Net.Infrastructure.AnsibleTower.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnsibleTowerController : ControllerBase
    {
        private IAnsibleTowerHandler AnsibleTowerHandler { get; set; }

        public AnsibleTowerController(IAnsibleTowerHandler ansibleTowerHandler)
        {
            AnsibleTowerHandler = ansibleTowerHandler;
        }


        /// <summary>
        /// Performs the basic Ansible Tower login
        /// </summary>
        /// <param name="user">User name</param>
        /// <param name="password">User password</param>
        /// <returns>The Ansible Tower login object result</returns>
        [HttpPost]
        [HttpOptions]
        [AllowAnonymous]
        [Route("/v1/ansible/login")]
        [ProducesResponseType(typeof(LoginRequestDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login(string user, string password)
        {
            Devon4NetLogger.Debug("Executing Login from controller AnsibleTowerController");
            return Ok(await AnsibleTowerHandler.Login(user, password));
        }

        /// <summary>
        /// Gets the Ansible tower application endpoint
        /// </summary>
        /// <param name="applicationsRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [HttpOptions]
        [AllowAnonymous]
        [Route("/v1/ansible/applications")]
        [ProducesResponseType(typeof(ApplicationsResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Applications(ApplicationsRequestDto applicationsRequest, [FromHeader] string authenticationToken)
        {
            Devon4NetLogger.Debug("Executing Login from controller AnsibleTowerController");
            return Ok(await AnsibleTowerHandler.Applications(applicationsRequest, authenticationToken));
        }

        /// <summary>
        /// Gets the Ansible tower application endpoint
        /// Please check the class OrganizationRelatedLinksConst class
        /// </summary>
        /// <param name="applicationsRequest"></param>
        /// <returns></returns>
        [HttpGet]
        [HttpOptions]
        [AllowAnonymous]
        [Route("/v1/ansible/organizations")]
        [ProducesResponseType(typeof(OrganizationsResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Organizations([FromHeader] string authenticationToken)
        {
            Devon4NetLogger.Debug("Executing Login from controller AnsibleTowerController");
            return Ok(await AnsibleTowerHandler.GetOrganizations(authenticationToken));
        }

    }
}
