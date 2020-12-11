namespace Services.AOP.ErrorHandler.Services.Interfaces
{
    public interface IErrorService
    {
        void DivideByZero();
        int DivideByParams(int aParam);
        void ThrowCustomException();
        void ThrowOperationNotSupportedException();
    }
}
