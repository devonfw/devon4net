using System.Text;
using Devon4Net.Infrastructure.Common.Helpers.Interfaces;
using Microsoft.AspNetCore.Http;
using Serilog;
using Serilog.Events;

namespace Devon4Net.Infrastructure.Common.Application.Middleware.Logs;

public class LoggerMiddleware
{
    private readonly RequestDelegate _next;
    private const string ResponseBodyEntity = "Response body";
    private const string RequestBodyEntity = "Request body";
    private const int MaxBodyResponse = 150;

    public LoggerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IRecyclableMemoryHelper recyclableMemoryHelper)
    {
        try
        {
            using var bodyReader = recyclableMemoryHelper.GetMemoryStream();
            context.Request.EnableBuffering();

            var originalResponseBody = context.Response.Body;
            context.Response.Body = bodyReader;

            Devon4NetLogger.Information($"REQUEST STARTED {context.TraceIdentifier} | HttpMethod: {context.Request.Method} | Path: {context.Request.Path} ");

            await _next(context).ConfigureAwait(false);
            bodyReader.Seek(0, SeekOrigin.Begin);
            await bodyReader.CopyToAsync(originalResponseBody).ConfigureAwait(false);

            if (Log.IsEnabled(LogEventLevel.Information))
            {
                if (context.Response.StatusCode is < StatusCodes.Status200OK or > StatusCodes.Status300MultipleChoices) Devon4NetLogger.Information(await HandleResponseStream(context.TraceIdentifier, context.Request?.Body, RequestBodyEntity).ConfigureAwait(false));
                Devon4NetLogger.Information(await HandleResponseStream(context.TraceIdentifier, context.Response.Body, ResponseBodyEntity, true).ConfigureAwait(false));
                Devon4NetLogger.Information($"REQUEST FINISHED {context.TraceIdentifier} | Status Code: {context.Response.StatusCode}");
            }
            if (Log.IsEnabled(LogEventLevel.Warning))
            {
                if (context.Response.StatusCode is >= StatusCodes.Status200OK and < StatusCodes.Status300MultipleChoices) return;
                Devon4NetLogger.Warning(await HandleResponseStream(context.TraceIdentifier, context.Response.Body, ResponseBodyEntity, true).ConfigureAwait(false));
                Devon4NetLogger.Warning($"REQUEST FINISHED {context.TraceIdentifier} Status Code: {context.Response.StatusCode}");
            }
            else
            {
                Devon4NetLogger.Debug(await HandleResponseStream(context.TraceIdentifier, context.Request?.Body, RequestBodyEntity).ConfigureAwait(false));
                Devon4NetLogger.Debug(await HandleResponseStream(context.TraceIdentifier, context.Response.Body, ResponseBodyEntity, true).ConfigureAwait(false));
                Devon4NetLogger.Debug($"REQUEST FINISHED {context.TraceIdentifier} | Status Code: {context.Response.StatusCode}");
            }
        }
        catch (System.Exception ex)
        {
            Devon4NetLogger.Error($"REQUEST FINISHED {context.TraceIdentifier} | Status Code: {context.Response.StatusCode} | Exception: {ex.Message}");
            throw;
        }
    }

    private static async Task<string> HandleResponseStream(string identifier, Stream bodyStream, string entityDisclaimer, bool trimBody = false)
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.Append("REQUEST ").Append(identifier).Append(' ').Append(entityDisclaimer).Append(": ");

        if (bodyStream == null || !bodyStream.CanRead)
        {
            stringBuilder.Append("No data found.");
            return stringBuilder.ToString();
        }

        using var bodyReader = new StreamReader(bodyStream);
        bodyStream.Seek(0, SeekOrigin.Begin);

        if (trimBody)
        {
            var buffer = new char[MaxBodyResponse];
            var count = await bodyReader.ReadBlockAsync(buffer, 0, MaxBodyResponse);
            stringBuilder.Append(new string(buffer, 0, count));
        }
        else
        {
            var text = await bodyReader.ReadToEndAsync().ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(text)) return default;
            stringBuilder.Append(text.Replace("\n", ""));
        }

        bodyStream.Seek(0, SeekOrigin.Begin);

        return stringBuilder.ToString();
    }
}