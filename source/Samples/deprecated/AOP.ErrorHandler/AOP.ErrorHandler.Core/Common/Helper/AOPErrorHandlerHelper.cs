using AOP.ErrorHandler.Core.Common.Handler;

namespace AOP.ErrorHandler.Core.Common.Helper
{
    public class AOPErrorHandlerHelper
    {
        public static T GetInstance<T>()
        {
            return ErrorHandlerSingleton.Container.Resolve<T>();
        }
    }
}
