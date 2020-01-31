using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace OASP4Net.Infrastructure.AOP
{
    public class AopControllerAttribute : ActionFilterAttribute
    {
        private readonly ILogger _logger;
        private Serilog.ILogger _seriLogger;

        public AopControllerAttribute(ILogger logger )
        {
            _logger = logger;

        }

        public AopControllerAttribute(Serilog.ILogger logger)
        {
            _seriLogger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var parameters = JsonConvert.SerializeObject(context.ActionArguments);
            DebugMessage($"Params: {parameters}");
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            DebugMessage("ClassFilter OnActionExecuted");
            base.OnActionExecuted(context);
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            DebugMessage("ClassFilter OnResultExecuting");
            base.OnResultExecuting(context);
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            DebugMessage("ClassFilter OnResultExecuted");
            base.OnResultExecuted(context);
        }

        private void DebugMessage(string message)
        {
            _seriLogger?.Debug(message);
            _logger?.LogDebug(message);
            Console.WriteLine(message);
        }

    }
}
