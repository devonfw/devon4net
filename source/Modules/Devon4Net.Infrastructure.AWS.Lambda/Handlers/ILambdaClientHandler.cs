using System.Threading.Tasks;
using Amazon.Lambda;

namespace Devon4Net.Infrastructure.AWS.Lambda.Handlers
{
    public interface ILambdaClientHandler
    {
        Task<TOutput> Invoke<TInput, TOutput>(string functionName, TInput inputParam, InvocationType invocationType = null);
    }
}