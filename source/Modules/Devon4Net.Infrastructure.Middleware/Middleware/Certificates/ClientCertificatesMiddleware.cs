using Devon4Net.Infrastructure.Common.Options.Devon;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Devon4Net.Infrastructure.Middleware.Middleware.Certificates
{
    public class ClientCertificatesMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ClientCertificatesMiddleware> logger;

        public ClientCertificatesMiddleware(RequestDelegate next, ILogger<ClientCertificatesMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context,  IOptions<CertificatesOptions> devonfwOptions)
        {
            var options = devonfwOptions.Value;
            if (options?.ClientCertificate.ClientCertificates == null)
            {
                logger.LogWarning("No configuration options were provided");
                context.Response.StatusCode = 500;
                return;
            }

            var clientCertificate = context.Connection.ClientCertificate;
            if (clientCertificate == null)
            {
                logger.LogWarning("No client certificate passed");
                context.Response.StatusCode = 403;
            }
            else
            {
                if (!options.ClientCertificate.ClientCertificates.Whitelist.Any(s => s.Equals(clientCertificate.Thumbprint, StringComparison.InvariantCultureIgnoreCase)))
                {
                    var thumbprint = clientCertificate.Thumbprint;
                    logger.LogWarning("Client certificate {thumbprint} not in whitelist", thumbprint);
                    context.Response.StatusCode = 403;
                }
                else
                {
                    await next(context).ConfigureAwait(false);
                }
            }
        }
    }
}