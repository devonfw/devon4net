namespace Devon4Net.Infrastructure.CircuitBreaker.Handler
{
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;

    public interface ICircuitBreakerHttpClient
    {
        Task<string> Delete(string endPointName, string url);
        Task<Stream> GetAsStream(string endPointName, string url);
        Task<string> Get(string endPointName, string url);
        Task<HttpResponseMessage> Patch(string endPointName, string url, HttpContent content);
        Task<T> Post<T>(string endPointName, string url, object dataToSend, string mediaType);
        Task<T> Put<T>(string endPointName, string url, object dataToSend, string mediaType);
    }
}