using System.Threading.Tasks;
using Devon4Net.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;

namespace Devon4Net.Infrastructure.Middleware.Headers
{
    /// <summary>
    /// Please read https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/index?tabs=aspnetcore2x#what-is-middleware
    /// </summary>
    public class CustomHeadersMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomHeadersMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method.ToLower() == "options") await _next(context);

            if (!string.IsNullOrEmpty(MiddlewareHeaderDefinition.StrictTransportSecurityHeader))
            {
                context.TryAddHeader(CustomMiddlewareHeaderTypeConst.StrictTransportSecurityHeader, MiddlewareHeaderDefinition.StrictTransportSecurityHeader);
            }

            if (!string.IsNullOrEmpty(MiddlewareHeaderDefinition.XFrameOptionsHeader))
            {
                context.TryAddHeader(CustomMiddlewareHeaderTypeConst.XFrameOptionsHeader, MiddlewareHeaderDefinition.XFrameOptionsHeader);
            }

            if (!string.IsNullOrEmpty(MiddlewareHeaderDefinition.XssProtectionHeader))
            {
                context.TryAddHeader(CustomMiddlewareHeaderTypeConst.XssProtectionHeader, MiddlewareHeaderDefinition.XssProtectionHeader);
            }

            if (!string.IsNullOrEmpty(MiddlewareHeaderDefinition.XContentTypeOptionsHeader))
            {
                context.TryAddHeader(CustomMiddlewareHeaderTypeConst.XContentTypeOptionsHeader, MiddlewareHeaderDefinition.XContentTypeOptionsHeader);
            }

            if (!string.IsNullOrEmpty(MiddlewareHeaderDefinition.ContentSecurityPolicyHeader))
            {
                context.TryAddHeader(CustomMiddlewareHeaderTypeConst.ContentSecurityPolicyHeader, MiddlewareHeaderDefinition.ContentSecurityPolicyHeader);
            }

            if (!string.IsNullOrEmpty(MiddlewareHeaderDefinition.PermittedCrossDomainPoliciesHeader))
            {
                context.TryAddHeader(CustomMiddlewareHeaderTypeConst.PermittedCrossDomainPoliciesHeader, MiddlewareHeaderDefinition.PermittedCrossDomainPoliciesHeader);
            }

            if (!string.IsNullOrEmpty(MiddlewareHeaderDefinition.ReferrerPolicyHeader))
            {
                context.TryAddHeader(CustomMiddlewareHeaderTypeConst.ReferrerPolicyHeader, MiddlewareHeaderDefinition.ReferrerPolicyHeader);
            }

            context.TryRemoveHeader(CustomMiddlewareHeaderTypeConst.ServerHeader);
            context.TryRemoveHeader(CustomMiddlewareHeaderTypeConst.AspNetVersion);
            context.TryRemoveHeader(CustomMiddlewareHeaderTypeConst.XPowered);
            
            await _next(context).ConfigureAwait(false);
        }
    }
}
