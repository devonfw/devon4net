using Devon4Net.Infrastructure.Middleware.Certificates;
using Devon4Net.Infrastructure.Middleware.Exception;
using Microsoft.AspNetCore.Builder;
using Devon4Net.Infrastructure.Middleware.Headers;

namespace Devon4Net.Application.WebAPI.Configuration
{
    public static class MiddlewareConfiguration
    {
        public static void SetupDevonfwMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ExceptionHandlingMiddleware>();
            builder.UseMiddleware<CustomHeadersMiddleware>();
            builder.UseMiddleware<ClientCertificatesMiddleware>();
        }
    }
}
