using System.Collections.Generic;

namespace Devon4Net.Infrastructure.CircuitBreaker.Handler
{
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;

    public interface ICircuitBreakerHttpClient
    {
        Task<string> Delete(string endPointName, string url, Dictionary<string,string> headers = null);
        Task<Stream> GetAsStream(string endPointName, string url, Dictionary<string,string> headers = null);
        Task<string> Get(string endPointName, string url, Dictionary<string,string> headers = null);
        Task<T> Get<T>(string endPointName, string url, Dictionary<string, string> headers = null);
        Task<HttpResponseMessage> GetResponseMessage(string endPointName, string url, Dictionary<string, string> headers = null);
        Task<HttpResponseMessage> Patch(string endPointName, string url, HttpContent content, Dictionary<string,string> headers = null);
        Task<T> Post<T>(string endPointName, string url, object dataToSend, string mediaType, Dictionary<string, string> headers = null);
        Task<T> PostJson<T>(string endPointName, string url, string dataToSend, string mediaType, Dictionary<string, string> headers = null);
        Task<T> Put<T>(string endPointName, string url, object dataToSend, string mediaType, Dictionary<string,string> headers = null);
    }
}