using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Devon4Net.Infrastructure.Common.Exceptions;
using Devon4Net.Infrastructure.Log;

namespace Devon4Net.Infrastructure.CircuitBreaker.Handler
{
    public class CircuitBreakerHttpClient : ICircuitBreakerHttpClient
    {
        private IHttpClientFactory HttpClientFactory { get; set; }

        public CircuitBreakerHttpClient(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
        }

        public async Task<string> Get(string endPointName, string url, Dictionary<string,string> headers = null)
        {
            HttpClient httpClient = null;
            HttpResponseMessage httpResponseMessage = null;
            string result;

            try
            {
                using (httpClient = GetDefaultClient(endPointName, headers))
                {
                    httpResponseMessage = await httpClient.GetAsync(GetEncodedUrl(httpClient.BaseAddress.ToString(), url)).ConfigureAwait(false);
                    result = await ManageHttpResponse(httpResponseMessage, endPointName);
                }
            }
            catch (Exception ex)
            {
                LogException(ref ex);
                throw;
            }
            finally
            {
                DisposeHttpObjects(ref httpClient, ref httpResponseMessage);
            }

            return result;
        }

        public async Task<T> Get<T>(string endPointName, string url, Dictionary<string, string> headers = null)
        {
            HttpClient httpClient = null;
            HttpResponseMessage httpResponseMessage = null;
            T result;

            try
            {
                using (httpClient = GetDefaultClient(endPointName, headers))
                {
                    
                    httpResponseMessage = await httpClient.GetAsync(GetEncodedUrl(httpClient.BaseAddress.ToString(), url)).ConfigureAwait(false);
                    var httpResult = await ManageHttpResponse(httpResponseMessage, endPointName);
                    result = Deserialize<T>(httpResult);
                }
            }
            catch (Exception ex)
            {
                LogException(ref ex);
                throw;
            }
            finally
            {
                DisposeHttpObjects(ref httpClient, ref httpResponseMessage);
            }

            return result;
        }

        public async Task<HttpResponseMessage> GetResponseMessage(string endPointName, string url, Dictionary<string, string> headers = null)
        {
            HttpResponseMessage httpResponseMessage;

            try
            {
                using var httpClient = GetDefaultClient(endPointName, headers);
                httpResponseMessage = await httpClient.GetAsync(GetEncodedUrl(httpClient.BaseAddress.ToString(), url)).ConfigureAwait(false);
                await LogHttpResponseAsync(httpResponseMessage).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                LogException(ref ex);
                throw;
            }

            return httpResponseMessage;
        }

        public async Task<Stream> GetAsStream(string endPointName, string url, Dictionary<string,string> headers = null)
        {
            Stream result;
            HttpClient httpClient = null;
            HttpResponseMessage httpResponseMessage = null;
            try
            {
                using (httpClient = GetDefaultClient(endPointName, headers))
                {
                    httpResponseMessage = await httpClient.GetAsync(GetEncodedUrl(httpClient.BaseAddress.ToString(), url)).ConfigureAwait(false);
                    result = await ManageHttpResponseAsStream(httpResponseMessage, endPointName);
                }
            }
            catch (Exception ex)
            {
                LogException(ref ex);
                throw;
            }
            finally
            {
                DisposeHttpObjects(ref httpClient, ref httpResponseMessage);
            }

            return result;
        }
        
        public async Task<T> PostJson<T>(string endPointName, string url, string jsonDataToSend, string mediaType, Dictionary<string,string> headers = null)
        {
            T result;
            HttpClient httpClient = null;
            HttpResponseMessage httpResponseMessage = null;

            try
            {
                using (httpClient = GetDefaultClient(endPointName, headers))
                {
                    httpResponseMessage = await httpClient.PostAsync(GetEncodedUrl(httpClient.BaseAddress.ToString(), url), new StringContent(jsonDataToSend, Encoding.UTF8, mediaType)).ConfigureAwait(false);
                    var httpResult = await ManageHttpResponse(httpResponseMessage, endPointName);
                    result = Deserialize<T>(httpResult);
                }
            }
            catch (Exception ex)
            {
                LogException(ref ex);
                throw;
            }
            finally
            {
                DisposeHttpObjects(ref httpClient, ref httpResponseMessage);
            }

            return result;
        }        

        public async Task<T> Post<T>(string endPointName, string url, object dataToSend, string mediaType, Dictionary<string,string> headers = null)
        {
            T result;
            HttpClient httpClient = null;
            HttpContent httpContent = null;
            HttpResponseMessage httpResponseMessage = null;

            try
            {
                using (httpClient = GetDefaultClient(endPointName, headers))
                {
                    httpContent = CreateJsonHttpContent(dataToSend, mediaType);

                    httpResponseMessage = await httpClient.PostAsync(GetEncodedUrl(httpClient.BaseAddress.ToString(), url), httpContent).ConfigureAwait(false);
                    var httpResult = await ManageHttpResponse(httpResponseMessage, endPointName, httpContent);
                    result = Deserialize<T>(httpResult);
                }
            }
            catch (Exception ex)
            {
                LogException(ref ex);
                throw;
            }
            finally
            {
                DisposeHttpObjects(ref httpClient, ref httpResponseMessage, ref httpContent);
            }

            return result;
        }

        public async Task<T> Put<T>(string endPointName, string url, object dataToSend, string mediaType, Dictionary<string,string> headers = null)
        {
            T result;
            HttpClient httpClient = null;
            HttpContent httpContent = null;
            HttpResponseMessage httpResponseMessage = null;

            try
            {
                using (httpClient = GetDefaultClient(endPointName, headers))
                {
                    httpContent = CreateJsonHttpContent(dataToSend, mediaType);
                    httpResponseMessage = await httpClient.PutAsync(GetEncodedUrl(httpClient.BaseAddress.ToString(), url), httpContent).ConfigureAwait(false);
                    var httpResult = await ManageHttpResponse(httpResponseMessage, endPointName);

                    result = Deserialize<T>(httpResult);
                }
            }
            catch (Exception ex)
            {
                LogException(ref ex);
                throw;
            }
            finally
            {
                DisposeHttpObjects(ref httpClient, ref httpResponseMessage, ref httpContent);
            }

            return result;

        }

        public async Task<string> Delete(string endPointName, string url, Dictionary<string,string> headers = null)
        {
            HttpClient httpClient = null;
            HttpResponseMessage httpResponseMessage = null;
            string result;

            try
            {
                using (httpClient = GetDefaultClient(endPointName, headers))
                {
                    httpResponseMessage = await httpClient.DeleteAsync(GetEncodedUrl(httpClient.BaseAddress.ToString(), url)).ConfigureAwait(false);
                    result = await ManageHttpResponse(httpResponseMessage, endPointName);
                }
            }
            catch (Exception ex)
            {
                LogException(ref ex);
                throw;
            }
            finally
            {
                DisposeHttpObjects(ref httpClient, ref httpResponseMessage);
            }

            return result;
        }

        public async Task<HttpResponseMessage> Patch(string endPointName, string url, HttpContent content, Dictionary<string,string> headers = null)
        {
            HttpClient httpClient = null;
            HttpResponseMessage result;
            try
            {
                var method = new HttpMethod("PATCH");

                using (httpClient = GetDefaultClient(endPointName, headers))
                {
                    var request = new HttpRequestMessage(method, GetEncodedUrl(httpClient.BaseAddress.ToString(), url))
                    {
                        Content = content
                    };

                    result = await httpClient.SendAsync(request).ConfigureAwait(false);
                    await LogHttpResponseAsync(result).ConfigureAwait(false);
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

            return result;
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

        private HttpContent CreateJsonHttpContent<T>(T requestContent, string mediaType)
        {
            if (requestContent == null) return null;
            var requestBody = Serialize(requestContent);
            HttpContent httpContent = new StringContent(requestBody);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
            return httpContent;
        }

        private void LogException(ref Exception exception)
        {
            Devon4NetLogger.Error(exception);
        }

        private string Serialize(object toPrint)
        {
            return JsonSerializer.Serialize(toPrint);
        }

        private T Deserialize<T>(string input)
        {
            return JsonSerializer.Deserialize<T>(input);
        }

        private async Task LogHttpResponseAsync(HttpResponseMessage httpResponseMessage, HttpContent httpContent = null)
        {
            if (httpResponseMessage != null)
            {
                Devon4NetLogger.Information($" HttpRequest :{httpResponseMessage.RequestMessage} | httpResponse: {httpResponseMessage}");
            }

            if (httpContent != null)
            {
                Devon4NetLogger.Information($" HttpRequestBody :{await httpContent.ReadAsStringAsync().ConfigureAwait(false)}");
            }
        }

        private async Task<string> ManageHttpResponse(HttpResponseMessage httpResponseMessage, string endPointName, HttpContent httpContent = null)
        {
            await CheckHttpResponse(httpResponseMessage, endPointName, httpContent);

            return await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        private async Task CheckHttpResponse(HttpResponseMessage httpResponseMessage, string endPointName, HttpContent httpContent)
        {
            await LogHttpResponseAsync(httpResponseMessage, httpContent).ConfigureAwait(false);

            if (httpResponseMessage == null)
            {
                throw new HttpRequestException($"The httprequest to {endPointName} was not successful.");
            }

            if (httpResponseMessage != null && !httpResponseMessage.IsSuccessStatusCode)
            {
                throw new HttpCustomRequestException($"The httprequest to {endPointName} was not successful. HttpStatus Error: {httpResponseMessage.StatusCode} | {httpResponseMessage}", (int) httpResponseMessage.StatusCode);
            }
        }

        private async Task<Stream> ManageHttpResponseAsStream(HttpResponseMessage httpResponseMessage, string endPointName)
        {
            await LogHttpResponseAsync(httpResponseMessage);
            await CheckHttpResponse(httpResponseMessage, endPointName, null);

            if (httpResponseMessage == null || !httpResponseMessage.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"The httprequest to {endPointName} was not successful.");
            }

            return await httpResponseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false);
        }
        
        private void DisposeHttpObjects(ref HttpClient httpClient, ref HttpResponseMessage httpResponseMessage, ref HttpContent? httpContent)
        {
            //httpContent?.Dispose();
            //DisposeHttpObjects(ref httpClient, ref httpResponseMessage);
        }

        private void DisposeHttpObjects(ref HttpClient httpClient, ref HttpResponseMessage httpResponseMessage)
        {
            //httpClient?.Dispose();
            //httpResponseMessage?.Dispose();
        }

        private string GetEncodedUrl(string baseAddress, string endPoint)
        {
            var result = string.Empty;

            if (string.IsNullOrEmpty(baseAddress)) throw new ArgumentException("The base address to perform the circuitbreaker call can not be null or empty");

            if (baseAddress.EndsWith("/") && endPoint.StartsWith("/"))
            {
                result = baseAddress + endPoint.Substring(1);
            }

            if (!baseAddress.EndsWith("/") && !endPoint.StartsWith("/"))
            {
                result = $"{baseAddress}/{endPoint}";
            }

            return Uri.EscapeUriString(result);
        }
    }
}