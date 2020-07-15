using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Devon4Net.Infrastructure.CircuitBreaker.Common.Enums;
using Devon4Net.Infrastructure.CircuitBreaker.Handler;
using Devon4Net.Infrastructure.Common.Options.SmaxHcm;
using Devon4Net.Infrastructure.SmaxHcm.Common;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Login;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Offering;
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

        public SmaxHcmHandler(IHttpClientHandler httpClientHandler,  IOptions<SmaxHcmOptions> smaxHcmOptions)
        {
            HttpClientHandler = httpClientHandler;
            SmaxHcmOptions = smaxHcmOptions?.Value ?? throw new ArgumentException("No SmaxHcm options provided");
        }

        #region Users

        public Task<GetUsersResponseDto> GetUsers(string authToken = null)
        {
            return SendSmaxHcm<GetUsersResponseDto>(HttpMethod.Get, SmaxHcmEndpointConst.Users, string.Empty, false, authToken);
        }

        public Task<SmaxGetUserResponseDto> GetUserById(string userId, string authToken = null)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("The userId can not be null");
            }

            return SendSmaxHcm<SmaxGetUserResponseDto>(HttpMethod.Get, string.Format(SmaxHcmEndpointConst.User, userId, DateTime.Now.Ticks.ToString()), string.Empty, false, authToken);
        }

        #endregion

        #region Tenants

        public Task<GetUserTenantsResponseDto> GetUserTenants(string userId, string authToken = null)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("The userId can not be null");
            }

            return SendSmaxHcm<GetUserTenantsResponseDto>(HttpMethod.Get, string.Format(SmaxHcmEndpointConst.UserTenants, DateTime.Now.Ticks.ToString(), userId), string.Empty, false, authToken);
        }

        #endregion

        #region Offerings

        public Task<GetOfferingsResponseDto> GetOfferings(string tenantId, string authToken = null)
        {
            if (string.IsNullOrEmpty(tenantId))
            {
                throw new ArgumentException("The tenantId can not be null");
            }

            return SendSmaxHcm<GetOfferingsResponseDto>(HttpMethod.Get, string.Format(SmaxHcmEndpointConst.Offerings, tenantId), string.Empty, false, authToken);
        }

        public Task<GetOfferingResponseDto> GetOffering(string tenantId, string offeringId, string authToken = null)
        {
            if (string.IsNullOrEmpty(tenantId))
            {
                throw new ArgumentException("The tenantId can not be null");
            }

            if (string.IsNullOrEmpty(offeringId))
            {
                throw new ArgumentException("The offeringId can not be null");
            }

            return SendSmaxHcm<GetOfferingResponseDto>(HttpMethod.Get, string.Format(SmaxHcmEndpointConst.OfferingDetail, tenantId, offeringId), string.Empty, false, authToken);
        }

        //

        #endregion

        #region Security

        private async Task Login(string authToken = null)
        {
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

        public async Task<string> Login(string userName, string password)
        {
            AuthToken = await HttpClientHandler.Send<string>(HttpMethod.Post, SmaxHcmOptions.CircuitBreakerName, SmaxHcmEndpointConst.Logon, new LoginRequestDto {Login = userName, Password = password},MediaType.ApplicationJson);
            return AuthToken;
        }

        #endregion

        #region HttpMethods
        private async Task<T> SendSmaxHcm<T>(HttpMethod httpMethod, string endpoint, string tenantId, bool useCamelCase, string authToken = null)
        {
            await Login(authToken);
            return await HttpClientHandler.Send<T>(httpMethod, SmaxHcmOptions.CircuitBreakerName, GetUrlWithTenant(endpoint, tenantId), null, MediaType.ApplicationJson, GetAuthorizationHeaders(), true, useCamelCase);
        }

        private string GetUrlWithTenant(string originalUrl, string tenantId)
        {
            return string.IsNullOrEmpty(tenantId) ? originalUrl : $"{originalUrl}&TENANTID={tenantId}";
        }

        private Dictionary<string, string> GetAuthorizationHeaders()
        {
            return new Dictionary<string, string>
            {
                {"Cookie", $"{SmaxHcmEndpointConst.AuthorizationHeaderTokenkey}={AuthToken}"}
            };
        }
        #endregion
    }
}
