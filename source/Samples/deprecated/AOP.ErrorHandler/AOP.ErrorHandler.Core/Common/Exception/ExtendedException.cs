using System;
using System.Reflection;
using AOP.ErrorHandler.Core.Common.Exception.Interface;

//using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace AOP.ErrorHandler.Core.Common.Exception
{
    public class ExtendedException : System.Exception, IExtendedException
    {
        //public static ILog Log;
        //Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(message, category.ToString(), (int) priority, 0, eventType, name);
        public string Target { get; set; }
        public string MethodName { get; set; }
        public string FormattedParams { get; set; }
        public ParameterInfo[] Params { get; set; }
        public DateTime DateTimeException { get; set; }

        public void DoExceptionStuff()
        {
            //Logger.Write("Hello World once more", "My Category");
            //var msg = string.Format("{0} had an error. Check MethodArgumentsStack for more information. @ {1}: {2}\n{3}\n{4}\n " +
            //                        "The arguments were:\n{5}\n",
            //           Target + " / " + MethodName, DateTimeException.ToString("yyyy/MM/dd"), Message, StackTrace, InnerException, FormattedParams);
            
        }

    }
}