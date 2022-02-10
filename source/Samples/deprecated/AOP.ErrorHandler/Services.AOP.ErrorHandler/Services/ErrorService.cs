using AOP.ErrorHandler.Common.Exception;
using Services.AOP.ErrorHandler.Services.Interfaces;

namespace Services.AOP.ErrorHandler.Services
{

    public class ErrorService : IErrorService
    {

        public virtual void DivideByZero()
        {

            var zero = 0;
            var aux = 2 / zero;            

        }

        public int DivideByParams(int aParam)
        {
            var zero = 0;            
            var aux = aParam / zero;
            return aux;
        }


        public virtual void ThrowCustomException()
        {
            throw new CustomException("CustomException");
        }

        public virtual void ThrowOperationNotSupportedException()
        {
            throw new OperationNotSupportedException("OperationNotSupportedException");
        }
    }
}
