using Microsoft.AspNetCore.Mvc.Filters;
using Devon4Net.Infrastructure.Logger.Logging;

namespace Devon4Net.Infrastructure.Logger.Attribute
{
    public class AopExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            Devon4NetLogger.Error(context.Exception);
            base.OnException(context);
        }

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            Devon4NetLogger.Error(context.Exception);
            return base.OnExceptionAsync(context);
        }
    }
}