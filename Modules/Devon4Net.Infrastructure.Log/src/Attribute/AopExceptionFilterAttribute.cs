using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Devon4Net.Infrastructure.Log.Attribute
{
    public class AopExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            Devon4Net.Infrastructure.Log.Devon4NetLogger.Error(context.Exception);
            base.OnException(context);
        }

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            Devon4Net.Infrastructure.Log.Devon4NetLogger.Error(context.Exception);
            return base.OnExceptionAsync(context);
        }
    }
}