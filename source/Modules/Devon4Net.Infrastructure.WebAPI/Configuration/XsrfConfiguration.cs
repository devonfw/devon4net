using Devon4Net.Application.WebAPI.Configuration.Common;
using Devon4Net.Infrastructure.Middleware.Middleware.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Infrastructure.WebAPI.Configuration
{
    public static class XsrfConfiguration
    {
        public static void ConfigureXsrf(this IServiceCollection services, IConfiguration configuration)
        {
            _ = bool.TryParse(configuration[$"{DevonFwConst.OptionsNodeName}:UseXsrf"], out bool useXsrf);
            if (!useXsrf) return;

            services.AddAntiforgery(options =>
            {
                options.HeaderName = CustomMiddlewareHeaderTypeConst.XsrfToken;
                options.SuppressXFrameOptionsHeader = false;
            });
        }
    }
}
