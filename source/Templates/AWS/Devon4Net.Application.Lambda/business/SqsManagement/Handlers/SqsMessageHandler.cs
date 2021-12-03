using System;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Devon4Net.Application.Lambda.Business.SnsManagement.Handlers;
using Devon4Net.Application.Lambda.Business.SqsManagement.Dto;
using Devon4Net.Infrastructure.AWS.Lambda.Interfaces;
using Microsoft.Extensions.Logging;

namespace Devon4Net.Application.Lambda.Business.SqsManagement.Handlers
{
    public class SqsMessageHandler : IMessageHandler<SqsCustomMessage, SqsMessageHandlerResult>
    {
        private readonly ILogger<SnsMessageHandler> _logger;

        public SqsMessageHandler(ILogger<SnsMessageHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task<SqsMessageHandlerResult> HandleMessage(SqsCustomMessage message, ILambdaContext context)
        {
            var messageContent = message?.Message;
            _logger.LogInformation("Handling notification: {messageContent}", messageContent);

            return Task.FromResult(new SqsMessageHandlerResult{Content = message?.Message});
        }
    }
}
