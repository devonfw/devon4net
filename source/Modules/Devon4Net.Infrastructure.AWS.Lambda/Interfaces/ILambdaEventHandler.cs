using Amazon.Lambda.Core;

namespace Devon4Net.Infrastructure.AWS.Lambda.Interfaces
{
    public interface ILambdaEventHandler<in TInput, TOutput> where TInput : class
    {
        Task<TOutput> FunctionHandler(TInput input, ILambdaContext context);
    }
}
