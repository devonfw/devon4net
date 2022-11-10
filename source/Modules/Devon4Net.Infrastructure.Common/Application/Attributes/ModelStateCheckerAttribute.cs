using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Devon4Net.Infrastructure.Common.Application.Attributes
{
    public class ModelStateCheckerAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList());
            }
        }
    }
}
