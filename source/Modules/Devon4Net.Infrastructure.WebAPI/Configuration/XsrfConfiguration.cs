using Devon4Net.Infrastructure.Middleware.Middleware.Headers;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Infrastructure.WebAPI.Configuration
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
