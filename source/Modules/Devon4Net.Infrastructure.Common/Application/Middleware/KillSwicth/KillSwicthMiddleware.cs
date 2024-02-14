using System.Text.Json;
using Devon4Net.Infrastructure.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Devon4Net.Infrastructure.Common.Application.Options;

namespace Devon4Net.Infrastructure.Common.Application.Middleware.KillSwicth
{
    public class KillSwicthMiddleware
    {
        private readonly RequestDelegate _next;

        public KillSwicthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IOptionsMonitor<KillSwitchOptions> killSwitch)
        {
            if (killSwitch?.CurrentValue.UseKillSwitch == true)
            {
                if (killSwitch.CurrentValue?.EnableRequests == true)
                {
                    await _next(context).ConfigureAwait(false);
                }
                else
                {
                    context.Response.Headers.Clear();
                    context.Response.StatusCode = killSwitch.CurrentValue?.HttpStatusCode > 0 ? killSwitch.CurrentValue.HttpStatusCode : 403;
                    await context.Response.WriteAsync(string.Empty).ConfigureAwait(false);
                }
            }
            else
            {
                await _next(context).ConfigureAwait(false);
            }
        }
    }
}
