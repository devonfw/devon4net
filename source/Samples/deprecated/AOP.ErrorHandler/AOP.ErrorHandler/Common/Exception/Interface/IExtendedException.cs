using System;
using System.Reflection;

namespace AOP.ErrorHandler.Common.Exception.Interface
{
    public  interface IExtendedException
    {
        string Target { get; set; }
        string MethodName { get; set; }
        string FormattedParams { get; set; }
        ParameterInfo[] Params { get; set; }
        DateTime DateTimeException { get; set; }

        void DoExceptionStuff();
    }
}