using AOP.ErrorHandler.Common.Handler;
using Castle.Core;
using Castle.MicroKernel.Proxy;

namespace AOP.ErrorHandler.Common.Interceptor
{
    public class ExceptionInterceptor : IModelInterceptorsSelector
    {
        public InterceptorReference[] SelectInterceptors(ComponentModel model)
        {
            return new[] { InterceptorReference.ForType<ExceptionHandler>() };
        }

        public bool HasInterceptors(ComponentModel model)
        {
            return model.Implementation.Namespace != null && (typeof(ExceptionHandler) != model.Implementation);
        }

        public InterceptorReference[] SelectInterceptors(ComponentModel model, InterceptorReference[] interceptors)
        {
            return new[] { InterceptorReference.ForType<ExceptionHandler>() };

        }
    }
}