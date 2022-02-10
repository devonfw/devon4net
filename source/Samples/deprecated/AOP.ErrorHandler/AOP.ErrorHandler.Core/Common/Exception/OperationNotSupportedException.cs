using System;

namespace AOP.ErrorHandler.Core.Common.Exception
{
    public class OperationNotSupportedException : CustomException
    {
        public OperationNotSupportedException(string operation, string trigger, string previousOperation)
            : base(string.Format("Operation {0} is not supported in the current state of {1}, please call {2} first", operation, trigger, previousOperation))
        {
        }

        public OperationNotSupportedException(string message)
            : base(message)
        {
        }

        public override void DoExceptionStuff()
        {
            Console.WriteLine("OperationNotSupportedException");
        }
    }
}