using AOP.ErrorHandler.Core.Factory.Interfaces;
using Castle.DynamicProxy;

namespace AOP.ErrorHandler.Core.Factory
{
    public class LoaderFactory : ILoaderFactory
    {
        public ILoader GetLoader()
        {
            return GetLoaderStatic();
        }

        public static ILoader GetLoaderStatic()
        {
            return (ILoader)new ProxyGenerator().CreateInterfaceProxyWithoutTarget(typeof(ILoader));
        }
    }
}
