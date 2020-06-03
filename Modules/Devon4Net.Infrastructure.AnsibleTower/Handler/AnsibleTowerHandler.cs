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

        public Task<ApplicationsResponseDto> CreateApplication(ApplicationsRequestDto applicationstRequest, string authenticationToken)
        {
            return PostAnsible<ApplicationsResponseDto>(authenticationToken,AnsibleTowerInstance.ApiDefinition.applications, applicationstRequest);
        }
        public Task<GetApplicationsResponseDto> GetApplications(string authenticationToken)
        {
            return GetAnsible<GetApplicationsResponseDto>(authenticationToken, AnsibleTowerInstance.ApiDefinition.applications, null);
        }

        #endregion

        #region Organizations

        public Task<OrganizationsResponseDto> GetOrganizations(string authenticationToken)
        {
            return GetAnsible<OrganizationsResponseDto>(authenticationToken, AnsibleTowerInstance.ApiDefinition.organizations, null);
        }

        public Task<ResultOrganizationDto> GetOrganizationById(string authenticationToken, string organizationId)
        {
            return GetAnsible<ResultOrganizationDto>(authenticationToken, $"{AnsibleTowerInstance.ApiDefinition.organizations}{organizationId}/", null);
        }

        public Task<ResultOrganizationDto> CreateOrganization(string authenticationToken, CreateOrganizationRequestDto organizationRequest)
        {
            return PostAnsible<ResultOrganizationDto>(authenticationToken, AnsibleTowerInstance.ApiDefinition.organizations, organizationRequest);
        }
        #endregion

        #region Inventories
        public Task<GetInventoriesResponseDto> GetInventories(string authenticationToken)
        {
            return GetAnsible<GetInventoriesResponseDto>(authenticationToken,AnsibleTowerInstance.ApiDefinition.inventory, null);
        }

        public Task<ResultInventoryDto> GetInventoryById(string authenticationToken, string inventoryId)
        {
            return GetAnsible<ResultInventoryDto>(authenticationToken, $"{AnsibleTowerInstance.ApiDefinition.inventory}{inventoryId}/", null);
        }

        public Task<ResultInventoryDto> PostInventories(string authenticationToken, CreateInventoryRequestDto inventoryRequest)
        {
            return PostAnsible<ResultInventoryDto>(authenticationToken, AnsibleTowerInstance.ApiDefinition.inventory, inventoryRequest);
        }



        #endregion

        #region JobTemplates
        public Task<GetJobTemplatesResponseDto> GetJobTemplates(string authenticationToken)
        {
            return GetAnsible<GetJobTemplatesResponseDto>(authenticationToken, AnsibleTowerInstance.ApiDefinition.job_templates, null);
        }

        public Task<ResultJobDto> GetJobTemplate(string authenticationToken, string jobTemplateId)
        {
            return GetAnsible<ResultJobDto>(authenticationToken, $"{AnsibleTowerInstance.ApiDefinition.job_templates}{jobTemplateId}/", null);
        }

        public Task<ResultJobDto> CreateJobTemplate(string authenticationToken, CreateJobTemplateRequestDto createJobTemplateRequest)
        {
            return PostAnsible<ResultJobDto>(authenticationToken, AnsibleTowerInstance.ApiDefinition.job_templates, createJobTemplateRequest);
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

        private Task<T> GetAnsible<T>(string authenticationToken, string endpoint, object input)
        {
            SetAutehnticationToken(authenticationToken);
            return CircuitBreakerHttpClient.Get<T>(AnsibleTowerInstance.CircuitBreakerName, endpoint, GetAuthorizationHeaders(), true);
        }

        private Task<T> PostAnsible<T>(string authenticationToken, string endpoint, object dataToSend)
        {
            SetAutehnticationToken(authenticationToken);
            return CircuitBreakerHttpClient.Post<T>(AnsibleTowerInstance.CircuitBreakerName, endpoint, dataToSend, MediaType.ApplicationJson, GetAuthorizationHeaders(), true);
        }

        #endregion
    }
}
