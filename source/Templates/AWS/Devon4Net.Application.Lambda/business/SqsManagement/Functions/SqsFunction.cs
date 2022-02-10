using Amazon.Lambda.SQSEvents;
using Devon4Net.Application.Lambda.Business.SqsManagement.Dto;
using Devon4Net.Application.Lambda.Business.SqsManagement.Handlers;
using Devon4Net.Infrastructure.AWS.Lambda;
using Devon4Net.Infrastructure.AWS.Lambda.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Application.Lambda.Business.SqsManagement.Functions
{
    /// <summary>
    /// Use this as message body:       "body": "{\"Message\":\"Hello from SQS!\"}"
    /// </summary>
    public class SqsFunction : LambdaFunction<SQSEvent, SqsFunctionResult>
    {
        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ILambdaEventHandler<SQSEvent, SqsFunctionResult>, SqsFunctionEventHandler<SqsCustomMessage>>();
            services.AddTransient<IMessageHandler<SqsCustomMessage, SqsMessageHandlerResult>, SqsMessageHandler>();
        }
    }
}
