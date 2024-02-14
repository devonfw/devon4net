using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Devon4Net.Infrastructure.Common.Exceptions;

namespace Devon4Net.Infrastructure.Common.Application.Attributes
{
    public class ExceptionHandlingFilterAttribute : ExceptionFilterAttribute
    {
        public ExceptionHandlingFilterAttribute() { }

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.Exception.InnerException != null)
                Devon4NetLogger.Debug(context.Exception.InnerException?.ToString()!);

            context.Result = context.Exception switch
            {
                WebApiException webApiException => HandleContext(webApiException.StatusCode, webApiException.Message, webApiException.ShowMessage),
                HttpRequestException httpRequestException => HandleContext((int?)httpRequestException.StatusCode, httpRequestException.Message),
                _ => HandleContext(),
            };

            return Task.CompletedTask;
        }

        private IActionResult HandleContext(int? statusCode = null, string? errorMessage = null, bool showMessage = false)
        {
            statusCode ??= StatusCodes.Status500InternalServerError;

            if (!showMessage || statusCode == StatusCodes.Status204NoContent || string.IsNullOrEmpty(errorMessage))
            {
                return new ContentResult
                {
                    StatusCode = (int)statusCode,
                    Content = string.Empty,
                };
            }

            var response = new { error = errorMessage };

            return new JsonResult(response)
            {
                StatusCode = (int)statusCode,
            };
        }
    }
}
