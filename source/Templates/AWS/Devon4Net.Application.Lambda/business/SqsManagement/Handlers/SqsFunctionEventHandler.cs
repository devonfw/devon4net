using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Devon4Net.Application.Lambda.Business.SqsManagement.Dto;
using Devon4Net.Infrastructure.AWS.Lambda.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Devon4Net.Application.Lambda.Business.SqsManagement.Handlers
{
    public class SqsFunctionEventHandler<TMessage> : ILambdaEventHandler<SQSEvent, SqsFunctionResult> where TMessage : class
    {
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;
        public SqsFunctionEventHandler(IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _logger = loggerFactory?.CreateLogger(typeof(TMessage).Name) ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task<SqsFunctionResult> FunctionHandler(SQSEvent input, ILambdaContext context)
        {
            var result = new SqsFunctionResult();

            if (!input.Records.Any()) return result;

            foreach (var record in input.Records)
            {
                if (record?.Body == null ) continue;

                using var scope = _serviceProvider.CreateScope();
                var sqsMessage = record.Body;
                var message = JsonSerializer.Deserialize<TMessage>(sqsMessage);

                var messageHandler = scope.ServiceProvider.GetService<IMessageHandler<TMessage, SqsMessageHandlerResult>>();
                if (messageHandler == null)
                {
                    var messageHandlerName = typeof(TMessage).Name;
                    _logger.LogCritical("No IMessageHandler<{messageHandlerName}> found", messageHandlerName);
                    throw new InvalidOperationException($"No IMessageHandler<{messageHandlerName}> found");
                }

                await messageHandler.HandleMessage(message, context).ConfigureAwait(false);
            }

            //Check Async management!! ForEachAsync

            result.NumberOfMessages = input.Records.Count;
            
            return result;
        }
    }
}
