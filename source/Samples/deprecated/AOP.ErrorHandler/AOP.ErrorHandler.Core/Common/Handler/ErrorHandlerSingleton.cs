using Castle.Windsor;

namespace AOP.ErrorHandler.Core.Common.Handler
{
    public sealed class ErrorHandlerSingleton
    {
        private static volatile ErrorHandlerSingleton _instance;
        public static readonly WindsorContainer Container = CastleWindsorAopRegister.SetUpWindsor();

        private ErrorHandlerSingleton() { }

        public static ErrorHandlerSingleton Instance
        {
            get
            {
                if (_instance != null) return _instance;
                lock (Container)
                {
                    if (_instance == null)
                        _instance = new ErrorHandlerSingleton();
                }

                return _instance;
            }
        }
    }
}