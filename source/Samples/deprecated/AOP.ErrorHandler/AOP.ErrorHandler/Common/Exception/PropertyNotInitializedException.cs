namespace AOP.ErrorHandler.Common.Exception
{
    public class PropertyNotInitializedException : CustomException
    {
        public PropertyNotInitializedException(string propertyName, string methodName)
            : base(string.Format("Property or parameter {0} of method {1} was not initialized", propertyName, methodName))
        {
        }
    }
}