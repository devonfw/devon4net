using System;
using System.Reflection;
using AOP.ErrorHandler.Common.Exception.Interface;
using Microsoft.Build.Utilities;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace AOP.ErrorHandler.Common.Exception
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

        }

    }
}