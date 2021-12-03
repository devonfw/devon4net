using System.Text;
using Devon4Net.Infrastructure.AnsibleTower.Dto;
using Devon4Net.Infrastructure.AnsibleTower.Dto.Applications;
using Devon4Net.Infrastructure.AnsibleTower.Dto.Common;
using Devon4Net.Infrastructure.AnsibleTower.Dto.Credentials;
using Devon4Net.Infrastructure.AnsibleTower.Dto.Inventories;
using Devon4Net.Infrastructure.AnsibleTower.Dto.Jobs;
using Devon4Net.Infrastructure.AnsibleTower.Dto.JobTemplates;
using Devon4Net.Infrastructure.AnsibleTower.Dto.Organizations;
using Devon4Net.Infrastructure.AnsibleTower.Dto.Projects;
using Devon4Net.Infrastructure.AnsibleTower.Handler;
using Devon4Net.Infrastructure.Log;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Devon4Net.Application.WebAPI.Implementation.Business.AnsibleTowerManagement.Controllers
{
    /// <summary>
    /// Ansible Tower Controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AnsibleTowerController : ControllerBase
    {
        private IAnsibleTowerHandler AnsibleTowerHandler { get; set; }

        /// <summary>
        /// Ansible Tower Controller constructor
        /// </summary>
        public AnsibleTowerController(IAnsibleTowerHandler ansibleTowerHandler)
        {
            AnsibleTowerHandler = ansibleTowerHandler;
        }


        #region Security
        /// <summary>
        /// Ping Ansible Tower
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/ansible/ping")]
        [ProducesResponseType(typeof(PaginatedResultDto<PingResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Ping()
        {
            Devon4NetLogger.Debug("Executing Login from controller AnsibleTowerController");
            return Ok(await AnsibleTowerHandler.Ping());
        }

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
            return Ok(await AnsibleTowerHandler.CreateInventory(authenticationToken, inventoryRequest));
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
        /// Creates a credential 
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <param name="createCredentialRequest">Please check the input params. Inputs sample: url (CyberArk cojur), api_key, account, username, cacert</param>
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

        #region Projects
        /// <summary>
        /// Gets the projects list 
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/ansible/projects")]
        [ProducesResponseType(typeof(PaginatedResultDto<GetProjectsRequestDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Projects([FromHeader] string authenticationToken, string searchCriteria)
        {
            Devon4NetLogger.Debug("Executing Login from controller AnsibleTowerController");
            return Ok(await AnsibleTowerHandler.GetProjects(authenticationToken, searchCriteria));
        }

        /// <summary>
        /// Creates a project
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <param name="createCredentialRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("/v1/ansible/projects")]
        [ProducesResponseType(typeof(PaginatedResultDto<GetProjectsRequestDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Projects([FromHeader] string authenticationToken, CreateProjectRequestDto createCredentialRequest)
        {
            Devon4NetLogger.Debug("Executing Login from controller AnsibleTowerController");
            return Ok(await AnsibleTowerHandler.CreateProject(authenticationToken, createCredentialRequest));
        }

        /// <summary>
        /// Deletes a project by its Id
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpDelete]
        [AllowAnonymous]
        [Route("/v1/ansible/projects")]
        [ProducesResponseType(typeof(PaginatedResultDto<string>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProject([FromHeader] string authenticationToken, string projectId)
        {
            Devon4NetLogger.Debug("Executing Login from controller AnsibleTowerController");
            return Ok(await AnsibleTowerHandler.DeleteProject(authenticationToken, projectId));
        }
        #endregion

        #region Jobs

        /// <summary>
        /// Gets the list of Job Templates
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/ansible/jobs")]
        [ProducesResponseType(typeof(PaginatedResultDto<GetJobResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Jobs([FromHeader] string authenticationToken, string searchCriteria)
        {
            Devon4NetLogger.Debug("Executing Login from controller AnsibleTowerController");
            return Ok(await AnsibleTowerHandler.GetJobs(authenticationToken, searchCriteria));
        }

        /// <summary>
        /// Returns if a Job can be canceled
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <param name="idJob"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/ansible/jobcancel")]
        [ProducesResponseType(typeof(GetCanCancelResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AskJobCancel([FromHeader] string authenticationToken, int idJob)
        {
            Devon4NetLogger.Debug("Executing Login from controller AnsibleTowerController");
            return Ok(await AnsibleTowerHandler.CanCancelJob(authenticationToken, idJob));
        }

        /// <summary>
        /// Cancels a job
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <param name="idJob"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("/v1/ansible/jobcancel")]
        [ProducesResponseType(typeof(PaginatedResultDto<GetJobEventsResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> JobCancel([FromHeader] string authenticationToken, int idJob)
        {
            Devon4NetLogger.Debug("Executing Login from controller AnsibleTowerController");
            return Ok(await AnsibleTowerHandler.CancelJob(authenticationToken, idJob));
        }

        /// <summary>
        /// Gets the event list of a Job 
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <param name="idJob"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/ansible/jobevents")]
        [ProducesResponseType(typeof(PaginatedResultDto<GetJobEventsResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> JobEvents([FromHeader] string authenticationToken, int idJob)
        {
            Devon4NetLogger.Debug("Executing Login from controller AnsibleTowerController");
            return Ok(await AnsibleTowerHandler.GetJobEvents(authenticationToken, idJob));
        }

        /// <summary>
        /// Gets the event list of a Job 
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <param name="idJob"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/ansible/jobeschedule")]
        [ProducesResponseType(typeof(PaginatedResultDto<GetCanCancelResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> JobSchedule([FromHeader] string authenticationToken, int idJob)
        {
            Devon4NetLogger.Debug("Executing Login from controller AnsibleTowerController");
            return Ok(await AnsibleTowerHandler.GetCanJobSchedule(authenticationToken, idJob));
        }
        #endregion

        #region JobTemplates

        /// <summary>
        /// Gets the job templates
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <param name="jobtemplateId"></param>
        /// <returns>if the template has been deleted</returns>
        [HttpDelete]
        [AllowAnonymous]
        [Route("/v1/ansible/jobtemplates")]
        [ProducesResponseType(typeof(PaginatedResultDto<string>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteJobTemplate([FromHeader] string authenticationToken, string jobtemplateId)
        {
            Devon4NetLogger.Debug("Executing Login from controller AnsibleTowerController");
            return Ok(await AnsibleTowerHandler.DeleteJobTemplate(authenticationToken, jobtemplateId));
        }

        #endregion

        #region FullFlowSample

        /// <summary>
        /// Performs a regular Ansible Tower workflow.
        /// </summary>
        /// <param name="ansibleUser"></param>
        /// <param name="ansiblePassword"></param>
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
        public async Task<IActionResult> FullFlow(string ansibleUser, string ansiblePassword, string organizationName,
            string credentialName, string credentialDescription,string credentialUserName, string credentialPassword,
            int idOrganization, int idCredentialType, int idUser)
        {
            var result = new StringBuilder();

            Devon4NetLogger.Debug("Executing FullFlow from controller AnsibleTowerController");
            result.AppendLine($"");
            result.AppendLine($"Perform login with the user; {ansibleUser} and password: {ansiblePassword}");
            var token = await AnsibleTowerHandler.Login(ansibleUser, ansiblePassword);
            result.AppendLine($"The user token is: {token.token}");
            var organizations = await AnsibleTowerHandler.GetOrganizations(token.token);

            // Organizations
            Devon4NetLogger.Debug("Organization count: " + organizations.count);
            if (organizations.results != null && organizations.results.Any() && organizations.results.FirstOrDefault()?.name != organizationName)
            {
                return BadRequest();
            }

            var organization = organizations.results?.FirstOrDefault();
            result.AppendLine($"The retreived organization is: name: {organization?.name}, id: {organization?.id}");

            // Credentials
            var credentialsList = await AnsibleTowerHandler.GetCredentials(token.token, credentialName);
            if (credentialsList?.results != null && credentialsList.results.Any()|| credentialsList?.results?.FirstOrDefault()?.name != credentialName)
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
                var savedCredential = await AnsibleTowerHandler.CreateCredential(token.token, credential);
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
