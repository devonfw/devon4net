using AOP.ErrorHandler.Common.Handler;

namespace AOP.ErrorHandler.Common.Helper
{
    public class AOPErrorHandlerHelper
    {
        public static T GetInstance<T>()
        {
            return ErrorHandlerSingleton.Container.Resolve<T>();
        }
    }
}
