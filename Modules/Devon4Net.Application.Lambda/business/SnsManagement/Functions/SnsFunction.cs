using Amazon.Lambda.Core;
using Amazon.Lambda.SNSEvents;
using Devon4Net.Application.Lambda.Business.SnsManagement.Dto;
using Devon4Net.Application.Lambda.Business.SnsManagement.Handlers;
using Devon4Net.Infrastructure.AWS.Lambda;
using Devon4Net.Infrastructure.AWS.Lambda.Interfaces;
using Microsoft.Extensions.DependencyInjection;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
namespace Devon4Net.Application.Lambda.Business.SnsManagement.Functions
{
    /// <summary>
    /// Use this as message: "Message": "{\"Message\":\"Hello from SNS!!\"}"
    /// </summary>
    public class SnsFunction : LambdaFunction<SNSEvent, SnsFunctionResult>
    {
        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ILambdaEventHandler<SNSEvent, SnsFunctionResult>, SnsFunctionEventHandler<SnsCustomMessage>>();
            services.AddTransient<IMessageHandler<SnsCustomMessage, SnsMessageHandlerResult>, SnsMessageHandler>();
        }
    }
}
