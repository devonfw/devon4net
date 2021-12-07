using Devon4Net.Infrastructure.CircuitBreaker.Common.Enums;
using Microsoft.AspNetCore.Http;

namespace Devon4Net.Infrastructure.CircuitBreaker.Handlers
{
    public interface IHttpRequestFromContextHandler
    {
        HttpRequestMessage GetHttpRequestMessageFromContext(HttpContext context, string uriDestination, bool addForwardedHeaders, object content = null, string contentMediaType = MediaType.ApplicationJson, bool contentAsJson = true, bool contentUseCamelCase = false);
    }
}