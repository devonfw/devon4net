using System;
using AOP.ErrorHandler.Common.Helper;
using Services.AOP.ErrorHandler.Services.Interfaces;

namespace Console.AOP.ErrorHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var service = AOPErrorHandlerHelper.GetInstance<IErrorService>();

                service.ThrowCustomException();
                service.ThrowOperationNotSupportedException();
                service.DivideByParams(1);
            }
            catch (Exception ex)
            {
                ex = null;
            }
        }
    }
}
