using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Devon4Net.Infrastructure.CircuitBreaker.Common.Enums;
using Devon4Net.Infrastructure.CircuitBreaker.Handler;
using Devon4Net.Infrastructure.Common.Options.SmaxHcm;
using Devon4Net.Infrastructure.Log;
using Devon4Net.Infrastructure.SmaxHcm.Common;
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

            return SendSmaxHcm<GetDesignResponseDto>(HttpMethod.Get, string.Format(SmaxHcmEndpointConst.GetDesign, designId), string.Empty, false, authToken);
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
            SetTenantId(tenantId);

            return SendSmaxHcm<GetOfferingsResponseDto>(HttpMethod.Get, string.Format(SmaxHcmEndpointConst.Offerings, tenantId), string.Empty, false, authToken);
        }

        public Task<GetOfferingResponseDto> GetOffering(string tenantId, string offeringId, string authToken = null)
        {
            SetTenantId(tenantId);

            if (string.IsNullOrEmpty(offeringId))
            {
                throw new ArgumentException("The offeringId can not be null");
            }

            return SendSmaxHcm<GetOfferingResponseDto>(HttpMethod.Get, string.Format(SmaxHcmEndpointConst.OfferingDetail, tenantId, offeringId), string.Empty, false, authToken);
        }
        #endregion

        #region Providers

        public Task<GetProvidersResponseDto> GetProviders(string tenantId, string authToken)
        {
            SetTenantId(tenantId);
            return SendSmaxHcm<GetProvidersResponseDto>(HttpMethod.Get, string.Format(SmaxHcmEndpointConst.Providers, TenantId), TenantId, false, authToken);
        }

        #endregion

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

        #region Security

        /// <summary>
        /// This method does not work. Microfocus help needed.
        /// Steps:
        /// GET /idm-service/idm/v0/login?tenant=903361753&tryLocal=true HTTP/1.1
        /// GET /idm-service/idm/v0/api/public/tenant?id=903361753 HTTP/1.1
        /// GET /idm-service/idm/v0/api/public/token HTTP/1.1
        /// /idm-service/idm/v0/api/public/authenticate
        ///  /bo/postBoLogin?LWREQ=X_...
        /// GET /idm-service/idm/v0/api/public/token HTTP/1.1
        /// GET /bo/userProfile?timeStamp=1595004859963 HTTP/1.1
        /// userprofile obtains the needed XSRF-TOKEN, IDM_REFRESH_TOKEN, IDM_X_AUTH_TOKEN
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<string> CookieLogin(string tenantId, string userName, string password)
        {
            var headers = new Dictionary<string, string>
            {
                {"Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8"}
            };

            SetTenantId(tenantId);

            //1 - set bo login to get the cookies
            var boLogin = await HttpClientHandler.Send(HttpMethod.Post, SmaxHcmOptions.CircuitBreakerName, SmaxHcmEndpointConst.BoLogin, "returnContext=%2Fbo", MediaType.ApplicationXwww, false, false, headers);
            var boCookies = boLogin.Headers.FirstOrDefault(k => k.Key == "Set-Cookie").Value;
            var cookiesFormatted = string.Join(";", boCookies).Replace("Secure", string.Empty).Replace("HttpOnly", string.Empty).Replace("Path=/idm-service", string.Empty).Replace(" ", string.Empty).Replace(";;;;", ";").Replace(";;", ";");

            //2 - Call to token
            headers.Add("Cookie", cookiesFormatted);
            var tokenRequest = await HttpClientHandler.Send(HttpMethod.Get, SmaxHcmOptions.CircuitBreakerName, SmaxHcmEndpointConst.BoLoginToken, null, MediaType.ApplicationXwww, false, false, headers);
            headers.Add("Referer", tokenRequest.RequestMessage.RequestUri.OriginalString.Replace(tokenRequest.RequestMessage.RequestUri.PathAndQuery, string.Empty) + string.Format(SmaxHcmEndpointConst.LoginTenant, TenantId));

            //3 -User Login

            var userLoginContent = new UserLoginRequestDto { passwordCredentials = new Passwordcredentials{ password = password, username = userName}, tenantName = TenantId, token = tokenRequest.RequestMessage.RequestUri.AbsoluteUri + string.Format(SmaxHcmEndpointConst.LoginTenant, TenantId) };
            var userLogin = await HttpClientHandler.Send<AuthenticateResponseDto>(HttpMethod.Post, SmaxHcmOptions.CircuitBreakerName, SmaxHcmEndpointConst.UserLogin, userLoginContent, MediaType.ApplicationJson);

            //4 Authenticate
            var authenticateResponse = await Authenticate(tenantId, userName, password, cookiesFormatted).ConfigureAwait(false);

            //5 - Return URL from userLogin ???
            //var returnUrl = await HttpClientHandler.Send(HttpMethod.Get, SmaxHcmOptions.CircuitBreakerName, SmaxHcmEndpointConst.BoLoginToken, null, MediaType.ApplicationXwww, false, false, headers);

            headers.Clear();
            headers.Add("Upgrade-Insecure-Requests", "1");
            headers.Add("Connection", "keep-alive");
            headers.Add("Accept-Encoding", "gzip, deflate, br");
            headers.Add("Accept", "application/json, text/plain, */*");
            headers.Add("Cookie", authenticateResponse.CookieResult.FirstOrDefault(c=>c.Contains("LWSSO_COOKIE_KEY")));

            var PostLogin = await HttpClientHandler.Send(HttpMethod.Get, SmaxHcmOptions.CircuitBreakerName, authenticateResponse.AuthenticateResponseDto.returnUri.return_uri, null, MediaType.ApplicationXwww, true, false, headers);



            return string.Empty;
        }

        private async Task<ResultAuthenticateResponseDto> Authenticate(string tenantId, string userName, string password, string cookieValue = null)
        {
            var user = !string.IsNullOrEmpty(userName) ? userName : SmaxHcmOptions.UserName;
            var userPassword = !string.IsNullOrEmpty(password) ? password : SmaxHcmOptions.Password;
            SetTenantId(tenantId);

            var request = new AuthenticateRequestDto
            {
                passwordCredentials = new Passwordcredentials {username = user, password = userPassword }, tenantName = TenantId
            };

            var result = await HttpClientHandler.Send(HttpMethod.Post, SmaxHcmOptions.CircuitBreakerName,
                SmaxHcmEndpointConst.Authenticate, request, MediaType.ApplicationJson, true, false,
                new Dictionary<string, string> { { "Cookie", cookieValue } });
            
            var authenticateResponse = JsonSerializer.Deserialize<AuthenticateResponseDto>(await result.Content.ReadAsStringAsync().ConfigureAwait(false));
            return new ResultAuthenticateResponseDto {AuthenticateResponseDto = authenticateResponse, CookieResult = result.Headers.FirstOrDefault(k => k.Key == "Set-Cookie").Value.ToList() };
        }

        public async Task<string> Login(string userName, string password)
        {
            AuthToken = await HttpClientHandler.Send<string>(HttpMethod.Post, SmaxHcmOptions.CircuitBreakerName, SmaxHcmEndpointConst.Logon, new LoginRequestDto {Login = userName, Password = password}, MediaType.ApplicationJson);
            return AuthToken;
        }

        #endregion

        #region HttpMethods and security
        private async Task<T> SendSmaxHcm<T>(HttpMethod httpMethod, string endpoint, string tenantId, bool useCamelCase, string authToken = null, bool getUrlWithTenant = false)
        {
            await PerformLogin(authToken, tenantId);
            return await HttpClientHandler.Send<T>(httpMethod, SmaxHcmOptions.CircuitBreakerName, getUrlWithTenant ? GetUrlWithTenant(endpoint) : endpoint, null, MediaType.ApplicationJson, GetAuthorizationHeaders(), true, useCamelCase);
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
    }
}
