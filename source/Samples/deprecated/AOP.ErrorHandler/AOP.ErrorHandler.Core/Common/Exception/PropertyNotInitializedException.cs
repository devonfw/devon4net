namespace AOP.ErrorHandler.Core.Common.Exception
{
    /// <summary>
    /// 
    /// </summary>
    public class PropertyNotInitializedException : CustomException
    {
        public PropertyNotInitializedException(string propertyName, string methodName)
            : base(string.Format("Property or parameter {0} of method {1} was not initialized", propertyName, methodName))
        {
        }
    }
}