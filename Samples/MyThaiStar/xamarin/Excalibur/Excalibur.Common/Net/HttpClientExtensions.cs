using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

// ReSharper disable once CheckNamespace
namespace System.Net.Http
{
    /// <summary>
    /// Various Extensions on the <see cref="HttpClient"/> mainly anything to do with Json
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// <see cref="JsonSerializerSettings"/> that are used when converting
        /// </summary>
        public static JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };

        public static Task<HttpResponseMessage> PostContentAsJsonAsync<T>(this HttpClient client, string requestUrl, T requestContent)
        {
            return client.PostContentAsJsonAsync(new Uri(requestUrl), requestContent, CancellationToken.None);
        }

        public static Task<HttpResponseMessage> PostContentAsJsonAsync<T>(this HttpClient client, string requestUrl, T requestContent, CancellationToken cancellationToken)
        {
            return client.PostContentAsJsonAsync(new Uri(requestUrl), requestContent, cancellationToken);
        }

        public static Task<HttpResponseMessage> PostContentAsJsonAsync<T>(this HttpClient client, Uri requestUri, T requestContent)
        {
            return client.PostContentAsJsonAsync(requestUri, requestContent, CancellationToken.None);
        }

        public static Task<HttpResponseMessage> PostContentAsJsonAsync<T>(this HttpClient client, Uri requestUri, T requestContent, CancellationToken cancellationToken)
        {
            var httpContent = CreateJsonHttpContent(requestContent);

            return client.PostAsync(requestUri, httpContent, cancellationToken);
        }

        public static Task<HttpResponseMessage> PutContentAsJsonAsync<T>(this HttpClient client, string requestUrl, T requestContent)
        {
            return client.PutContentAsJsonAsync(new Uri(requestUrl), requestContent, CancellationToken.None);
        }

        public static Task<HttpResponseMessage> PutContentAsJsonAsync<T>(this HttpClient client, string requestUrl, T requestContent, CancellationToken cancellationToken)
        {
            return client.PutContentAsJsonAsync(new Uri(requestUrl), requestContent, cancellationToken);
        }

        public static Task<HttpResponseMessage> PutContentAsJsonAsync<T>(this HttpClient client, Uri requestUri, T requestContent)
        {
            return client.PutContentAsJsonAsync(requestUri, requestContent, CancellationToken.None);
        }

        public static Task<HttpResponseMessage> PutContentAsJsonAsync<T>(this HttpClient client, Uri requestUri, T requestContent, CancellationToken cancellationToken)
        {
            var httpContent = CreateJsonHttpContent(requestContent);

            return client.PutAsync(requestUri, httpContent, cancellationToken);
        }

        public static Task<HttpResponseMessage> PatchContentAsJsonAsync<T>(this HttpClient client, string requestUrl, T requestContent)
        {
            return client.PatchContentAsJsonAsync(new Uri(requestUrl), requestContent, CancellationToken.None);
        }

        public static Task<HttpResponseMessage> PatchContentAsJsonAsync<T>(this HttpClient client, string requestUrl, T requestContent, CancellationToken cancellationToken)
        {
            return client.PatchContentAsJsonAsync(new Uri(requestUrl), requestContent, cancellationToken);
        }

        public static Task<HttpResponseMessage> PatchContentAsJsonAsync<T>(this HttpClient client, Uri requestUri, T requestContent)
        {
            return client.PatchContentAsJsonAsync(requestUri, requestContent, CancellationToken.None);
        }

        public static Task<HttpResponseMessage> PatchContentAsJsonAsync<T>(this HttpClient client, Uri requestUri, T requestContent, CancellationToken cancellationToken)
        {
            var httpContent = CreateJsonHttpContent(requestContent);

            return client.PatchAsync(requestUri, httpContent, cancellationToken);
        }

        private static HttpContent CreateJsonHttpContent<T>(T requestContent)
        {
            var requestBody = JsonConvert.SerializeObject(requestContent, JsonSerializerSettings);

            HttpContent httpContent = new StringContent(requestBody);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return httpContent;
        }
        
        #region Patch support

        public static Task<HttpResponseMessage> PatchAsync(this HttpClient client, string requestUri, HttpContent content)
        {
            var method = new HttpMethod("PATCH");

            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = content
            };

            return client.SendAsync(request);
        }

        public static Task<HttpResponseMessage> PatchAsync(this HttpClient client, Uri requestUri, HttpContent content)
        {
            var method = new HttpMethod("PATCH");

            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = content
            };

            return client.SendAsync(request);
        }

        public static Task<HttpResponseMessage> PatchAsync(this HttpClient client, string requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            var method = new HttpMethod("PATCH");

            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = content
            };

            return client.SendAsync(request, cancellationToken);
        }

        public static Task<HttpResponseMessage> PatchAsync(this HttpClient client, Uri requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            var method = new HttpMethod("PATCH");

            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = content
            };

            return client.SendAsync(request, cancellationToken);
        }

        #endregion
    }
}
