using Microsoft.AspNetCore.Builder;
using Devon4Net.Infrastructure.Middleware.Middleware.Exception;
using Devon4Net.Infrastructure.Middleware.Middleware.Headers;
using Devon4Net.Infrastructure.Middleware.Middleware.Certificates;
using Devon4Net.Infrastructure.Common.Options.KillSwitch;
using Microsoft.Extensions.DependencyInjection;
using Devon4Net.Infrastructure.Common.Handlers;
using Microsoft.Extensions.Configuration;
using Devon4Net.Infrastructure.Middleware.Middleware.KillSwicth;
using Devon4Net.Infrastructure.Common.Options.Devon;
using Microsoft.Extensions.Options;

namespace Devon4Net.Infrastructure.Middleware.Middleware
{
    public static class MiddlewareConfiguration
    {

        public static void SetupMiddleware(this IApplicationBuilder builder, IServiceCollection services)
        {
            using var serviceProvider = services.BuildServiceProvider();
            var killSwitch = serviceProvider.GetService<IOptions<KillSwitchOptions>>()?.Value;
            var certificates = serviceProvider.GetService<IOptions<CertificatesOptions>>()?.Value;

            if (killSwitch?.UseKillSwitch == true) builder.UseMiddleware<KillSwicthMiddleware>();
            if (certificates?.ClientCertificate?.EnableClientCertificateCheck == true) builder.UseMiddleware<ClientCertificatesMiddleware>();

            builder.UseMiddleware<ExceptionHandlingMiddleware>();
            builder.UseMiddleware<CustomHeadersMiddleware>();
        }

        public static void SetupMiddleware(this IServiceCollection services, IConfiguration configuration)
        {
            services.SetupHeaders(configuration);

            services.GetTypedOptions<KillSwitchOptions>(configuration, "KillSwitch");
            services.GetTypedOptions<CertificatesOptions>(configuration, "Certificates");

        }
    }
}
