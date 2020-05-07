using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Devon4Net.Infrastructure.Log.Attribute
{
    public class AopControllerAttribute : ActionFilterAttribute
    {
        private bool UseAopObjectTrace { get; set; }

        public AopControllerAttribute(bool useAop)
        {
            UseAopObjectTrace = useAop;
        }

        public AopControllerAttribute()
        {
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {                
            try
            {
                var controllerValues = GetControllerProperties((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor, context.ActionArguments);
                LogEvent("OnActionExecuting", $"Controller: {controllerValues.ControllerName} | method: {controllerValues.ControllerMethod}| ActionArguments: {controllerValues.ActionArguments}");
                base.OnActionExecuting(context);
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error(ex);
                throw;
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            try
            {
                if (context.Result!=null) LogEvent("OnActionExecuted", context.Result as Microsoft.AspNetCore.Mvc.ObjectResult);
                base.OnActionExecuted(context);
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error(ex);
                throw;
            }
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            try
            {                
                base.OnResultExecuting(context);
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error(ex);
                throw;
            }
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            try
            {
                if (context.Result != null) LogEvent("OnResultExecuted", context.Result as Microsoft.AspNetCore.Mvc.ObjectResult);
                base.OnResultExecuted(context);
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error(ex);
                throw;
            }
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            try
            {
                var controllerValues = GetControllerProperties((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor, context.ActionArguments);
                LogEvent("OnActionExecutionAsync", $"Controller: {controllerValues.ControllerName} | method: {controllerValues.ControllerMethod}| ActionArguments: {controllerValues.ActionArguments}");

                return base.OnActionExecutionAsync(context, next);
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error(ex);
                throw;
            }
        }

        public override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            try
            {
                if (context.Result != null) LogEvent("OnResultExecutionAsync", context.Result as Microsoft.AspNetCore.Mvc.ObjectResult);
                return base.OnResultExecutionAsync(context, next);
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error(ex);
                throw;
            }
        }

        private void LogEvent(string method, Microsoft.AspNetCore.Mvc.ObjectResult result)
        {
            Devon4NetLogger.Information($"Result from {method}: {result.StatusCode} | Value: {GetValue(result.Value)}");
        }

        private void LogEvent(string method, string result)
        {
            Devon4NetLogger.Information($"Result from {method}: {result}");
        }

        private string GetValue(object toPrint)
        {
            var result = string.Empty;
            try
            {
                result = JsonSerializer.Serialize(toPrint);
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error($"The result object can not be represented. Please use serializable objects: {ex.Message} | {ex.InnerException} ");
            }

            return result;
        }

        #region prettyprint
        private string PrettyPrint(object toPrint, string paramName = "", string separator = "\n", string prefix = "")
        {
            var sb = new StringBuilder(string.IsNullOrEmpty(paramName) ? string.Empty : prefix + paramName + ": ");
            if (toPrint == null)
            {
                sb.AppendFormat("null");
                return sb.ToString();
            }

            var flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.NonPublic;
            var properties = toPrint.GetType().GetProperties(flags);

            // basic type or struct
            if (!properties.Any() || IsBasic(toPrint))
            {
                sb.AppendFormat(prefix + toPrint + separator);
            }
            else if (IsEnumerable(toPrint))
            {
                var valueToPrint = separator;
                var values = (IEnumerable)toPrint;
                valueToPrint = values.Cast<object>().Aggregate(valueToPrint, (current, o) => current + (PrettyPrint(o, separator: ", ", prefix: prefix + "")));
                sb.AppendFormat(valueToPrint);
            }
            else
            {
                foreach (PropertyInfo info in properties.Where(info => info.PropertyType.Namespace != null && (info.Name != "SyncRoot" /*&& !info.PropertyType.Namespace.StartsWith("System")*/)))
                {
                    sb.AppendFormat("\t\n{0}{1}{2}", prefix, PrettyPrint(info.GetValue(toPrint, null), info.Name, prefix = prefix + ""), separator);
                }
            }

            return sb.ToString();
        }

        private bool IsBasic(object toPrint)
        {

            return toPrint.GetType().IsPrimitive || toPrint is DateTime || toPrint is string;
        }

        private bool IsEnumerable(Object obj)
        {
            return obj.GetType().IsGenericType && obj.GetType().GetInterfaces().Any(iface => iface.GetGenericTypeDefinition() == typeof(IEnumerable<>));
        }
        #endregion

        private AopController GetControllerProperties(Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor actionDescriptor, IDictionary<string, object> actionArguments)
        {
            return new AopController {
                ControllerName = actionDescriptor.ControllerName,
                ControllerMethod = actionDescriptor.MethodInfo.Name,
                ActionArguments = UseAopObjectTrace ? PrettyPrint(actionArguments) : "AOP Arguments not enabled!"
            }; 
        }

    }
}
