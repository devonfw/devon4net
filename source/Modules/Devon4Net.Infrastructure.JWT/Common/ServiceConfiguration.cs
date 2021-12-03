using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Infrastructure.JWT.Common
{
    public static class ServiceConfiguration
    {
        public static void AddJwtPolicy(this IServiceCollection services, string policyName, string claimType, string claimValue)
        {
            services.AddAuthorizationCore(options => options.AddPolicy(policyName, policy => policy.RequireClaim(claimType, claimValue)));
        }
    }
}
