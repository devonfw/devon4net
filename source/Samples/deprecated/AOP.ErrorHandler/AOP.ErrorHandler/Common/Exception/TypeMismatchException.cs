namespace AOP.ErrorHandler.Common.Exception
{
    public class TypeMismatchException : CustomException
    {
        public TypeMismatchException(string obtainedType, string expectedType, string method) : base(string.Format("Type {0} can not be cast to {1} in method {2}", obtainedType, expectedType, method)) { }

        public TypeMismatchException(string message) : base(message) { }
    }
}