using System;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Devon4Net.Application.Lambda.Business.SnsManagement.Dto;
using Devon4Net.Infrastructure.AWS.Lambda.Interfaces;
using Microsoft.Extensions.Logging;

namespace Devon4Net.Application.Lambda.Business.SnsManagement.Handlers
{
    public class SnsMessageHandler : IMessageHandler<SnsCustomMessage, SnsMessageHandlerResult>
    {
        private readonly ILogger<SnsMessageHandler> _logger;

        public SnsMessageHandler(ILogger<SnsMessageHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task<SnsMessageHandlerResult> HandleMessage(SnsCustomMessage notification, ILambdaContext context)
        {
            var message = notification?.Message;
            _logger.LogInformation("Handling notification: {message}", message);

            return Task.FromResult(new SnsMessageHandlerResult{Content = notification?.Message });
        }
    }
}
