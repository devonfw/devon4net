using Devon4Net.Infrastructure.Common.Application.Middleware.Headers;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Infrastructure.Common.Application.ApplicationTypes.API.Configuration
{
    public static class XsrfConfiguration
    {
        public static void ConfigureXsrf(this IServiceCollection services)
        {
            services.AddAntiforgery(options =>
            {
                options.HeaderName = CustomMiddlewareHeaderTypeConst.XsrfToken;
                options.SuppressXFrameOptionsHeader = false;
            });
        }
    }
}
