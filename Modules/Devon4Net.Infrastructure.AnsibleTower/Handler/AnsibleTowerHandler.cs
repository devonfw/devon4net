using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Devon4Net.Infrastructure.AnsibleTower.Common;
using Devon4Net.Infrastructure.AnsibleTower.Dto;
using Devon4Net.Infrastructure.AnsibleTower.Dto.Applications;
using Devon4Net.Infrastructure.AnsibleTower.Dto.Inventories;
using Devon4Net.Infrastructure.AnsibleTower.Dto.JobTemplates;
using Devon4Net.Infrastructure.AnsibleTower.Dto.Organizations;
using Devon4Net.Infrastructure.AnsibleTower.Exceptions;
using Devon4Net.Infrastructure.CircuitBreaker.Common.Enums;
using Devon4Net.Infrastructure.CircuitBreaker.Handler;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Infrastructure.AnsibleTower.Handler
{
    public class AnsibleTowerHandler : IAnsibleTowerHandler
    {
        private ICircuitBreakerHttpClient CircuitBreakerHttpClient { get; }
        private IAnsibleTowerInstance AnsibleTowerInstance { get; }
        private string AuthToken { get; set; }

        public AnsibleTowerHandler(IAnsibleTowerInstance ansibleTowerInstance, ICircuitBreakerHttpClient circuitBreakerHttpClient)
        {
            CircuitBreakerHttpClient = circuitBreakerHttpClient;
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
            var result = await CircuitBreakerHttpClient.Post<LoginRequestDto>(AnsibleTowerInstance.CircuitBreakerName, AnsibleTowerInstance.ApiDefinition.tokens, null, MediaType.ApplicationJson, GetLoginHeaders(userName, password), true).ConfigureAwait(false);
            AuthToken = result.Token;

            return result;
        }

        #endregion

        #region Applications

        public async Task<ApplicationsResponseDto> CreateApplication(ApplicationsRequestDto applicationstRequest, string authenticationToken)
        {
            return await PostAnsible<ApplicationsResponseDto>(authenticationToken,AnsibleTowerInstance.ApiDefinition.applications, applicationstRequest).ConfigureAwait(false);
        }
        public async Task<GetApplicationsResponseDto> GetApplications(string authenticationToken)
        {
            return await GetAnsible<GetApplicationsResponseDto>(authenticationToken, AnsibleTowerInstance.ApiDefinition.applications, null).ConfigureAwait(false);
        }

        #endregion

        #region Organizations

        public async Task<OrganizationsResponseDto> GetOrganizations(string authenticationToken)
        {
            return await GetAnsible<OrganizationsResponseDto>(authenticationToken, AnsibleTowerInstance.ApiDefinition.organizations, null).ConfigureAwait(false);
        }

        public async Task<ResultOrganizationDto> GetOrganizationById(string authenticationToken, string organizationId)
        {
            return await GetAnsible<ResultOrganizationDto>(authenticationToken, $"{AnsibleTowerInstance.ApiDefinition.organizations}{organizationId}/", null).ConfigureAwait(false);
        }

        public async Task<ResultOrganizationDto> CreateOrganization(string authenticationToken, CreateOrganizationRequestDto organizationRequest)
        {
            return await PostAnsible<ResultOrganizationDto>(authenticationToken, AnsibleTowerInstance.ApiDefinition.organizations, organizationRequest).ConfigureAwait(false);
        }
        #endregion

        #region Inventories
        public async Task<GetInventoriesResponseDto> GetInventories(string authenticationToken)
        {
            return await GetAnsible<GetInventoriesResponseDto>(authenticationToken,AnsibleTowerInstance.ApiDefinition.inventory, null).ConfigureAwait(false);
        }

        public async Task<ResultInventoryDto> GetInventoryById(string authenticationToken, string inventoryId)
        {
            return await GetAnsible<ResultInventoryDto>(authenticationToken, $"{AnsibleTowerInstance.ApiDefinition.inventory}{inventoryId}/", null).ConfigureAwait(false);
        }

        public async Task<ResultInventoryDto> PostInventories(string authenticationToken, CreateInventoryRequestDto inventoryRequest)
        {
            return await PostAnsible<ResultInventoryDto>(authenticationToken, AnsibleTowerInstance.ApiDefinition.inventory, inventoryRequest).ConfigureAwait(false);
        }



        #endregion

        #region JobTemplates
        public async Task<GetJobTemplatesResponseDto> GetJobTemplates(string authenticationToken)
        {
            return await GetAnsible<GetJobTemplatesResponseDto>(authenticationToken, AnsibleTowerInstance.ApiDefinition.job_templates, null).ConfigureAwait(false);
        }

        public async Task<ResultJobDto> GetJobTemplate(string authenticationToken, string jobTemplateId)
        {
            return await GetAnsible<ResultJobDto>(authenticationToken, $"{AnsibleTowerInstance.ApiDefinition.job_templates}{jobTemplateId}/", null).ConfigureAwait(false);
        }

        public async Task<ResultJobDto> CreateJobTemplate(string authenticationToken, CreateJobTemplateRequestDto createJobTemplateRequest)
        {
            return await PostAnsible<ResultJobDto>(authenticationToken, AnsibleTowerInstance.ApiDefinition.job_templates, createJobTemplateRequest).ConfigureAwait(false);
        }

        #endregion

        #region Security

        private void SetAutehnticationToken(string authenticationToken)
        {
            if (string.IsNullOrEmpty(authenticationToken))
            {
                throw new AnsibleTowerUnauthorizedException("No authorization token provided");
            }

            AuthToken = authenticationToken;
        }

        private Dictionary<string, string> GetLoginHeaders(string userName, string password)
        {
            var authCredential = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{userName}:{password}"));
            return new Dictionary<string, string> { { "Authorization", $"Basic {authCredential}" } };
        }

        private Dictionary<string, string> GetAuthorizationHeaders()
        {
            return new Dictionary<string, string> { { "Authorization", $"Bearer {AuthToken}" } };
        }

        #endregion

        #region HttpMethods

        private async Task<T> GetAnsible<T>(string authenticationToken, string endpoint, object input)
        {
            SetAutehnticationToken(authenticationToken);
            return await CircuitBreakerHttpClient.Get<T>(AnsibleTowerInstance.CircuitBreakerName, endpoint, GetAuthorizationHeaders(), true).ConfigureAwait(false);
        }

        private async Task<T> PostAnsible<T>(string authenticationToken, string endpoint, object dataToSend)
        {
            SetAutehnticationToken(authenticationToken);
            return await CircuitBreakerHttpClient.Post<T>(AnsibleTowerInstance.CircuitBreakerName, endpoint, dataToSend, MediaType.ApplicationJson, GetAuthorizationHeaders(), true).ConfigureAwait(false);
        }

        #endregion
    }
}
