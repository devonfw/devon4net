using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Devon4Net.Infrastructure.CircuitBreaker.Common.Enums;
using Devon4Net.Infrastructure.CircuitBreaker.Handler;
using Devon4Net.Infrastructure.Common.Options.SmaxHcm;
using Devon4Net.Infrastructure.Log;
using Devon4Net.Infrastructure.SmaxHcm.Common;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Catalog;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Designer;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Login;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Offering;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Providers;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Tenants;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Users;
using Devon4Net.Infrastructure.SmaxHcm.Exceptions;
using Microsoft.Extensions.Options;

namespace Devon4Net.Infrastructure.SMAXHCM.Handler
{
    public class SmaxHcmHandler : ISmaxHcmHandler
    {
        private IHttpClientHandler HttpClientHandler { get; }
        private SmaxHcmOptions SmaxHcmOptions { get; }
        private string AuthToken { get; set; }
        private string TenantId { get; set; }

        public SmaxHcmHandler(IHttpClientHandler httpClientHandler,  IOptions<SmaxHcmOptions> smaxHcmOptions)
        {
            HttpClientHandler = httpClientHandler;
            SmaxHcmOptions = smaxHcmOptions?.Value ?? throw new ArgumentException("No SmaxHcm options provided");
        }



        #region Designer

        public Task<GetDesignResponseDto> GetDesign(string tenantId, string designId, string authToken = null)
        {
            if (string.IsNullOrEmpty(designId))
            {
                throw new ArgumentException("The designId can not be null");
            }

            SetTenantId(tenantId);

            return SendSmaxHcm<GetDesignResponseDto>(HttpMethod.Get, string.Format(SmaxHcmEndpointConst.GetDesign, TenantId, designId), string.Empty, null, false, authToken);
        }

        #endregion

        #region Tenants

        public Task<GetUserTenantsResponseDto> GetUserTenants(string userId, string authToken = null)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("The userId can not be null");
            }

            return SendSmaxHcm<GetUserTenantsResponseDto>(HttpMethod.Get, string.Format(SmaxHcmEndpointConst.UserTenants, DateTime.Now.Ticks.ToString(), userId), string.Empty, null,false, authToken);
        }

        #endregion

        #region Offerings

        public Task<GetOfferingsResponseDto> GetOfferings(string tenantId, string authToken = null)
        {
            SetTenantId(tenantId);

            return SendSmaxHcm<GetOfferingsResponseDto>(HttpMethod.Get, string.Format(SmaxHcmEndpointConst.Offerings, tenantId), string.Empty, null, false, authToken);
        }

        public Task<GetOfferingResponseDto> GetOffering(string tenantId, string offeringId, string authToken = null)
        {
            SetTenantId(tenantId);

            if (string.IsNullOrEmpty(offeringId))
            {
                throw new ArgumentException("The offeringId can not be null");
            }

            return SendSmaxHcm<GetOfferingResponseDto>(HttpMethod.Get, string.Format(SmaxHcmEndpointConst.OfferingDetail, tenantId, offeringId), string.Empty, null, false, authToken);
        }
        #endregion

        #region Providers

        public Task<GetProvidersResponseDto> GetProviders(string tenantId, string authToken)
        {
            SetTenantId(tenantId);
            return SendSmaxHcm<GetProvidersResponseDto>(HttpMethod.Get, string.Format(SmaxHcmEndpointConst.Providers, TenantId), TenantId, null, false, authToken);
        }

        #endregion

        #region Users

        public Task<GetUsersResponseDto> GetUsers(string authToken = null)
        {
            return SendSmaxHcm<GetUsersResponseDto>(HttpMethod.Get, SmaxHcmEndpointConst.Users, string.Empty, null, false, authToken);
        }

        public Task<SmaxGetUserResponseDto> GetUserById(string userId, string authToken = null)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("The userId can not be null");
            }

            return SendSmaxHcm<SmaxGetUserResponseDto>(HttpMethod.Get, string.Format(SmaxHcmEndpointConst.User, userId, DateTime.Now.Ticks.ToString()), string.Empty, null, false, authToken);
        }

        #endregion

        #region Security

        public async Task<string> Login(string userName, string password)
        {
            AuthToken = await HttpClientHandler.Send<string>(HttpMethod.Post, SmaxHcmOptions.CircuitBreakerName, SmaxHcmEndpointConst.Logon, new LoginRequestDto {Login = userName, Password = password}, MediaType.ApplicationJson);
            return AuthToken;
        }

        #endregion

        #region HttpMethods and security
        private async Task<T> SendSmaxHcm<T>(HttpMethod httpMethod, string endpoint, string tenantId, object content = null, bool useCamelCase = false, string authToken = null, bool getUrlWithTenant = false)
        {
            await PerformLogin(authToken, tenantId);
            return await HttpClientHandler.Send<T>(httpMethod, SmaxHcmOptions.CircuitBreakerName, getUrlWithTenant ? GetUrlWithTenant(endpoint) : endpoint, content, MediaType.ApplicationJson, GetAuthorizationHeaders(), true, useCamelCase);
        }

        private string GetUrlWithTenant(string originalUrl)
        {
            return string.IsNullOrEmpty(TenantId) ? originalUrl : $"{originalUrl}&TENANTID={TenantId}";
        }

        private Dictionary<string, string> GetAuthorizationHeaders()
        {
            return new Dictionary<string, string>
            {
                {"Cookie", $"{SmaxHcmEndpointConst.AuthorizationHeaderTokenkey}={AuthToken};TENANTID={SmaxHcmOptions.TenantId}"}
            };
        }

        private async Task PerformLogin(string authToken = null, string tenantId = null)
        {
            SetTenantId(tenantId);

            if (!string.IsNullOrEmpty(authToken))
            {
                AuthToken = authToken;
                return;
            }

            if (SmaxHcmOptions == null || string.IsNullOrEmpty(SmaxHcmOptions.UserName) || string.IsNullOrEmpty(SmaxHcmOptions.Password))
            {
                throw new SmaxHcmUnauthorizedException("No Smax authorization credentials provided");
            }

            AuthToken = await Login(SmaxHcmOptions.UserName, SmaxHcmOptions.Password).ConfigureAwait(false);
        }

        private void SetTenantId(string tenantId = null)
        {
            TenantId = !string.IsNullOrEmpty(tenantId) ? tenantId : SmaxHcmOptions.TenantId;
            Devon4NetLogger.Information($"Using TenanId : {TenantId}");
        }
        #endregion

        #region Catalog

        public Task<object> GetCatalogProviders(string category, bool includeArticles, bool includeOfferings, string query, string authToken = null, string tenantId = null)
        {
            var data = new QueryCatalogRequest
            {
                categoryId = category,
                includeArticles = includeArticles,
                includeOfferings = includeOfferings,
                searchQuery = query ?? string.Empty
            };

            return SendSmaxHcm<Object>(HttpMethod.Post, string.Format(SmaxHcmEndpointConst.GetCatalogFeaturedProviders, TenantId), TenantId, data, false, authToken);
        }

        public Task<GetOfferingsResponseDto> GetServiceDefinitions(string authToken = null, string tenantId = null)
        {
            return SendSmaxHcm<GetOfferingsResponseDto>(HttpMethod.Get, string.Format(SmaxHcmEndpointConst.GetServiceDefinitions, TenantId), TenantId, null,false, authToken);
        }

        public Task<object> CreateNewOffering(CreateOfferingDto createOfferingDto, string authToken = null, string tenantId = null)
        {
            var data = new CreateOfferingRequest
            {
                providerId = string.IsNullOrEmpty(createOfferingDto.providerId) ? SmaxHcmOptions.ProviderId : createOfferingDto.providerId,
                service = createOfferingDto.service,
                offeringDisplayName = createOfferingDto.offeringDisplayName,
                offeringId = string.IsNullOrEmpty(createOfferingDto.offeringId) ? Guid.NewGuid().ToString() : createOfferingDto.offeringId
            };

            return SendSmaxHcm<Object>(HttpMethod.Post, string.Format(SmaxHcmEndpointConst.CreateNewOffering, TenantId), TenantId, data, false, authToken);
        }
        #endregion
    }
}
