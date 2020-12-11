using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Devon4Net.Application.Lambda.Business.StringManagement.Dto;
using Devon4Net.Infrastructure.AWS.Lambda.Interfaces;

namespace Devon4Net.Application.Lambda.Business.StringManagement.Handlers.Lower
{
    public class LowerFunctionEventHandler : ILambdaEventHandler<LowerInput, string>
    {
        public Task<string> FunctionHandler(LowerInput input, ILambdaContext context)
        {
            return Task.FromResult(input?.LowerData?.StringChain.ToLower());
        }
    }
}