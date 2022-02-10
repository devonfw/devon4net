using AOP.ErrorHandler.Factory.Interfaces;
using Castle.DynamicProxy;

namespace AOP.ErrorHandler.Factory
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
