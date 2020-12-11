using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace OASP4Net.Infrastructure.AOP
{
    public class AopExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger _logger;
        private Serilog.ILogger _seriLogger;

        public AopExceptionFilter(ILogger logger)
        {
            _logger = logger;
        }

        public AopExceptionFilter(Serilog.ILogger logger)
        {
            _seriLogger = logger;
        }
        public override void OnException(ExceptionContext context)
        {
            DebugMessage("OnException");
            var error = $"{context.Exception?.Message} : {context.Exception?.InnerException}";
            DebugMessage
                (error);
            base.OnException(context);
        }

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            DebugMessage("OnActionExecuting async");
            return base.OnExceptionAsync(context);
        }
        private void DebugMessage(string message)
        {
            _seriLogger?.Debug(message);
            _logger?.LogDebug(message);
            Console.WriteLine(message);
        }
    }
}