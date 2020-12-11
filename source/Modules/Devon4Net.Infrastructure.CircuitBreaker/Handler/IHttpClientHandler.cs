using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Devon4Net.Infrastructure.CircuitBreaker.Handler
{
    public interface IHttpClientHandler
    {
        Task<T> Send<T>(HttpMethod httpMethod, string endPointName, string url, object content, string mediaType, Dictionary<string, string> headers = null, bool contentAsJson = true, bool useCamelCase = false);
        Task<Stream> Send(HttpMethod httpMethod, string endPointName, string url, object content, string mediaType, Dictionary<string, string> headers = null, bool contentAsJson = true, bool useCamelCase = false);
        Task<HttpResponseMessage> Send(HttpMethod httpMethod, string endPointName, string url, object content, string mediaType, bool contentAsJson = true, bool useCamelCase = false, Dictionary<string, string> headers = null);
        Task<string> SendSoapRequest(string endPointName, string url, string soapMessage, string soapActionUrl = null, Dictionary<string, string> headers = null);
    }
}