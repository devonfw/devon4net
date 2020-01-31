using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Devon4Net.Infrastructure.Common.Exceptions;
using Devon4Net.Infrastructure.Common.Options.KillSwitch;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Devon4Net.Infrastructure.Middleware.Exception
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context, IOptionsMonitor<KillSwitchOptions> killSwitch)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            try
            {
                if (killSwitch?.CurrentValue.UseKillSwitch == true)
                {
                    if (killSwitch?.CurrentValue != null && killSwitch.CurrentValue.EnableRequests)
                    {
                        await _next(context).ConfigureAwait(false);
                    }
                    else
                    {
                        context.Response.Headers.Clear();
                        context.Response.StatusCode = killSwitch.CurrentValue.HttpStatusCode > 0 ? killSwitch.CurrentValue.HttpStatusCode : 403;
                        await context.Response.WriteAsync(string.Empty).ConfigureAwait(false);
                    }
                }
                else
                {
                    await _next(context).ConfigureAwait(false);
                }
            }
#pragma warning disable CA1031 // #warning directive
            catch (System.Exception ex)
#pragma warning restore CA1031 // #warning directive
            {
                await HandleException(ref context, ref ex).ConfigureAwait(false);
            }
        }

        private Task HandleException(ref HttpContext context, ref System.Exception exception)
        {
            var exceptionTypeValue = exception.GetType();
            List<string> exceptionInterfaces = exceptionTypeValue.GetInterfaces().Select(i => i.Name).ToList();
            exceptionInterfaces.Add(exceptionTypeValue.Name);

            switch (exceptionInterfaces)
            {
                case List<string> exceptionType when exceptionType.Contains("InvalidDataException"):
                    return HandleContext(ref context, StatusCodes.Status422UnprocessableEntity);

                case List<string> exceptionType when exceptionType.Contains("ArgumentException")
                                                || exceptionType.Contains("ArgumentNullException")
                                                || exceptionType.Contains("NotFoundException")
                                                || exceptionType.Contains("FileNotFoundException"):
                    return HandleContext(ref context, StatusCodes.Status400BadRequest);

                case List<string> exceptionType when exceptionType.Contains("IWebApiException"):
                    return HandleContext(ref context, ((IWebApiException)exception).StatusCode, ((IWebApiException)exception).ShowMessage ? exception.Message : null);

                default:
                    return HandleContext(ref context, StatusCodes.Status500InternalServerError);
            }
        }

        private static Task HandleContext(ref HttpContext context, int? statusCode = null, string errorMessage = null)
        {
            context.Response.Headers.Clear();
            context.Response.StatusCode = statusCode ?? StatusCodes.Status500InternalServerError;

            if (string.IsNullOrEmpty(errorMessage)) return context.Response.WriteAsync(string.Empty);
            
            context.Response.ContentType = "application/json";
            var result = JsonConvert.SerializeObject(new { error = errorMessage });
            return context.Response.WriteAsync(result);

        }
    }
}
