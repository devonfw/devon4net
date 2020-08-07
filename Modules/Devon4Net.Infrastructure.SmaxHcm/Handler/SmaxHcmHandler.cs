using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Devon4Net.Infrastructure.CircuitBreaker.Common.Enums;
using Devon4Net.Infrastructure.CircuitBreaker.Handler;
using Devon4Net.Infrastructure.Common.Options.SmaxHcm;
using Devon4Net.Infrastructure.SmaxHcm.Common;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Catalog;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Designer;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Login;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Offering;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Providers;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Request.CreateRequest;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Request.GetRequest;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Request.UserOptions;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Tenants;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Users;
using Devon4Net.Infrastructure.SmaxHcm.Exceptions;
using Microsoft.Extensions.Options;
using Properties = Devon4Net.Infrastructure.SmaxHcm.Dto.Offering.Properties;

namespace Devon4Net.Infrastructure.SMAXHCM.Handler
{
    public class SmaxHcmHandler : ISmaxHcmHandler
    {
        private IHttpClientHandler HttpClientHandler { get; }
        private SmaxHcmOptions SmaxHcmOptions { get; }
        private string AuthToken { get; set; }
        private string BasicAuthToken { get; set; }

        public SmaxHcmHandler(IHttpClientHandler httpClientHandler,  IOptions<SmaxHcmOptions> smaxHcmOptions)
        {
            HttpClientHandler = httpClientHandler;
            SmaxHcmOptions = smaxHcmOptions?.Value ?? throw new ArgumentException("No SmaxHcm options provided");
        }

        #region Designer

        public Task<GetDesignResponseDto> GetDesign(string designId)
        {
            if (string.IsNullOrEmpty(designId))
            {
                throw new ArgumentException("The designId can not be null");
            }

            return SendSmaxHcm<GetDesignResponseDto>(HttpMethod.Get, string.Format(SmaxHcmEndpointConst.GetDesign, SmaxHcmOptions.TenantId, designId), null, false, true);
        }
        #endregion

        #region Tenants

        public Task<GetUserTenantsResponseDto> GetUserTenants(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("The userId can not be null");
            }

            return SendSmaxHcm<GetUserTenantsResponseDto>(HttpMethod.Get, string.Format(SmaxHcmEndpointConst.UserTenants, DateTime.Now.Ticks.ToString(), userId));
        }

        #endregion

        #region Offerings

        public Task<GetOfferingsResponseDto> GetOfferings()
        {
            return SendSmaxHcm<GetOfferingsResponseDto>(HttpMethod.Get, string.Format(SmaxHcmEndpointConst.Offerings, SmaxHcmOptions.TenantId));
        }

        public Task<GetOfferingResponseDto> GetOffering(string offeringId)
        {
            if (string.IsNullOrEmpty(offeringId))
            {
                throw new ArgumentException("The offeringId can not be null");
            }

            return SendSmaxHcm<GetOfferingResponseDto>(HttpMethod.Get, string.Format(SmaxHcmEndpointConst.OfferingDetail, SmaxHcmOptions.TenantId, offeringId));
        }
        #endregion

        #region Providers

        public Task<GetProvidersResponseDto> GetProviders()
        {
            return SendSmaxHcm<GetProvidersResponseDto>(HttpMethod.Get, string.Format(SmaxHcmEndpointConst.Providers, SmaxHcmOptions.TenantId), null, false, true);
        }
        #endregion

        #region Users
        public Task<GetUsersResponseDto> GetUsers()
        {
            return SendSmaxHcm<GetUsersResponseDto>(HttpMethod.Get, SmaxHcmEndpointConst.Users);
        }

        public Task<SmaxGetUserResponseDto> GetUserById(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("The userId can not be null");
            }

            return SendSmaxHcm<SmaxGetUserResponseDto>(HttpMethod.Get, string.Format(SmaxHcmEndpointConst.User, userId, DateTime.Now.Ticks.ToString()));
        }

        #endregion

        #region Security

        public async Task<string> Login(string userName, string password)
        {
            BasicAuthToken = Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(userName + ":" + password));
            AuthToken = await HttpClientHandler.Send<string>(HttpMethod.Post, SmaxHcmOptions.CircuitBreakerName, string.Format(SmaxHcmEndpointConst.Logon, SmaxHcmOptions.TenantId), new LoginRequestDto {Login = userName, Password = password}, MediaType.ApplicationJson);
            return AuthToken;
        }
        #endregion

        #region HttpMethods and security
        private async Task<T> SendSmaxHcm<T>(HttpMethod httpMethod, string endpoint, object content = null,  bool getUrlWithTenant = false, bool addBasicAuth = false, bool useCamelCase = false)
        {
            await PerformLogin();
            return await HttpClientHandler.Send<T>(httpMethod, SmaxHcmOptions.CircuitBreakerName, getUrlWithTenant ? GetUrlWithTenant(endpoint) : endpoint, content, MediaType.ApplicationJson, GetAuthorizationHeaders(addBasicAuth), true, useCamelCase);
        }

        private string GetUrlWithTenant(string originalUrl)
        {
            return string.IsNullOrEmpty(SmaxHcmOptions.TenantId) ? originalUrl : $"{originalUrl}&TENANTID={SmaxHcmOptions.TenantId}";
        }

        /// <summary>
        /// Please check that some SMAX endpoint sometimes need the basic Athentication
        /// </summary>
        /// <param name="addBasicAuthentication">Flag to set the basic authentication in the header's request</param>
        /// <returns></returns>
        private Dictionary<string, string> GetAuthorizationHeaders(bool addBasicAuthentication)
        {
            var result = new Dictionary<string, string>
            {
                {"Cookie", $"{SmaxHcmEndpointConst.AuthorizationHeaderTokenkey}={AuthToken};TENANTID={SmaxHcmOptions.TenantId}"}
            };

            if (addBasicAuthentication)
            {
                result.Add("Authorization", $"Basic {BasicAuthToken}");
            }

            return result;
        }

        private async Task PerformLogin()
        {
            if (SmaxHcmOptions == null || string.IsNullOrEmpty(SmaxHcmOptions.UserName) || string.IsNullOrEmpty(SmaxHcmOptions.Password))
            {
                throw new SmaxHcmUnauthorizedException("No Smax authorization credentials provided");
            }

            AuthToken = await Login(SmaxHcmOptions.UserName, SmaxHcmOptions.Password).ConfigureAwait(false);
        }
        #endregion

        #region Catalog

        public Task<object> GetCatalogProviders(string category, bool includeArticles, bool includeOfferings, string query)
        {
            var data = new QueryCatalogRequest
            {
                categoryId = category,
                includeArticles = includeArticles,
                includeOfferings = includeOfferings,
                searchQuery = query ?? string.Empty
            };

            return SendSmaxHcm<Object>(HttpMethod.Post, string.Format(SmaxHcmEndpointConst.GetCatalogFeaturedProviders, SmaxHcmOptions.TenantId), data);
        }

        public Task<GetOfferingsResponseDto> GetServiceDefinitions()
        {
            return SendSmaxHcm<GetOfferingsResponseDto>(HttpMethod.Get, string.Format(SmaxHcmEndpointConst.GetServiceDefinitions, SmaxHcmOptions.TenantId));
        }

        public Task<object> CreateNewOffering(CreateOfferingDto createOfferingDto)
        {
            var data = new CreateOfferingRequest
            {
                providerId = string.IsNullOrEmpty(createOfferingDto.providerId) ? SmaxHcmOptions.ProviderId : createOfferingDto.providerId,
                service = createOfferingDto.service,
                offeringDisplayName = createOfferingDto.offeringDisplayName,
                offeringId = string.IsNullOrEmpty(createOfferingDto.offeringId) ? Guid.NewGuid().ToString() : createOfferingDto.offeringId
            };

            return SendSmaxHcm<Object>(HttpMethod.Post, string.Format(SmaxHcmEndpointConst.CreateNewOffering, SmaxHcmOptions.TenantId), data);
        }

        public Task<ActivateOfferingResponse> ActivateOffering(ActivateOfferingDto activateOfferingDto)
        {
            var data = new ActivateOfferingEntity
            {
                entity_type = BulkEntityConst.Offering,
                properties = new Properties()
                {
                    Id = activateOfferingDto.offeringId,
                    Status = OfferingStatusConst.Active
                }
            };

            var request = new ActivateOfferingRequest()
            {
                entities = new List<ActivateOfferingEntity>{data},
                operation = BulkOperationConst.Update
            };

            return SendSmaxHcm<ActivateOfferingResponse>(HttpMethod.Post, string.Format(SmaxHcmEndpointConst.ActivateOffering, SmaxHcmOptions.TenantId), request);
        }
        #endregion

        #region Request

        public Task<GetAllRequestDto> GetAllRequest()
        {
            return SendSmaxHcm<GetAllRequestDto>(HttpMethod.Get, string.Format(SmaxHcmEndpointConst.GetAllRequest, SmaxHcmOptions.TenantId));
        }

        public Task<CreateRequestResponse> CreateRequest(CreateNewRequestDto createNewRequestDto)
        {
            if (createNewRequestDto == null || createNewRequestDto.entity == null ||
                string.IsNullOrEmpty(createNewRequestDto.operation))
            {
                throw new ArgumentException(
                    "Please check the create NewRequest Dto object properties. Object can not be null or empty");
            }

            var data = new CreateRequestEntity
            {
                entity_type = BulkEntityConst.Request,
                properties = new CreateRequestProperties
                {
                    Description = createNewRequestDto.entity.properties.Description,
                    DisplayLabel = createNewRequestDto.entity.properties.DisplayLabel,
                    StartDate = createNewRequestDto.entity.properties.StartDate,
                    EndDate = createNewRequestDto.entity.properties.EndDate,
                    RequestedByPerson = createNewRequestDto.entity.properties.RequestedByPerson,
                    RequestsOffering = createNewRequestDto.entity.properties.RequestsOffering,
                    ImpactScope = BulkImpactScopeConst.Enterprise,
                    Urgency = BulkUrgencyConst.NoDisruption,
                    //UserOptions = createNewRequestDto.entity.properties.UserOptions != null ? createNewRequestDto.entity.properties.UserOptions : new UserOptionsDto{complexTypeProperties = new List<Complextypeproperty>()},
                    UserOptions = createNewRequestDto.entity.properties.UserOptions,
                    DataDomains = new List<string> {BulkDataDomainsConst.Public},
                    RequestAttachments = createNewRequestDto.entity.properties.RequestAttachments
                }
            };

            var request = new CreateRequestDto
            {
                operation = BulkOperationConst.Create,
                entities = new List<CreateRequestEntity> {data}
            };

            return SendSmaxHcm<CreateRequestResponse>(HttpMethod.Post, string.Format(SmaxHcmEndpointConst.CreateRequest, SmaxHcmOptions.TenantId), request);

        }

        #endregion
    }
}
