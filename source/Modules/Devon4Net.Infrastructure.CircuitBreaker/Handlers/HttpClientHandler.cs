using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using Devon4Net.Infrastructure.CircuitBreaker.Common.Enums;
using Devon4Net.Infrastructure.CircuitBreaker.Constants;
using Devon4Net.Infrastructure.Common.Exceptions;
using Devon4Net.Infrastructure.Extensions.Helpers;
using Devon4Net.Infrastructure.Log;
using Microsoft.AspNetCore.Http;

namespace Devon4Net.Infrastructure.CircuitBreaker.Handlers
{
    public class HttpClientHandler : IHttpClientHandler
    {
        private IHttpClientFactory HttpClientFactory { get; set; }
        private IJsonHelper JsonHelper { get; }
        private const string SoapAction = "SOAPAction";

        public HttpClientHandler(IHttpClientFactory httpClientFactory, IJsonHelper jsonHelper)
        {
            HttpClientFactory = httpClientFactory;
            JsonHelper = jsonHelper;
        }

        public HttpClientHandler(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
            JsonHelper = new JsonHelper();
        }

        public async Task<HttpResponseMessage> Send(HttpMethod httpMethod, string endPointName, string url, object content, string mediaType, bool contentAsJson = true, bool useCamelCase = false, Dictionary<string, string> headers = null)
        {
            try
            {
                return await SendCommand(httpMethod, endPointName, url, content, mediaType, headers, contentAsJson, useCamelCase).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                LogException(ref ex);
                throw;
            }
        }

        public async Task<Stream> Send(HttpMethod httpMethod, string endPointName, string url, object content, string mediaType, Dictionary<string, string> headers = null, bool contentAsJson = true, bool useCamelCase = false)
        {
            try
            {
                using var httpResponseMessage = await SendCommand(httpMethod, endPointName, url, content, mediaType, headers, contentAsJson, useCamelCase).ConfigureAwait(false);
                return await ManageHttpResponseAsStream(httpResponseMessage, endPointName).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                LogException(ref ex);
                throw;
            }
        }

        public async Task<T> Send<T>(HttpMethod httpMethod, string endPointName, string url, object content, string mediaType, Dictionary<string, string> headers = null, bool contentAsJson = true, bool useCamelCase = false)
        {
            try
            {
                using var httpResponseMessage = await SendCommand(httpMethod, endPointName, url, content, mediaType, headers, contentAsJson, useCamelCase).ConfigureAwait(false);
                var httpResult = await ManageHttpResponse(httpResponseMessage, endPointName).ConfigureAwait(false);
                var result = JsonHelper.Deserialize<T>(httpResult, useCamelCase);
                if (result != null) return result;

                throw new BadHttpRequestException("The request could not be performed. A null object was obtained after the deserialization process. Please check the request and the type of result mapping object");
            }
            catch (Exception ex)
            {
                LogException(ref ex);
                throw;
            }
        }

        public async Task<string> SendSoapRequest(string endPointName, string url, string soapMessage, string soapActionUrl = null, Dictionary<string, string> headers = null)
        {
            try
            {
                var soapHeaders = AddSoapHeaders(headers, soapActionUrl);
                using var httpResponseMessage = await SendCommand(HttpMethod.Post, endPointName, url, soapMessage, MediaType.TextXml, soapHeaders, false).ConfigureAwait(false);
                return await ManageHttpResponse(httpResponseMessage, endPointName).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                LogException(ref ex);
                throw;
            }
        }

        public async Task<T> SendFormRequest<T>(string endPointName, string url, IEnumerable<KeyValuePair<string, string>> dataToSend, Dictionary<string, string> headers = null, string mediaType = MediaType.ApplicationXwww, bool useCamelCase = false)
        {
            try
            {
                using var dataForm = new FormUrlEncodedContent(dataToSend);
                using var httpResponseMessage = await SendCommand(HttpMethod.Post, endPointName, url, dataForm, mediaType, headers, false).ConfigureAwait(false);
                var httpResult = await ManageHttpResponse(httpResponseMessage, endPointName).ConfigureAwait(false);
                var result = JsonHelper.Deserialize<T>(httpResult, useCamelCase);
                if (result != null) return result;

                throw new BadHttpRequestException("The request could not be performed. A null object was obtained after the deserialization process. Please check the request and the type of result mapping object");
            }
            catch (Exception ex)
            {
                LogException(ref ex);
                throw;
            }
        }

        public async Task<T> SendFormRequest<T>(string endPointName, string url, IFormCollection dataToSend, Dictionary<string, string> headers = null, string mediaType = MediaType.ApplicationXwww, bool useCamelCase = false)
        {
            try
            {
                using var httpResponseMessage = await SendCommand(HttpMethod.Post, endPointName, url, dataToSend, mediaType, headers, false).ConfigureAwait(false);
                var httpResult = await ManageHttpResponse(httpResponseMessage, endPointName).ConfigureAwait(false);
                var result = JsonHelper.Deserialize<T>(httpResult, useCamelCase);
                if (result != null) return result;

                throw new BadHttpRequestException("The request could not be performed. A null object was obtained after the deserialization process. Please check the request and the type of result mapping object");
            }
            catch (Exception ex)
            {
                LogException(ref ex);
                throw;
            }
        }

        /// <summary>
        /// Redirects the request with to the new endpoint. If you want to change the content of the request, put it on the content field
        /// </summary>
        /// <param name="endPointName">name for the connection. You can set a random name here</param>
        /// <param name="sourceContext">Pass the current HTTPContext that has the request information</param>
        /// <param name="uriDestination">The final url</param>
        /// <param name="addForwardedHeaders"></param>
        /// <param name="content">Only needid if the body content is going to be changed</param>
        /// <param name="contentMediaType">only needed if the new content is not null</param>
        /// <param name="contentAsJson">only needed if the new content is not null</param>
        /// <param name="contentUseCamelCase">only needed if the new content is not null</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> RedirectRequest(string endPointName, HttpContext sourceContext, string uriDestination, bool addForwardedHeaders = false, object content = null, string contentMediaType = MediaType.ApplicationJson, bool contentAsJson = true, bool contentUseCamelCase = false)
        {
            HttpResponseMessage httpResponseMessage;

            try
            {
                using var httpClient = GetDefaultClient(endPointName, null);
                var request = GetHttpRequestMessageFromContext(sourceContext, uriDestination, addForwardedHeaders, content, contentMediaType, contentAsJson, contentUseCamelCase);
                httpResponseMessage = await httpClient.SendAsync(request).ConfigureAwait(false);
                await LogHttpResponse(httpResponseMessage, endPointName).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                LogException(ref ex);
                throw;
            }

            return httpResponseMessage;
        }

        #region private methods

        private static Dictionary<string, string> AddSoapHeaders(Dictionary<string, string> headers, string soapActionUrl)
        {
            if (string.IsNullOrEmpty(soapActionUrl)) return headers;

            if (headers == null) return new Dictionary<string, string> { { SoapAction, soapActionUrl } };

            var result = new Dictionary<string, string>(headers);

            if (!result.ContainsKey(SoapAction))
            {
                result.Add(SoapAction, soapActionUrl);
            }

            return result;
        }

        private async Task<HttpResponseMessage> SendCommand(HttpMethod httpMethod, string endPointName, string url, object content, string mediaType = MediaType.ApplicationJson, Dictionary<string, string> headers = null, bool contentAsJson = true, bool useCamelCase = false) //NOSONAR false positive
        {
            HttpResponseMessage httpResponseMessage;

            try
            {
                var method = new HttpMethod(httpMethod.ToString());

                using var httpClient = GetDefaultClient(endPointName, headers);

                var request = new HttpRequestMessage(method, GetEncodedUrl(httpClient.BaseAddress?.ToString(), url));

                if (content != null)
                {
                    if (content is IFormCollection)
                    {
                        using var data = ToHttpContent(content as IFormCollection, mediaType);
                        request.Content = content as HttpContent;
                    }
                    else
                    {
                        request.Content = CreateHttpContent(content, mediaType, contentAsJson, useCamelCase);
                    }
                }

                httpResponseMessage = await httpClient.SendAsync(request).ConfigureAwait(false);
                await LogHttpResponse(httpResponseMessage, endPointName).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                LogException(ref ex);
                throw;
            }

            return httpResponseMessage;
        }

        private HttpClient GetDefaultClient(string endPointName, Dictionary<string, string> headers = null)
        {
            var httpClient = HttpClientFactory.CreateClient(endPointName);

            if (headers?.Any() != true) return httpClient;

            foreach (var (key, value) in headers)
            {
                httpClient.DefaultRequestHeaders.Add(key, value);
            }

            return httpClient;
        }

        private HttpContent CreateHttpContent<T>(T requestContent, string mediaType, bool contentAsJson, bool useCamelCase)
        {
            if (requestContent == null) return null;

            var requestBody = contentAsJson ? JsonHelper.Serialize(requestContent, useCamelCase) : requestContent.ToString();

            HttpContent httpContent = new StringContent(requestBody);

            if (mediaType != null)
            {
                httpContent.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
            }

            return httpContent;
        }

        private static void LogException(ref Exception exception)
        {
            Devon4NetLogger.Error(exception);
        }

        private static async Task CheckHttpResponse(HttpResponseMessage httpResponseMessage, string endPointName)
        {
            await LogHttpResponse(httpResponseMessage, endPointName).ConfigureAwait(false);

            if (httpResponseMessage == null)
            {
                throw new HttpRequestException($"The http request to {endPointName} was not successful.");
            }

            if (httpResponseMessage?.IsSuccessStatusCode == false)
            {
                var responseResult = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                throw new HttpCustomRequestException($"The http request to {endPointName} was not successful. HttpStatus Error: {(int)httpResponseMessage.StatusCode} | {responseResult}", (int)httpResponseMessage.StatusCode);
            }
        }

        private static async Task<string> ManageHttpResponse(HttpResponseMessage httpResponseMessage, string endPointName)
        {
            await CheckHttpResponse(httpResponseMessage, endPointName).ConfigureAwait(false);

            return await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        private static async Task<Stream> ManageHttpResponseAsStream(HttpResponseMessage httpResponseMessage, string endPointName)
        {
            await CheckHttpResponse(httpResponseMessage, endPointName).ConfigureAwait(false);

            if (httpResponseMessage == null)
            {
                throw new HttpRequestException($"The Http Request to {endPointName} was not successful.");
            }

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return await httpResponseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false);
            }

            var messageResult = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            throw new HttpRequestException($"The Http Request to {endPointName} was not successful: {messageResult}");
        }

        private static async Task LogHttpResponse(HttpResponseMessage httpResponseMessage, string endPointName)
        {
            if (httpResponseMessage == null)
            {
                Devon4NetLogger.Information($"HttpResponse message for endpoint call {endPointName} is null");
                return;
            }

            var contentResult = httpResponseMessage.IsSuccessStatusCode
                ? "The Http response is success"
                : await LogHttpContent(httpResponseMessage.Content, endPointName).ConfigureAwait(false);

            Devon4NetLogger.Information($"HttpResponse message for endpoint call {endPointName} : HttpRequest :{httpResponseMessage.RequestMessage} | httpResponse: {httpResponseMessage} | message Content: {contentResult}");
        }

        private static async Task<string> LogHttpContent(HttpContent httpContent, string endPointName)
        {
            if (httpContent == null)
            {
                return $"HttpContent for {endPointName} is null";
            }

            var contentResult = await httpContent.ReadAsStringAsync().ConfigureAwait(false);
            return $"HttpContent for {endPointName}:{contentResult}";
        }

        private static string GetEncodedUrl(string baseAddress, string endPoint)
        {
            if (string.IsNullOrEmpty(baseAddress)) {
                Devon4NetLogger.Information("GetEncodedUrl method inoveed with empty baseAddres");
                return string.Empty;
            }

            var result = string.Empty;

            if (endPoint.Contains(baseAddress))
            {
                endPoint = endPoint.Replace(baseAddress, "/");
            }

            if (endPoint.Contains("//"))
            {
                endPoint = endPoint.Replace("//", "/");
            }

            if (baseAddress.EndsWith("/") && endPoint.StartsWith("/"))
            {
                result = string.Concat(baseAddress, endPoint.AsSpan(1));
            }

            if (!baseAddress.EndsWith("/") && !endPoint.StartsWith("/"))
            {
                result = $"{baseAddress}/{endPoint}";
            }

            if (!endPoint.StartsWith("/") || baseAddress.EndsWith("/") && !endPoint.StartsWith("/") || !baseAddress.EndsWith("/") && endPoint.StartsWith("/"))
            {
                result = $"{baseAddress}{endPoint}";
            }

            return Uri.EscapeDataString(result);
        }

        private HttpRequestMessage GetHttpRequestMessageFromContext(HttpContext context, string uriDestination, bool addForwardedHeaders, object content = null, string contentMediaType = MediaType.ApplicationJson, bool contentAsJson = true, bool contentUseCamelCase = false)
        {
            try
            {
                var uri = new Uri(uriDestination);
                var request = context.Request;
                var requestMessage = new HttpRequestMessage();
                var requestMethod = request.Method;
                var usesStreamContent = SetupContent(content, contentMediaType, contentAsJson, contentUseCamelCase, request, requestMessage, requestMethod);

                ManageHeaders(context, addForwardedHeaders, request, requestMessage, usesStreamContent);

                requestMessage.Headers.Host = uri.Authority;
                requestMessage.RequestUri = uri;
                requestMessage.Method = new HttpMethod(requestMethod);

                return requestMessage;
            }
            catch (Exception ex)
            {
                LogException(ref ex);
                throw;
            }
        }

        private static void ManageHeaders(HttpContext context, bool addForwardedHeaders, HttpRequest request, HttpRequestMessage requestMessage, bool usesStreamContent)
        {
            foreach (var header in request.Headers)
            {
                if (!usesStreamContent && (header.Key.Equals("Content-Type", StringComparison.OrdinalIgnoreCase) || header.Key.Equals("Content-Length", StringComparison.OrdinalIgnoreCase)))
                    continue;
                if (!requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray()))
                    requestMessage.Content?.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
            }

            if (addForwardedHeaders)
            {
                AddForwardedHeadersToHttpRequest(context, requestMessage);
            }
        }

        private bool SetupContent(object content, string contentMediaType, bool contentAsJson, bool contentUseCamelCase, HttpRequest request, HttpRequestMessage requestMessage, string requestMethod)
        {
            if (!HttpMethods.IsGet(requestMethod) && !HttpMethods.IsHead(requestMethod) && !HttpMethods.IsDelete(requestMethod) && !HttpMethods.IsTrace(requestMethod))
            {
                if (request.HasFormContentType)
                {
                    var dataToSend = content != null ? content as IFormCollection : request.Form;
                    requestMessage.Content = ToHttpContent(dataToSend, request.ContentType);
                    return false;
                }
                else
                {
                    requestMessage.Content = content != null ? CreateHttpContent(content, contentMediaType, contentAsJson, contentUseCamelCase) : new StreamContent(request.Body);
                }
            }

            return true;
        }

        private static void AddForwardedHeadersToHttpRequest(HttpContext context, HttpRequestMessage requestMessage)
        {
            var request = context.Request;
            var connection = context.Connection;

            var host = request.Host.ToString();
            var protocol = request.Scheme;

            var localIp = connection.LocalIpAddress?.ToString();
            var isLocalIpV6 = connection.LocalIpAddress?.AddressFamily == AddressFamily.InterNetworkV6;

            var remoteIp = context.Connection.RemoteIpAddress?.ToString();
            var isRemoteIpV6 = connection.RemoteIpAddress?.AddressFamily == AddressFamily.InterNetworkV6;

            if (remoteIp != null)
            {
                requestMessage.Headers.TryAddWithoutValidation(CircuitBreakerConsts.XForwardedFor, remoteIp);
            }

            requestMessage.Headers.TryAddWithoutValidation(CircuitBreakerConsts.XForwardedProto, protocol);
            requestMessage.Headers.TryAddWithoutValidation(CircuitBreakerConsts.XForwardedHost, host);

            var forwardedHeader = new StringBuilder($"proto={protocol};host={host};");

            if (localIp != null)
            {
                if (isLocalIpV6)
                    localIp = $"\"[{localIp}]\"";

                forwardedHeader.Append("by=").Append(localIp).Append(';');
            }

            if (remoteIp != null)
            {
                if (isRemoteIpV6)
                    remoteIp = $"\"[{remoteIp}]\"";

                forwardedHeader.Append("for=").Append(remoteIp).Append(';');
            }

            requestMessage.Headers.TryAddWithoutValidation(CircuitBreakerConsts.Forwarded, forwardedHeader.ToString());
        }

        private static HttpContent ToHttpContent(IFormCollection collection, string contentTypeHeader)
        {
            var contentType = MediaTypeHeaderValue.Parse(contentTypeHeader);

            if (contentType.MediaType.Equals(MediaType.ApplicationXwww, StringComparison.OrdinalIgnoreCase)) // specification: https://url.spec.whatwg.org/#concept-urlencoded
                return new FormUrlEncodedContent(collection.SelectMany(formItemList => formItemList.Value.Select(value => new KeyValuePair<string, string>(formItemList.Key, value))));

            if (!contentType.MediaType.Equals(MediaType.MultipartFormData, StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException($"Unknown form content type `{contentType.MediaType}`.");

            var delimiter = contentType.Parameters.Single(p => p.Name.Equals("boundary", StringComparison.OrdinalIgnoreCase)).Value.Trim('"');

            var multipart = new MultipartFormDataContent(delimiter);
            foreach (var formVal in collection)
            {
                foreach (var value in formVal.Value)
                    multipart.Add(new StringContent(value), formVal.Key);
            }
            foreach (var file in collection.Files)
            {
                var content = new StreamContent(file.OpenReadStream());
                foreach (var header in file.Headers)
                    content.Headers.TryAddWithoutValidation(header.Key, (IEnumerable<string>)header.Value);
                multipart.Add(content, file.Name, file.FileName);
            }
            return multipart;
        }
        #endregion
    }
}
