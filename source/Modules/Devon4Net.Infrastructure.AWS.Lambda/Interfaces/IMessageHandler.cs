using Amazon.Lambda.Core;

namespace Devon4Net.Infrastructure.AWS.Lambda.Interfaces
{
    public interface IMessageHandler<in TInput, TOutput> where TInput : class
    {
        Task<TOutput> HandleMessage(TInput message, ILambdaContext context);
    }
}
