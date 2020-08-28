using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Devon4Net.Infrastructure.AnsibleTower.Common;
using Devon4Net.Infrastructure.AnsibleTower.Dto;
using Devon4Net.Infrastructure.AnsibleTower.Dto.Applications;
using Devon4Net.Infrastructure.AnsibleTower.Dto.Common;
using Devon4Net.Infrastructure.AnsibleTower.Dto.Credentials;
using Devon4Net.Infrastructure.AnsibleTower.Dto.Inventories;
using Devon4Net.Infrastructure.AnsibleTower.Dto.Jobs;
using Devon4Net.Infrastructure.AnsibleTower.Dto.JobTemplates;
using Devon4Net.Infrastructure.AnsibleTower.Dto.Organizations;
using Devon4Net.Infrastructure.AnsibleTower.Dto.Projects;
using Devon4Net.Infrastructure.AnsibleTower.Exceptions;
using Devon4Net.Infrastructure.CircuitBreaker.Common.Enums;
using Devon4Net.Infrastructure.CircuitBreaker.Handler;
using Microsoft.AspNetCore.Mvc;

namespace Devon4Net.Infrastructure.AnsibleTower.Handler
{
    /// <summary>
    /// Ansible Tower API manager
    /// </summary>
    public class AnsibleTowerHandler : IAnsibleTowerHandler
    {
        private IHttpClientHandler HttpClientHandler { get; }
        private IAnsibleTowerInstance AnsibleTowerInstance { get; }
        private string AuthToken { get; set; }

        /// <summary>
        /// Ansible Tower handler constructor
        /// </summary>
        /// <param name="ansibleTowerInstance"></param>
        /// <param name="circuitBreakerHttpClient"></param>
        public AnsibleTowerHandler(IAnsibleTowerInstance ansibleTowerInstance, IHttpClientHandler httpClientHandler)
        {
            HttpClientHandler = httpClientHandler;
            AnsibleTowerInstance = ansibleTowerInstance;
        }

        #region Login

        /// <summary>
        /// Performs the basic login on Ansible Tower
        /// </summary>
        /// <param name="userName">UserName</param>
        /// <param name="password">Password</param>
        /// <returns>Result object structure</returns>
        [Route("/v1/ansible/Ansiblelogin")]
        public async Task<LoginRequestDto> Login(string userName, string password)
        {
            var result = await HttpClientHandler.Send<LoginRequestDto>(HttpMethod.Post, AnsibleTowerInstance.CircuitBreakerName, AnsibleTowerInstance.ApiDefinition.tokens, null, MediaType.ApplicationJson, GetLoginHeaders(userName, password)).ConfigureAwait(false);
            //var result = await CircuitBreakerHttpClient.Post<LoginRequestDto>(AnsibleTowerInstance.CircuitBreakerName, AnsibleTowerInstance.ApiDefinition.tokens, null, MediaType.ApplicationJson, GetLoginHeaders(userName, password), true).ConfigureAwait(false);
            AuthToken = result.token;

            return result;
        }

        #endregion

        #region Applications

        /// <summary>
        /// Creates an application object in Ansible Tower
        /// </summary>
        /// <param name="applicationstRequest"></param>
        /// <param name="authenticationToken"></param>
        /// <returns></returns>
        public Task<ApplicationsResponseDto> CreateApplication(ApplicationsRequestDto applicationstRequest, string authenticationToken)
        {
            return PostAnsible<ApplicationsResponseDto>(authenticationToken,AnsibleTowerInstance.ApiDefinition.applications, applicationstRequest);
        }

        /// <summary>
        /// Gets an application object from Ansible Tower
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <returns></returns>
        public Task<PaginatedResultDto<ResultApplication>> GetApplications(string authenticationToken)
        {
            return GetAnsible<PaginatedResultDto<ResultApplication>>(authenticationToken, AnsibleTowerInstance.ApiDefinition.applications);
        }

        #endregion

        #region Organizations
        /// <summary>
        /// Gets an organization object from Ansible Tower by its identification
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        public Task<ResultOrganizationDto> GetOrganizationById(string authenticationToken, string organizationId)
        {
            return GetAnsible<ResultOrganizationDto>(authenticationToken, $"{AnsibleTowerInstance.ApiDefinition.organizations}{organizationId}/");
        }

        /// <summary>
        /// Creates an Organization object in Ansible Tower
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <param name="organizationRequest"></param>
        /// <returns></returns>
        public Task<ResultOrganizationDto> CreateOrganization(string authenticationToken, CreateOrganizationRequestDto organizationRequest)
        {
            return PostAnsible<ResultOrganizationDto>(authenticationToken, AnsibleTowerInstance.ApiDefinition.organizations, organizationRequest);
        }

        /// <summary>
        /// Gets the organization objects from Ansible Tower
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
        public Task<PaginatedResultDto<ResultOrganizationDto>> GetOrganizations(string authenticationToken, string searchCriteria = null)
        {
            return GetAnsible<PaginatedResultDto<ResultOrganizationDto>>(authenticationToken, AnsibleTowerInstance.ApiDefinition.organizations , searchCriteria);
        }
        #endregion

        #region Inventories
        /// <summary>
        /// Gets an Invetory object from Ansible Tower
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <returns></returns>
        public Task<PaginatedResultDto<ResultInventoryDto>> GetInventories(string authenticationToken)
        {
            return GetAnsible<PaginatedResultDto<ResultInventoryDto>>(authenticationToken,AnsibleTowerInstance.ApiDefinition.inventory);
        }

        /// <summary>
        /// Gets an Inventory object from Ansible Tower via its Id
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <param name="inventoryId"></param>
        /// <returns></returns>
        public Task<ResultInventoryDto> GetInventoryById(string authenticationToken, string inventoryId)
        {
            return GetAnsible<ResultInventoryDto>(authenticationToken, $"{AnsibleTowerInstance.ApiDefinition.inventory}{inventoryId}/");
        }

        /// <summary>
        /// Creates an Inventory in Ansible Tower
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <param name="inventoryRequest"></param>
        /// <returns></returns>
        public Task<ResultInventoryDto> CreateInventory(string authenticationToken, CreateInventoryRequestDto inventoryRequest)
        {
            return PostAnsible<ResultInventoryDto>(authenticationToken, AnsibleTowerInstance.ApiDefinition.inventory, inventoryRequest);
        }
        #endregion

        #region JobTemplates
        /// <summary>
        /// Get the Job Templates
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <returns></returns>
        public Task<PaginatedResultDto<GetJobTemplatesResponseDto>> GetJobTemplates(string authenticationToken)
        {
            return GetAnsible<PaginatedResultDto<GetJobTemplatesResponseDto>>(authenticationToken, AnsibleTowerInstance.ApiDefinition.job_templates);
        }

        /// <summary>
        /// Gets a Job Template
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <param name="jobTemplateId"></param>
        /// <returns></returns>
        public Task<GetJobTemplatesResponseDto> GetJobTemplate(string authenticationToken, string jobTemplateId)
        {
            return GetAnsible<GetJobTemplatesResponseDto>(authenticationToken, $"{AnsibleTowerInstance.ApiDefinition.job_templates}{jobTemplateId}/");
        }

        /// <summary>
        /// Creates a Job template in Ansible Tower
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <param name="createJobTemplateRequest"></param>
        /// <returns></returns>
        public Task<GetJobTemplatesResponseDto> CreateJobTemplate(string authenticationToken, CreateJobTemplateRequestDto createJobTemplateRequest)
        {
            return PostAnsible<GetJobTemplatesResponseDto>(authenticationToken, AnsibleTowerInstance.ApiDefinition.job_templates, createJobTemplateRequest);
        }
        #endregion

        #region Credentials
        /// <summary>
        /// Creates a Credential object in Ansible Tower
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
        public Task<PaginatedResultDto<GetCredentialsResponseDto>> GetCredentials(string authenticationToken, string searchCriteria = null)
        {
            return GetAnsible<PaginatedResultDto<GetCredentialsResponseDto>>(authenticationToken, AnsibleTowerInstance.ApiDefinition.credentials , searchCriteria);
        }

        /// <summary>
        /// Creates a Credential object in Ansible Tower
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <param name="credentialRequest"></param>
        /// <returns></returns>
        public Task<GetCredentialsResponseDto> CreateCredential(string authenticationToken, CreateCredentialRequestDto credentialRequest)
        {
            return PostAnsible<GetCredentialsResponseDto>(authenticationToken, AnsibleTowerInstance.ApiDefinition.credentials, credentialRequest);
        }
        #endregion

        #region Projects
        /// <summary>
        /// Gets the Projects list from Ansible Tower
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
        public Task<PaginatedResultDto<GetProjectsRequestDto>> GetProjects(string authenticationToken, string searchCriteria = null)
        {
            return GetAnsible<PaginatedResultDto<GetProjectsRequestDto>>(authenticationToken, AnsibleTowerInstance.ApiDefinition.projects , searchCriteria);
        }

        /// <summary>
        /// Creates a Project in Ansible Tower
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <param name="credentialRequest"></param>
        /// <returns></returns>
        public Task<GetProjectsRequestDto> CreateProject(string authenticationToken, CreateProjectRequestDto credentialRequest)
        {
            return PostAnsible<GetProjectsRequestDto>(authenticationToken, AnsibleTowerInstance.ApiDefinition.credentials, credentialRequest);
        }

        /// <summary>
        /// Deletes a Project in Ansible Tower by Id
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <param name="credentialRequest"></param>
        /// <returns></returns>
        public Task<string> DeleteProject(string authenticationToken, string projectId)
        {
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentException("The project id cannot be null");
            }


            return DeleteAnsible<string>(authenticationToken, $"{AnsibleTowerInstance.ApiDefinition.projects}/{projectId}", false);
        }
        #endregion

        #region Jobs
        /// <summary>
        /// Gets the Job list from Ansible Tower
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
        public Task<PaginatedResultDto<GetJobResponseDto>> GetJobs(string authenticationToken, string searchCriteria = null)
        {
            return GetAnsible<PaginatedResultDto<GetJobResponseDto>>(authenticationToken, AnsibleTowerInstance.ApiDefinition.jobs , searchCriteria);
        }

        /// <summary>
        /// Cancels a Job in Ansible Tower
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <param name="idJob"></param>
        /// <returns></returns>
        public Task<string> CancelJob(string authenticationToken, int idJob)
        {
            return PostAnsible<string>(authenticationToken, $"{AnsibleTowerInstance.ApiDefinition.jobs}{idJob}/cancel/", null);
        }

        /// <summary>
        /// Gets if a Job from Ansible Tower can be cancelled
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <param name="idJob"></param>
        /// <returns></returns>
        public Task<GetCanCancelResponseDto> CanCancelJob(string authenticationToken, int idJob)
        {
            return GetAnsible<GetCanCancelResponseDto>(authenticationToken, $"{AnsibleTowerInstance.ApiDefinition.jobs}{idJob}/cancel/");
        }

        /// <summary>
        /// Gets the events from a Job in Ansible Tower
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <param name="idJob"></param>
        /// <returns></returns>
        public Task<PaginatedResultDto<GetJobEventsResponseDto>> GetJobEvents(string authenticationToken, int idJob)
        {
            return GetAnsible<PaginatedResultDto<GetJobEventsResponseDto>>(authenticationToken, $"{AnsibleTowerInstance.ApiDefinition.jobs}{idJob}/job_events/");
        }

        /// <summary>
        /// Gets if a Job can be scheduled
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <param name="jobId"></param>
        /// <returns></returns>
        public Task<GetCanCancelResponseDto> GetCanJobSchedule(string authenticationToken, int jobId)
        {
            return GetAnsible<GetCanCancelResponseDto>(authenticationToken, $"{AnsibleTowerInstance.ApiDefinition.jobs}{jobId}/create_schedule/");
        }
        #endregion

        #region JobTemplates

        /// <summary>
        /// Deletes a Project in Ansible Tower by Id
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <param name="credentialRequest"></param>
        /// <returns></returns>
        public Task<string> DeleteJobTemplate(string authenticationToken, string jobTemplateId)
        {
            if (string.IsNullOrEmpty(jobTemplateId))
            {
                throw new ArgumentException("The project id cannot be null");
            }

            return DeleteAnsible<string>(authenticationToken, $"{AnsibleTowerInstance.ApiDefinition.job_templates}/{jobTemplateId}");
        }

        #endregion

        #region Security
        /// <summary>
        /// Performs the ping command in Ansible Tower
        /// </summary>
        /// <returns></returns>
        public Task<PingResponseDto> Ping()
        {
            return HttpClientHandler.Send<PingResponseDto>(HttpMethod.Get, AnsibleTowerInstance.CircuitBreakerName, AnsibleTowerInstance.ApiDefinition.ping, null, MediaType.ApplicationJson);
        }

        /// <summary>
        /// Sets the internal Auth token variable value
        /// </summary>
        /// <param name="authenticationToken"></param>
        private async Task SetAutehnticationTokenAsync(string authenticationToken)
        {

            if (!string.IsNullOrEmpty(authenticationToken))
            {
                AuthToken = authenticationToken;
                return;
            }


            if (string.IsNullOrEmpty(AnsibleTowerInstance.Username) || string.IsNullOrEmpty(AnsibleTowerInstance.Password))
            {
                throw new AnsibleTowerUnauthorizedException("No Ansible Tower authorization credentials provided");
            }

            var credentials = await Login(AnsibleTowerInstance.Username, AnsibleTowerInstance.Password);

            AuthToken = credentials.token;
        }

        /// <summary>
        /// Creates the required headers to connect Ansible Tower
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private Dictionary<string, string> GetLoginHeaders(string userName, string password)
        {
            var authCredential = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{userName}:{password}"));
            return new Dictionary<string, string> { { "Authorization", $"Basic {authCredential}" } };
        }

        /// <summary>
        /// Gets the requiered headers to perform the Ansible tower API calls
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> GetAuthorizationHeaders()
        {
            return new Dictionary<string, string> { { "Authorization", $"Bearer {AuthToken}" } };
        }

        #endregion

        #region HttpMethods
        /// <summary>
        /// Performs the generic API Get call to Ansible Tower
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="authenticationToken"></param>
        /// <param name="endpoint"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
        private async Task<T> GetAnsible<T>(string authenticationToken, string endpoint, string searchCriteria = null)
        {
            await SetAutehnticationTokenAsync(authenticationToken);
            var searchCriteriaUri = (searchCriteria != null ? "?search=" + searchCriteria : string.Empty);
            return await HttpClientHandler.Send<T>(HttpMethod.Get, AnsibleTowerInstance.CircuitBreakerName, endpoint + searchCriteriaUri, null, MediaType.ApplicationJson, GetAuthorizationHeaders());
        }

        /// <summary>
        /// Performs the generic API Post call to Ansible Tower
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="authenticationToken"></param>
        /// <param name="endpoint"></param>
        /// <param name="dataToSend"></param>
        /// <returns></returns>
        private async Task<T> PostAnsible<T>(string authenticationToken, string endpoint, object dataToSend)
        {
            await SetAutehnticationTokenAsync(authenticationToken);
            return await HttpClientHandler.Send<T>(HttpMethod.Post, AnsibleTowerInstance.CircuitBreakerName, endpoint, dataToSend, MediaType.ApplicationJson, GetAuthorizationHeaders());
        }

        /// <summary>
        /// Performs the generic API Delete call to Ansible Tower
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="authenticationToken"></param>
        /// <param name="endpoint"></param>
        /// <param name="dataToSend"></param>
        /// <returns></returns>
        private async Task<T> DeleteAnsible<T>(string authenticationToken, string endpoint, bool useCamelCase = true)
        {
            await SetAutehnticationTokenAsync(authenticationToken);
            return await HttpClientHandler.Send<T>(HttpMethod.Delete, AnsibleTowerInstance.CircuitBreakerName, endpoint, null, MediaType.ApplicationJson, GetAuthorizationHeaders(),true, useCamelCase);
        }
        #endregion
    }
}
