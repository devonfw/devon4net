using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.SNSEvents;
using Devon4Net.Application.Lambda.Business.SnsManagement.Dto;
using Devon4Net.Infrastructure.AWS.Lambda.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Devon4Net.Application.Lambda.Business.SnsManagement.Handlers
{
    public class SnsFunctionEventHandler<TMessage> : ILambdaEventHandler<SNSEvent, SnsFunctionResult> where TMessage : class
    {
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;

        public SnsFunctionEventHandler(IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _logger = loggerFactory?.CreateLogger(typeof(TMessage).Name) ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task<SnsFunctionResult> FunctionHandler(SNSEvent input, ILambdaContext context)
        {
            var result = new SnsFunctionResult();
            if (!input.Records.Any()) return result;

            foreach (var record in input.Records)
            {
                if (record?.Sns == null) continue;

                using var scope = _serviceProvider.CreateScope();
                var messageValue = record.Sns.Message;
                var message = JsonSerializer.Deserialize<TMessage>(messageValue);
                
                _logger.LogDebug($"Message : {messageValue}");

                var messageHandler = scope.ServiceProvider.GetService<IMessageHandler<TMessage, SnsMessageHandlerResult>>();
                if (messageHandler == null)
                {
                    var errorMessage = $"No IMessageHandler<{typeof(TMessage).Name}> found";
                    _logger.LogCritical(errorMessage);
                    throw new InvalidOperationException(errorMessage);
                }

                await messageHandler.HandleMessage(message, context).ConfigureAwait(false);
            }

            result.NumberOfMessages = input.Records.Count;

            return result;
        }
    }
}
