using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Infrastructure.Middleware.Middleware.Headers
{
    public static class MiddlewareHeaderDefinition
    {
        public static string AccessControlExposeHeader { get; set; }
        public static string StrictTransportSecurityHeader { get; set; }
        public static string XFrameOptionsHeader { get; set; }
        public static string XssProtectionHeader { get; set; }
        public static string XContentTypeOptionsHeader { get; set; }
        public static string ContentSecurityPolicyHeader { get; set; }
        public static string PermittedCrossDomainPoliciesHeader { get; set; }
        public static string ReferrerPolicyHeader { get; set; }

        public static void SetupHeaders(this IServiceCollection services, IConfiguration configuration)
        {
            AccessControlExposeHeader = configuration["Headers:AccessControlExposeHeader"];
            StrictTransportSecurityHeader = configuration["Headers:StrictTransportSecurityHeader"];
            XFrameOptionsHeader = configuration["Headers:XFrameOptionsHeader"];
            XssProtectionHeader = configuration["Headers:XssProtectionHeader"];
            XContentTypeOptionsHeader = configuration["Headers:XContentTypeOptionsHeader"];
            ContentSecurityPolicyHeader = configuration["Headers:ContentSecurityPolicyHeader"];
            PermittedCrossDomainPoliciesHeader = configuration["Headers:PermittedCrossDomainPoliciesHeader"];
            ReferrerPolicyHeader = configuration["Headers:ReferrerPolicyHeader"];
        }
    }

}
