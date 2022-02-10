using Devon4Net.Application.Lambda.Business.StringManagement.Dto;
using Devon4Net.Application.Lambda.Business.StringManagement.Handlers.Upper;
using Devon4Net.Infrastructure.AWS.Lambda;
using Devon4Net.Infrastructure.AWS.Lambda.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Application.Lambda.Business.StringManagement.Functions.Upper
{
    public class UpperFunction : LambdaFunction<UpperInput, string>
    {
        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ILambdaEventHandler<UpperInput, string>, UpperFunctionEventHandler>();
        }
    }
}
