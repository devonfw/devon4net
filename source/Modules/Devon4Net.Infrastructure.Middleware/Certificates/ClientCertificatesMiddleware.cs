using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Devon4Net.Infrastructure.Common.Options.Devon;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Devon4Net.Infrastructure.Middleware.Certificates
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

        public async Task Invoke(HttpContext context,  IOptions<DevonfwOptions> devonfwOptions)
        {
                var options = devonfwOptions.Value;
            if (options?.Kestrel?.ClientCertificate == null)
            {
                logger.LogWarning("No configuration options were provided");
                context.Response.StatusCode = 500;
                return;
            }
            
            if (!options.Kestrel.ClientCertificate.DisableClientCertificateCheck)
            {
               var clientCertificate = context.Connection.ClientCertificate;
                if (clientCertificate == null)
                {
                    logger.LogWarning("No client certificate passed");
                    context.Response.StatusCode = 403;
                }
                else if (!options.Kestrel.ClientCertificate.ClientCertificates.Whitelist.Any(s => s.Equals(clientCertificate.Thumbprint, StringComparison.InvariantCultureIgnoreCase)))
                {
                    logger.LogWarning($"Client certificate {clientCertificate.Thumbprint} not in whitelist");
                    context.Response.StatusCode = 403;
                }
                else
                {
                    await next(context).ConfigureAwait(false);
                }
            }
            else
            {
                await next(context).ConfigureAwait(false);
            }
        }
    }
}