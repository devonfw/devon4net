using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Devon4Net.Application.Lambda.Business.StringManagement.Dto;
using Devon4Net.Infrastructure.AWS.Lambda.Interfaces;

namespace Devon4Net.Application.Lambda.Business.StringManagement.Handlers.Upper
{
    public class UpperFunctionEventHandler : ILambdaEventHandler<UpperInput, string>
    {
        public Task<string> FunctionHandler(UpperInput input, ILambdaContext context)
        {
            return Task.FromResult(input?.UpperData?.StringChain.ToUpper());
        }
    }
}
