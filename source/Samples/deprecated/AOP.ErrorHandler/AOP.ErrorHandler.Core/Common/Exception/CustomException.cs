using System;
using System.Collections.Generic;
using System.Reflection;
using AOP.ErrorHandler.Core.Common.Exception.Interface;

namespace AOP.ErrorHandler.Core.Common.Exception
{
    public class CustomException : System.Exception, IExtendedException
    {
        public virtual string Target { get; set; }
        public virtual string MethodName { get; set; }
        public virtual string FormattedParams { get; set; }
        public virtual ParameterInfo[] Params { get; set; }
        public virtual DateTime DateTimeException { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public IList<String> MethodsArgumentsStack { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message">The error occurred.</param>
        public CustomException(String message) : this(message, new List<String>(), null) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message">The error occurred.</param>
        /// <param name="argumentsStack">A list with information of the parameters in the method that throw the exception.</param>
        public CustomException(String message, IList<String> argumentsStack) : this(message, argumentsStack, null) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message">The error information.</param>
        /// <param name="inner">The inner exception you catch.</param>
        /// <param name="argumentsStack">A list with information of the parameters in the method that throw the exception.</param>
        public CustomException(String message, IList<String> argumentsStack, System.Exception inner)
            : base(message, inner)
        {
            MethodsArgumentsStack = argumentsStack;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message">The error information.</param>
        /// <param name="inner">The inner exception you catch.</param>        
        public CustomException(String message, System.Exception inner)
            : this(message, null, inner)
        {
        }

        public virtual void DoExceptionStuff()
        {
            Console.WriteLine("An exception has been produced");
        }
    }
}