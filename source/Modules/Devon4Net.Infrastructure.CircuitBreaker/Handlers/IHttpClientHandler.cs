using Devon4Net.Infrastructure.CircuitBreaker.Common.Enums;
using Microsoft.AspNetCore.Http;

namespace Devon4Net.Infrastructure.CircuitBreaker.Handlers
{
    public interface IHttpClientHandler
    {
        Task<T> Send<T>(HttpMethod httpMethod, string endPointName, string url, object content, string mediaType, Dictionary<string, string> headers = null, bool contentAsJson = true, bool useCamelCase = false, bool escapeDataString = false); //NOSONAR false positive
        Task<Stream> Send(HttpMethod httpMethod, string endPointName, string url, object content, string mediaType, Dictionary<string, string> headers = null, bool contentAsJson = true, bool useCamelCase = false, bool escapeDataString = false); //NOSONAR false positive
        Task<HttpResponseMessage> Send(HttpMethod httpMethod, string endPointName, string url, object content, string mediaType, bool contentAsJson = true, bool useCamelCase = false, Dictionary<string, string> headers = null, bool escapeDataString = false); //NOSONAR false positive
        Task<string> SendSoapRequest(string endPointName, string url, string soapMessage, string soapActionUrl = null, Dictionary<string, string> headers = null, bool escapeDataString = false);
        Task<HttpResponseMessage> RedirectRequest(string endPointName, HttpContext sourceContext, string uriDestination, bool addForwardedHeaders = false, object content = null, string contentMediaType = MediaType.ApplicationJson, bool contentAsJson = true, bool contentUseCamelCase = false); //NOSONAR false positive
        Task<T> SendFormRequest<T>(string endPointName, string url, IEnumerable<KeyValuePair<string, string>> dataToSend, Dictionary<string, string> headers = null, string mediaType = MediaType.ApplicationXwww, bool useCamelCase = false, bool escapeDataString = false);
        Task<T> SendFormRequest<T>(string endPointName, string url, IFormCollection dataToSend, Dictionary<string, string> headers = null, string mediaType = MediaType.ApplicationXwww, bool useCamelCase = false, bool escapeDataString = false);
    }
}