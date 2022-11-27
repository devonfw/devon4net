using Devon4Net.Infrastructure.CircuitBreaker.Constants;
using Devon4Net.Infrastructure.CircuitBreaker.Interfaces;
using Devon4Net.Infrastructure.Common.Constants;
using Devon4Net.Infrastructure.Common.Helpers;
using Devon4Net.Infrastructure.Common.Helpers.Interfaces;
using Devon4Net.Infrastructure.Common;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Net.Sockets;
using System.Text;

namespace Devon4Net.Infrastructure.CircuitBreaker.Handlers
{
    public class HttpRequestFromContextHandler : IHttpRequestFromContextHandler
    {
        private IJsonHelper JsonHelper { get; }

        public HttpRequestFromContextHandler()
{
            JsonHelper = new JsonHelper();
        }

        public HttpRequestFromContextHandler(IJsonHelper jsonHelper)
        {
            JsonHelper = jsonHelper;
        }

        public HttpRequestMessage GetHttpRequestMessageFromContext(HttpContext context, string uriDestination, bool addForwardedHeaders, object content = null, string contentMediaType = MediaType.ApplicationJson, bool contentAsJson = true, bool contentUseCamelCase = false)
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
                Devon4NetLogger.Error(ex);
                throw;
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
    }
}
