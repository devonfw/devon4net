using Devon4Net.Application.Lambda.Business.StringManagement.Dto;
using Devon4Net.Application.Lambda.Business.StringManagement.Handlers.Lower;
using Devon4Net.Infrastructure.AWS.Lambda;
using Devon4Net.Infrastructure.AWS.Lambda.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Application.Lambda.Business.StringManagement.Functions.Lower
{
    public class LowerFunction : LambdaFunction<LowerInput, string>
    {
        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ILambdaEventHandler<LowerInput, string>, LowerFunctionEventHandler>();
        }
    }
}