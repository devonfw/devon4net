using Amazon.SQS.Model;
using Devon4Net.Infrastructure.AWS.SQS.ConsoleApplication.Business.SQSManagement.Dto;
using Devon4Net.Infrastructure.AWS.SQS.Dto;
using Devon4Net.Infrastructure.AWS.SQS.Handlers;
using Devon4Net.Infrastructure.AWS.SQS.Interfaces;
using Devon4Net.Infrastructure.Common;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace Devon4Net.Infrastructure.AWS.SQS.ConsoleApplication.Business.SQSManagement.Consumers
{
    public class SqsConsumerSample : SqsMessageConsumerHandler<SqSMessage>
    {
        public SqsConsumerSample(IServiceCollection services, ISqsClientHandler sqsClientHandler, SqsQueue sqsQueue) : base(services, sqsClientHandler, sqsQueue)
        {
        }

        protected override Task<bool> ProcessMessage(Message message)
        {
            var theMessage = JsonSerializer.Deserialize<SqSMessage>(message.Body);
            Devon4NetLogger.Debug($"Received message {message.MessageId} with Id : {theMessage.Id}");
            return Task.FromResult(true);
        }
    }
}
