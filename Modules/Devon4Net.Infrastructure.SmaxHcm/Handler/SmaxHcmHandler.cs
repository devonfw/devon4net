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

        public Task<CreateOfferingResponseDto> CreateOffering(CreateOfferingRequestDto createOfferingRequestDto)
        {
            var data = new CreateOfferingEntity
            {
                entity_type = BulkEntityConst.Offering,
                properties = new CreateOfferingProperties
                {
                    Description = createOfferingRequestDto.Description,
                    DisplayLabel = createOfferingRequestDto.DisplayLabel,
                    IsBundle = createOfferingRequestDto.IsBundle,
                    IsDefault = createOfferingRequestDto.IsDefault,
                    IsPopularity = createOfferingRequestDto.IsPopularity,
                    NumOfRequests = createOfferingRequestDto.NumOfRequests,
                    OfferingType = OfferingTypeConst.ServiceOffering,
                    RequireAssetInfo = createOfferingRequestDto.RequireAssetInfo,
                    Service = createOfferingRequestDto.Service,
                    Status = OfferingStatusConst.Active,
                    SubscriptionActionType = SubscriptionActionTypeConst.All
                }
            };

            var request = new CreateBulkOfferingRequestDto
            {
                operation = BulkOperationConst.Create,
                entities = new List<CreateOfferingEntity> { data }
            };

            return SendSmaxHcm<CreateOfferingResponseDto>(HttpMethod.Post, string.Format(SmaxHcmEndpointConst.CreateOffering, SmaxHcmOptions.TenantId), request);
        }

        public Task<object> UpdateOffering(UpdateOfferingDto updateOfferingDto)
        {

            if (updateOfferingDto == null || string.IsNullOrEmpty(updateOfferingDto.offeringId))
            {
                throw new ArgumentException(
                    "Please check the offering object properties. Object can not be null or empty");
            }

            var data = new UpdateOfferingRequest
            {
                providerId = string.IsNullOrEmpty(updateOfferingDto.providerId) ? SmaxHcmOptions.ProviderId : updateOfferingDto.providerId,
                service = updateOfferingDto.service,
                offeringDisplayName = updateOfferingDto.offeringDisplayName,
                offeringId = updateOfferingDto.offeringId
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
                entities = new List<ActivateOfferingEntity> { data },
                operation = BulkOperationConst.Update
            };

            return SendSmaxHcm<ActivateOfferingResponse>(HttpMethod.Post, string.Format(SmaxHcmEndpointConst.ActivateOffering, SmaxHcmOptions.TenantId), request);
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
        #endregion

        #region Request

        public Task<GetAllRequestDto> GetAllRequest()
        {
            return SendSmaxHcm<GetAllRequestDto>(HttpMethod.Get, string.Format(SmaxHcmEndpointConst.GetAllRequest, SmaxHcmOptions.TenantId));
        }

        /// <summary>
        /// sample: { "entity": { "entity_type": "Request", "properties": { "ImpactScope": "Enterprise", "Urgency": "NoDisruption", "RequestedByPerson": "10016", "RequestsOffering": "11428", "DisplayLabel": "Request from Postman", "Description": "<p>This is a request created from Postman</p>", "UserOptions": "{"complexTypeProperties":[{"properties":{"OptionSet5FCCC5624DDDBA17DA397224C5D4ED0B_c":{"Option55F063755603608CB8287224C5D426DE_c":true},"OptionSetB9C2FB0D304A86418C651492D37A5E72_c":{"Option90AB77B8232585150B951492D37AE403_c":true},"changedUserOptionsForSimulation":"EndDate&","PropertyamazonResourceProvider55F063755603608CB8287224C5D426DE_c":"8a809efe73146d590173147b4a954cfc","Propertyregion55F063755603608CB8287224C5D426DE_c":"eu-west-1","PropertyPlatformType55F063755603608CB8287224C5D426DE_c":"Amazon Linux#eu-west-1","Propertykeypair55F063755603608CB8287224C5D426DE_c":"FARM-1-0d7bdb2a","PropertyvpcId55F063755603608CB8287224C5D426DE_c":"eu-west-1#vpc-017f010990ed442ce","Propertyimage55F063755603608CB8287224C5D426DE_c":"ami-0093757e056f6fe96","PropertysubnetId55F063755603608CB8287224C5D426DE_c":"subnet-0dcd276d74eb84059"}}]}", "DataDomains": [ "Public" ], "StartDate": 1596664800000, "EndDate": 1599170400000, "RequestAttachments": "{"complexTypeProperties":[]}" } }, "operation": "CREATE" } 
        /// </summary>
        /// <param name="createNewRequestDto">The response of the API please check the status provided at "completion_status" node</param>
        /// <returns></returns>
        public Task<CreateRequestResponse> CreateRequest(CreateRequestPropertiesDto createNewRequestDto)
        {
            if (createNewRequestDto == null )
            {
                throw new ArgumentException(
                    "Please check the create NewRequest Dto object properties. Object can not be null or empty");
            }

            var data = new CreateRequestEntity
            {
                entity_type = BulkEntityConst.Request,
                properties = new CreateRequestProperties
                {
                    Description = createNewRequestDto.Description,
                    DisplayLabel = createNewRequestDto.DisplayLabel,
                    StartDate = (new TimeSpan(createNewRequestDto.StartDate.Ticks)).TotalMilliseconds,
                    EndDate = (new TimeSpan(createNewRequestDto.EndDate.Ticks)).TotalMilliseconds,
                    RequestedByPerson = createNewRequestDto.RequestedByPerson,
                    RequestsOffering = createNewRequestDto.RequestsOffering,
                    ImpactScope = BulkImpactScopeConst.Enterprise,
                    Urgency = BulkUrgencyConst.NoDisruption,
                    //UserOptions = createNewRequestDto.entity.properties.UserOptions != null ? createNewRequestDto.entity.properties.UserOptions : new UserOptionsDto{complexTypeProperties = new List<Complextypeproperty>()},
                    UserOptions = createNewRequestDto.UserOptions,
                    DataDomains = new List<string> {BulkDataDomainsConst.Public},
                    RequestAttachments = createNewRequestDto.RequestAttachments
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
