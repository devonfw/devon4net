using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AOP.ErrorHandler.Core.Common.Exception.Interface;
using Castle.DynamicProxy;

namespace AOP.ErrorHandler.Core.Common.Handler
{
    public class ExceptionHandler : IInterceptor
    {
        //!public static ILog Log; //remember to inject log4net instance!
        public virtual void Intercept(IInvocation invocation)
        {
            var args = invocation.Method;
            try
            {
                invocation.Proceed();
            }
            catch (System.Exception ex)
            {                
                var formattedParams = GetParametersValue(invocation.Method.GetParameters(), invocation.Method.Name, ref invocation);

                var msg =
                    $"{invocation.Method.Name + " from " + invocation.InvocationTarget} had an error. Check MethodArgumentsStack for more information. @ {DateTime.Now.ToString("yyyy/MM/dd")}: {ex.Message}\n{ex.StackTrace}\n{ex.InnerException}\n " +
                    $"The arguments were:\n{formattedParams}\n";


                WriteExceptionMessage(msg);

                var excepts = ex.GetType().GetInterfaces().Where(e => e.Name == "IExtendedException");
                if (excepts.Any())
                {
                    var customException = ex as IExtendedException;                    
                    if (customException != null)
                    {
                        customException.FormattedParams = formattedParams;
                        customException.MethodName = args.Name;
                        customException.Target = invocation.InvocationTarget.ToString();
                        customException.Params = invocation.Method.GetParameters();
                        customException.DoExceptionStuff();     
                        throw;
                    }
                }
                ex = null;
            }
            GC.Collect();
        }

        public Type GetExceptionType(MethodBase targetMethod)
        {
            return typeof(System.Exception);
        }

        // ReSharper disable once UnusedMember.Local
        private string PrettyPrint(object toPrint, string paramName = "", string separator = "\n", string prefix = "")
        {
            var sb = new StringBuilder(string.IsNullOrEmpty(paramName) ? string.Empty : prefix + paramName + ": ");
            if (toPrint == null)
            {
                sb.AppendFormat("null");
                return sb.ToString();
            }

            var flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy;
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
                valueToPrint = values.Cast<object>().Aggregate(valueToPrint, (current, o) => current + (PrettyPrint(o, separator: ", ", prefix: prefix + "\t")));
                sb.AppendFormat(valueToPrint);
            }
            else
            {
                foreach (PropertyInfo info in properties.Where(info => info.PropertyType.Namespace != null && (info.Name != "SyncRoot" && !info.PropertyType.Namespace.StartsWith("System"))))
                {
                    sb.AppendFormat("\t\n{0}{1}{2}", prefix, PrettyPrint(info.GetValue(toPrint, null), info.Name, prefix = prefix + "\t"), separator);
                }
            }

            return sb.ToString();
        }

        private string GetParametersValue(IEnumerable<ParameterInfo> args, string name, ref IInvocation aInvocation)
        {
            var arguments = string.Format("Method name: {0}{1}Arguments: {1}", name, Environment.NewLine);
            if (args != null)
            {
                int parameterNumber = 0;
                //(1) 
                foreach (var argument in args)
                {                    
                    var pprint =
                        $"Attribute: {argument.Name} | Value: {aInvocation.GetArgumentValue(parameterNumber)} | Type: {aInvocation.Arguments[parameterNumber].GetType()}";
                    arguments = arguments + pprint;
                    parameterNumber++;
                }
            }
            else
            {
                arguments += "void";
            }

            return arguments;
        }

        private static bool IsBasic(object toPrint)
        {            
            return toPrint is  char || toPrint is DateTime || toPrint is string || toPrint is int || toPrint is short || toPrint is long || toPrint is double || toPrint is String || toPrint is Single || toPrint is Byte;
        }

        private static bool IsEnumerable(Object obj)
        {
            return obj.GetType().GetInterfaces().Any(iface => iface.GetGenericTypeDefinition() == typeof(IEnumerable<>));
        }

        private static void WriteExceptionMessage(string message)
        {
            Console.WriteLine(message); 
        }
    }
}

//LINQ EXPRESSIONS:
//----------------------------
//(1) GetParametersValue:
//foreach (var argument in args)
//{
//    var pprint = PrettyPrint(argument, argument.Name);
//    arguments = arguments + pprint;
//}
//END LINQ EXPRESSIONS