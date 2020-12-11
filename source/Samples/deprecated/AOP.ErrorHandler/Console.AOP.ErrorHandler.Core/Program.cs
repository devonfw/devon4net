using System;
using AOP.ErrorHandler.Core.Common.Helper;
using Services.AOP.ErrorHandler.Core.Services.Interfaces;

namespace Console.AOP.ErrorHandler.Core
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