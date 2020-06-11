using System.Collections.Generic;

namespace Devon4Net.Infrastructure.CircuitBreaker.Handler
{
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;

    public interface ICircuitBreakerHttpClient
    {
        Task<string> Delete(string endPointName, string url, Dictionary<string,string> headers = null);
        Task<T> Delete<T>(string endPointName, string url, Dictionary<string, string> headers = null, bool useCamelCase = false);
        Task<Stream> GetAsStream(string endPointName, string url, Dictionary<string,string> headers = null);
        Task<string> Get(string endPointName, string url, Dictionary<string,string> headers = null);
        Task<T> Get<T>(string endPointName, string url, Dictionary<string, string> headers = null, bool useCamelCase = false);
        Task<HttpResponseMessage> GetResponseMessage(string endPointName, string url, Dictionary<string, string> headers = null);
        Task<HttpResponseMessage> Patch(string endPointName, string url, HttpContent content, Dictionary<string,string> headers = null);
        Task<T> Post<T>(string endPointName, string url, object dataToSend, string mediaType, Dictionary<string, string> headers = null, bool useCamelCase = false);
        Task<T> PostJson<T>(string endPointName, string url, string jsonDataToSend, string mediaType, Dictionary<string, string> headers = null, bool useCamelCase = false);
        Task<T> Put<T>(string endPointName, string url, object dataToSend, string mediaType, Dictionary<string,string> headers = null, bool useCamelCase = false);
    }
}