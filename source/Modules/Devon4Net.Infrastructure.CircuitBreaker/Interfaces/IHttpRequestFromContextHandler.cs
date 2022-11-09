using Devon4Net.Infrastructure.Common.Constants;
using Microsoft.AspNetCore.Http;

namespace Devon4Net.Infrastructure.CircuitBreaker.Interfaces
{
    public interface IHttpRequestFromContextHandler
    {
        HttpRequestMessage GetHttpRequestMessageFromContext(HttpContext context, string uriDestination, bool addForwardedHeaders, object content = null, string contentMediaType = MediaType.ApplicationJson, bool contentAsJson = true, bool contentUseCamelCase = false);
    }
}