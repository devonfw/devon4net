using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devon4Net.Infrastructure.AnsibleTower.Dto;
using Devon4Net.Infrastructure.AnsibleTower.Dto.Applications;
using Devon4Net.Infrastructure.AnsibleTower.Dto.Common;
using Devon4Net.Infrastructure.AnsibleTower.Dto.Credentials;
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
        [ProducesResponseType(typeof(PaginatedResultDto<ResultApplication>), StatusCodes.Status200OK)]
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
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/ansible/organizations")]
        [ProducesResponseType(typeof(PaginatedResultDto<ResultOrganizationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Organizations([FromHeader] string authenticationToken, string searchCriteria)
        {
            Devon4NetLogger.Debug("Executing Login from controller AnsibleTowerController");
            return Ok(await AnsibleTowerHandler.GetOrganizations(authenticationToken,searchCriteria));
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
        [ProducesResponseType(typeof(PaginatedResultDto<ResultInventoryDto>), StatusCodes.Status200OK)]
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
        [Route("/v1/ansible/jobtemplates")]
        [ProducesResponseType(typeof(PaginatedResultDto<GetJobTemplatesResponseDto>), StatusCodes.Status200OK)]
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
        [Route("/v1/ansible/getjobtemplate")]
        [ProducesResponseType(typeof(GetJobTemplatesResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> JobTemplates([FromHeader] string authenticationToken, string jobTemplateId)
        {
            Devon4NetLogger.Debug("Executing Login from controller AnsibleTowerController");
            return Ok(await AnsibleTowerHandler.GetJobTemplate(authenticationToken, jobTemplateId));
        }

        /// <summary>
        /// Creates a Job Template
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <param name="createJobTemplateRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("/v1/ansible/jobtemplates")]
        [ProducesResponseType(typeof(GetJobTemplatesResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> JobTemplates([FromHeader] string authenticationToken, CreateJobTemplateRequestDto createJobTemplateRequest)
        {
            Devon4NetLogger.Debug("Executing Login from controller AnsibleTowerController");
            return Ok(await AnsibleTowerHandler.CreateJobTemplate(authenticationToken, createJobTemplateRequest));
        }
        #endregion

        #region Credentials
        /// <summary>
        /// Gets the credentials list 
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/ansible/credentials")]
        [ProducesResponseType(typeof(PaginatedResultDto<GetCredentialsResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Credentials([FromHeader] string authenticationToken, string searchCriteria)
        {
            Devon4NetLogger.Debug("Executing Login from controller AnsibleTowerController");
            return Ok(await AnsibleTowerHandler.GetCredentials(authenticationToken, searchCriteria));
        }

        /// <summary>
        /// Gets the credentials list 
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("/v1/ansible/credentials")]
        [ProducesResponseType(typeof(PaginatedResultDto<GetCredentialsResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Credentials([FromHeader] string authenticationToken, CreateCredentialRequestDto createCredentialRequest)
        {
            Devon4NetLogger.Debug("Executing Login from controller AnsibleTowerController");
            return Ok(await AnsibleTowerHandler.CreateCredential(authenticationToken, createCredentialRequest));
        }

        #endregion

        #region FullFlowSample

        /// <summary>
        /// Performs a regular Ansible Tower workflow.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="organizationName"></param>
        /// <param name="credentialName"></param>
        /// <param name="credentialDescription"></param>
        /// <param name="credentialUserName"></param>
        /// <param name="credentialPassword"></param>
        /// <param name="idOrganization"></param>
        /// <param name="idCredentialType"></param>
        /// <param name="idUser"></param>
        /// <returns>Something</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("/v1/ansible/fullflowsample")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> FullFlow(string user, string password, string organizationName,
            string credentialName, string credentialDescription,string credentialUserName, string credentialPassword,
            int idOrganization, int idCredentialType, int idUser)
        {
            var result = new StringBuilder();

            Devon4NetLogger.Debug("Executing FullFlow from controller AnsibleTowerController");
            result.AppendLine($"");
            result.AppendLine($"Perform login with the user; {user} and password: {password}");
            var token = await AnsibleTowerHandler.Login(user, password);
            result.AppendLine($"The user token is: {token.Token}");
            var organizations = await AnsibleTowerHandler.GetOrganizations(token.Token);

            // Organizations
            Devon4NetLogger.Debug("Organization count: " + organizations.count);
            if (organizations?.results != null && organizations.results.Any() && organizations.results.FirstOrDefault()?.name != organizationName)
            {
                return BadRequest();
            }

            var organization = organizations.results?.FirstOrDefault();
            result.AppendLine($"The retreived organization is: name: {organization.name}, id: {organization.id}");

            // Credentials
            var credentialsList = await AnsibleTowerHandler.GetCredentials(token.Token, credentialName);
            if (credentialsList != null && credentialsList.results != null && credentialsList.results.Any()|| credentialsList.results.FirstOrDefault()?.name != credentialName)
            {
                result.AppendLine($"The credentials with name {credentialName} has been retreived");
                var credential = new CreateCredentialRequestDto
                {
                    name = credentialName, 
                    description = credentialDescription, 
                    organization = idOrganization, 
                    credential_type = idCredentialType, 
                    inputs = new Dictionary<string, string>{{ "username", credentialUserName }, {"password", credentialPassword }}, 
                    user = idUser
                };
                result.AppendLine($"Storing the credential...");
                var savedCredential = await AnsibleTowerHandler.CreateCredential(token.Token, credential);
                result.AppendLine($"The credential has been created in Ansible Tower. id: {savedCredential.id}, name: {savedCredential.name}, created: {savedCredential.created}");
            }
            else
            {
                result.AppendLine($"ERROR! The credentials with name {credentialName} has NOT been retreived");
            }

            return Ok(result.ToString());
        }

        #endregion

    }
}
