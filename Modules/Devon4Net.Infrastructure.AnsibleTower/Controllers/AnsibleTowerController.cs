using System.Threading.Tasks;
using Devon4Net.Infrastructure.AnsibleTower.Dto;
using Devon4Net.Infrastructure.AnsibleTower.Dto.Applications;
using Devon4Net.Infrastructure.AnsibleTower.Dto.Inventories;
using Devon4Net.Infrastructure.AnsibleTower.Dto.JobTemplates;
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


        #region Security

        /// <summary>
        /// Performs the basic Ansible Tower login
        /// </summary>
        /// <param name="user">User name</param>
        /// <param name="password">User password</param>
        /// <returns>The Ansible Tower login object result</returns>
        [HttpPost]
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

        #endregion

        #region Applications

        /// <summary>
        /// Gets the list of applications
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/ansible/applications")]
        [ProducesResponseType(typeof(GetApplicationsResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Applications([FromHeader] string authenticationToken)
        {
            Devon4NetLogger.Debug("Executing Login from controller AnsibleTowerController");
            return Ok(await AnsibleTowerHandler.GetApplications(authenticationToken));
        }

        /// <summary>
        /// Creates an application
        /// </summary>
        /// <param name="applicationsRequest"></param>
        /// <param name="authenticationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("/v1/ansible/applications")]
        [ProducesResponseType(typeof(ApplicationsResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Applications(ApplicationsRequestDto applicationsRequest, [FromHeader] string authenticationToken)
        {
            Devon4NetLogger.Debug("Executing Login from controller AnsibleTowerController");
            return Ok(await AnsibleTowerHandler.CreateApplication(applicationsRequest, authenticationToken));
        }

        #endregion

        #region Organizations

        /// <summary>
        /// Gets the organizations list 
        /// Please check the OrganizationRolesConst and OrganizationRelatedLinksConst classes
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <returns></returns>
        [HttpGet]
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

        /// <summary>
        /// Gets the organization given the organization identifier
        /// Please check the OrganizationRolesConst and OrganizationRelatedLinksConst classes
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/ansible/getorganization")]
        [ProducesResponseType(typeof(ResultOrganizationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrganization([FromHeader] string authenticationToken, string organizationId)
        {
            Devon4NetLogger.Debug("Executing Login from controller AnsibleTowerController");
            if (string.IsNullOrEmpty(organizationId)) return BadRequest();
            return Ok(await AnsibleTowerHandler.GetOrganizationById(authenticationToken, organizationId));
        }

        /// <summary>
        /// Creates an organization
        /// Please check the OrganizationRolesConst and OrganizationRelatedLinksConst classes
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <param name="organizationRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("/v1/ansible/organizations")]
        [ProducesResponseType(typeof(ResultOrganizationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostOrganization([FromHeader] string authenticationToken, CreateOrganizationRequestDto organizationRequest)
        {
            Devon4NetLogger.Debug("Executing Login from controller AnsibleTowerController");
            return Ok(await AnsibleTowerHandler.CreateOrganization(authenticationToken, organizationRequest));
        }

        #endregion

        #region Inventories

        /// <summary>
        /// Gets the list of inventories
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/ansible/inventories")]
        [ProducesResponseType(typeof(GetInventoriesResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Inventories([FromHeader] string authenticationToken)
        {
            Devon4NetLogger.Debug("Executing Login from controller AnsibleTowerController");
            return Ok(await AnsibleTowerHandler.GetInventories(authenticationToken));
        }

        /// <summary>
        /// Gets the inventory by the provided inventory Id
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <param name="inventoryId"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/ansible/getinventory")]
        [ProducesResponseType(typeof(ResultInventoryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Inventories([FromHeader] string authenticationToken, string inventoryId)
        {
            Devon4NetLogger.Debug("Executing Login from controller AnsibleTowerController");
            return Ok(await AnsibleTowerHandler.GetInventoryById(authenticationToken, inventoryId));
        }

        /// <summary>
        /// Creates tan inventory
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <param name="inventoryRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("/v1/ansible/inventories")]
        [ProducesResponseType(typeof(ResultInventoryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Inventories([FromHeader] string authenticationToken, CreateInventoryRequestDto inventoryRequest)
        {
            Devon4NetLogger.Debug("Executing Login from controller AnsibleTowerController");
            return Ok(await AnsibleTowerHandler.PostInventories(authenticationToken, inventoryRequest));
        }
        #endregion

        #region JobTemplates

        /// <summary>
        /// Gets the list of Job Templates
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/ansible/JobTemplates")]
        [ProducesResponseType(typeof(GetJobTemplatesResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> JobTemplates([FromHeader] string authenticationToken)
        {
            Devon4NetLogger.Debug("Executing Login from controller AnsibleTowerController");
            return Ok(await AnsibleTowerHandler.GetJobTemplates(authenticationToken));
        }

        /// <summary>
        /// Gets a Job Templates by the provided template job id
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <param name="jobTemplateId"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/ansible/GetJobTemplate")]
        [ProducesResponseType(typeof(ResultJobDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> JobTemplates([FromHeader] string authenticationToken, string jobTemplateId)
        {
            Devon4NetLogger.Debug("Executing Login from controller AnsibleTowerController");
            return Ok(await AnsibleTowerHandler.GetJobTemplate(authenticationToken, jobTemplateId));
        }

        /// <summary>
        /// Gets the list of Job Templates
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <param name="createJobTemplateRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("/v1/ansible/JobTemplates")]
        [ProducesResponseType(typeof(ResultJobDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> JobTemplates([FromHeader] string authenticationToken, CreateJobTemplateRequestDto createJobTemplateRequest)
        {
            Devon4NetLogger.Debug("Executing Login from controller AnsibleTowerController");
            return Ok(await AnsibleTowerHandler.CreateJobTemplate(authenticationToken, createJobTemplateRequest));
        }
        #endregion

    }
}
