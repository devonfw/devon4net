using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Devon4Net.Infrastructure.CircuitBreaker.Common.Enums;
using Devon4Net.Infrastructure.Common.Common;
using Devon4Net.Infrastructure.Common.Exceptions;
using Devon4Net.Infrastructure.Log;

namespace Devon4Net.Infrastructure.CircuitBreaker.Handler
{
    public class HttpClientHandler : IHttpClientHandler
    {
        private IHttpClientFactory HttpClientFactory { get; set; }
        private IBuiltInTypes BuiltInTypes { get; set; }
        private const string SoapAction = "SOAPAction";
        private static readonly JsonSerializerOptions CamelJsonSerializerOptions = new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase, IgnoreNullValues = true};

        public HttpClientHandler(IHttpClientFactory httpClientFactory, IBuiltInTypes builtInTypes)
        {
            HttpClientFactory = httpClientFactory;
            BuiltInTypes = builtInTypes;
        }

        public async Task<HttpResponseMessage> Send(HttpMethod httpMethod, string endPointName, string url, object content, string mediaType, bool contentAsJson = true, bool useCamelCase = false, Dictionary<string, string> headers = null)
        {
            try
            {
                return await SendCommand(httpMethod, endPointName, url, content, mediaType, headers, contentAsJson, useCamelCase);
            }
            catch (Exception ex)
            {
                LogException(ref ex);
                throw;
            }
        }

        public async Task<Stream> Send(HttpMethod httpMethod, string endPointName, string url, object content, string mediaType, Dictionary<string, string> headers = null, bool contentAsJson = true, bool useCamelCase = false)
        {
            HttpResponseMessage httpResponseMessage = null;

            try
            {
                httpResponseMessage = await SendCommand(httpMethod, endPointName, url, content, mediaType, headers, contentAsJson, useCamelCase);
                return await ManageHttpResponseAsStream(httpResponseMessage, endPointName);
            }
            catch (Exception ex)
            {
                LogException(ref ex);
                throw;
            }
            finally
            {
                httpResponseMessage?.Dispose();
            }
        }

        public async Task<T> Send<T>(HttpMethod httpMethod, string endPointName, string url, object content, string mediaType, Dictionary<string, string> headers = null, bool contentAsJson = true, bool useCamelCase = false)
        {
            HttpResponseMessage httpResponseMessage = null;
            try
            {
                httpResponseMessage = await SendCommand(httpMethod, endPointName, url, content, mediaType, headers, contentAsJson, useCamelCase);
                var httpResult = await ManageHttpResponse(httpResponseMessage, endPointName);
                return Deserialize<T>(httpResult, useCamelCase);
            }
            catch (Exception ex)
            {
                LogException(ref ex);
                throw;
            }
            finally
            {
                httpResponseMessage?.Dispose();
            }
        }

        public async Task<string> SendSoapRequest(string endPointName, string url, string soapMessage, string soapActionUrl = null, Dictionary<string, string> headers = null)
        {
            var soapHeaders = AddSoapHeaders(headers, soapActionUrl);
            var httpResponse = await SendCommand(HttpMethod.Post, endPointName, url, soapMessage, MediaType.TextXml, soapHeaders, false);
            return await ManageHttpResponse(httpResponse, endPointName);
        }

        #region private methods

        private Dictionary<string, string> AddSoapHeaders(Dictionary<string, string> headers, string soapActionUrl)
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

        private async Task<HttpResponseMessage> SendCommand(HttpMethod httpMethod, string endPointName, string url, object content, string mediaType = MediaType.ApplicationJson, Dictionary<string, string> headers = null, bool contentAsJson = true, bool useCamelCase = false)
        {
            HttpClient httpClient = null;
            HttpResponseMessage httpResponseMessage;

            try
            {
                var method = new HttpMethod(httpMethod.ToString());

                using (httpClient = GetDefaultClient(endPointName, headers))
                {
                    var request = new HttpRequestMessage(method, GetEncodedUrl(httpClient.BaseAddress.ToString(), url));

                    if (content != null)
                    {
                        request.Content = CreateHttpContent(content, mediaType, contentAsJson, useCamelCase);
                    }

                    httpResponseMessage = await httpClient.SendAsync(request).ConfigureAwait(false);
                    await LogHttpResponse(httpResponseMessage, endPointName).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                LogException(ref ex);
                throw;
            }
            finally
            {
                httpClient?.Dispose();
            }

            return httpResponseMessage;
        }

        private HttpClient GetDefaultClient(string endPointName, Dictionary<string, string> headers = null)
        {
            var httpClient = HttpClientFactory.CreateClient(endPointName);

            if (headers == null || !headers.Any()) return httpClient;

            foreach (var (key, value) in headers)
            {
                httpClient.DefaultRequestHeaders.Add(key, value);
            }

            return httpClient;
        }

        private HttpContent CreateHttpContent<T>(T requestContent, string mediaType, bool contentAsJson, bool useCamelCase)
        {
            if (requestContent == null) return null;

            var requestBody = contentAsJson ? Serialize(requestContent, useCamelCase) : requestContent.ToString(); /*SerializeToXml(requestContent);*/

            HttpContent httpContent = new StringContent(requestBody);
            
            if (mediaType != null)
            {
                httpContent.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
            }

            return httpContent;
        }

        private void LogException(ref Exception exception)
        {
            Devon4NetLogger.Error(exception);
        }

        private string Serialize(object toPrint, bool useCamelCase)
        {
            return JsonSerializer.Serialize(toPrint, useCamelCase ? CamelJsonSerializerOptions : null);
        }

        private T Deserialize<T>(string input, bool useCamelCase)
        {
            return string.IsNullOrEmpty(input)
                ? default
                : BuiltInTypes.GetBuiltInTypeObjecNames().Contains(typeof(T).Name)  ? (T)Convert.ChangeType(input, typeof(T)) : JsonSerializer.Deserialize<T>(input, useCamelCase ? CamelJsonSerializerOptions : null);
        }

        private async Task CheckHttpResponse(HttpResponseMessage httpResponseMessage, string endPointName)
        {
            await LogHttpResponse(httpResponseMessage, endPointName);

            if (httpResponseMessage == null)
            {
                throw new HttpRequestException($"The http request to {endPointName} was not successful.");
            }

            if (httpResponseMessage != null && !httpResponseMessage.IsSuccessStatusCode)
            {
                var responseResult = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                throw new HttpCustomRequestException($"The http request to {endPointName} was not successful. HttpStatus Error: {(int) httpResponseMessage.StatusCode} | {responseResult}", (int) httpResponseMessage.StatusCode);
            }
        }

        private async Task<string> ManageHttpResponse(HttpResponseMessage httpResponseMessage, string endPointName)
        {
            await CheckHttpResponse(httpResponseMessage, endPointName);

            return await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        private async Task<Stream> ManageHttpResponseAsStream(HttpResponseMessage httpResponseMessage, string endPointName)
        {
            await CheckHttpResponse(httpResponseMessage, endPointName);

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

        private async Task LogHttpResponse(HttpResponseMessage httpResponseMessage, string endPointName)
        {
            if (httpResponseMessage == null)
            {
                Devon4NetLogger.Information($"HttpResponse message for endpoint call {endPointName} is null");
                return;
            }

            var contentResult = httpResponseMessage.IsSuccessStatusCode
                ? "The Http response is success"
                : await LogHttpContent(httpResponseMessage.Content, endPointName);

            Devon4NetLogger.Information($"HttpResponse message for endpoint call {endPointName} : HttpRequest :{httpResponseMessage.RequestMessage} | httpResponse: {httpResponseMessage} | message Content: {contentResult}");
        }

        private async Task<string> LogHttpContent(HttpContent httpContent, string endPointName)
        {
            if (httpContent == null)
            {
                return $"HttpContent for {endPointName} is null";
            }

            var contentResult = await httpContent.ReadAsStringAsync().ConfigureAwait(false);
            return $"HttpContent for {endPointName}:{contentResult}";
        }

        private string GetEncodedUrl(string baseAddress, string endPoint)
        {
            var result = string.Empty;

            if (endPoint.Contains(baseAddress))
            {
                endPoint = endPoint.Replace(baseAddress, "/");
            }

            if (endPoint.Contains("//"))
            {
                endPoint = endPoint.Replace("//", "/");
            }

            if (string.IsNullOrEmpty(baseAddress))
            {
                throw new ArgumentException("The base address to perform the circuitbreaker call can not be null or empty");
            }

            if (baseAddress.EndsWith("/") && endPoint.StartsWith("/"))
            {
                result = baseAddress + endPoint.Substring(1);
            }

            if (!baseAddress.EndsWith("/") && !endPoint.StartsWith("/"))
            {
                result = $"{baseAddress}/{endPoint}";
            }

            if (!endPoint.StartsWith("/") || baseAddress.EndsWith("/") && !endPoint.StartsWith("/") || !baseAddress.EndsWith("/") && endPoint.StartsWith("/"))
            {
                result = $"{baseAddress}{endPoint}";
            }

            return Uri.EscapeUriString(result);
        }

        #endregion
    }
}
