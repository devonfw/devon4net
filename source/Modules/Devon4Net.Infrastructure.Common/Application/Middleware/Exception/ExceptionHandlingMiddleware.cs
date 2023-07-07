using System.Text.Json;
using Devon4Net.Infrastructure.Common.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Devon4Net.Infrastructure.Common.Application.Middleware.Exception;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context).ConfigureAwait(false);
        }
        catch (System.Exception ex)
        {
            await HandleException(ref context, ref ex).ConfigureAwait(false);
        }
    }

    private static Task HandleException(ref HttpContext context, ref System.Exception exception)
    {
        Devon4NetLogger.Error(exception.Message);
        if(exception.InnerException != null)
            Devon4NetLogger.Debug(exception.InnerException?.ToString());

        var exceptionTypeValue = exception.GetType();
        var exceptionInterfaces = exceptionTypeValue.GetInterfaces().Select(i => i.Name).ToList();
        exceptionInterfaces.Add(exceptionTypeValue.Name);

        return exceptionInterfaces switch
        {
            { } when exceptionInterfaces.Contains("InvalidDataException") => HandleContext(ref context,
                StatusCodes.Status422UnprocessableEntity),
            { } when exceptionInterfaces.Contains("ArgumentException") ||
                     exceptionInterfaces.Contains("ArgumentNullException") ||
                     exceptionInterfaces.Contains("NotFoundException") ||
                     exceptionInterfaces.Contains("FileNotFoundException") => HandleContext(ref context,
                StatusCodes.Status400BadRequest),
            { } when exceptionInterfaces.Contains("ValidationException") => HandleContext(ref context,
                StatusCodes.Status400BadRequest, exception.Message, true),
            { } when exceptionInterfaces.Contains("IWebApiException") => HandleContext(ref context,
                ((IWebApiException)exception).StatusCode, exception.Message,
                ((IWebApiException)exception).ShowMessage),
            _ => HandleContext(ref context, StatusCodes.Status500InternalServerError, exception.Message)
        };
    }

    private static Task HandleContext(ref HttpContext context, int? statusCode = null, string errorMessage = null,
        bool showMessage = false)
    {
        context.Response.Headers.Clear();
        context.Response.StatusCode = statusCode ?? StatusCodes.Status500InternalServerError;

        if (!showMessage || statusCode == StatusCodes.Status204NoContent || string.IsNullOrEmpty(errorMessage))
            return Task.CompletedTask;

        context.Response.ContentType = "application/json";
        return context.Response.WriteAsync(JsonSerializer.Serialize(new { error = errorMessage }));
    }
}