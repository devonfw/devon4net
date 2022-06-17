using Amazon;
using Amazon.Runtime;
using Amazon.SQS.Model;
using System;
using System.Threading.Tasks;
using System.Threading;
using Devon4Net.Infrastructure.AWS.SQS.Dto;
using Devon4Net.Infrastructure.AWS.SQS.Helper;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Devon4Net.Infrastructure.Logger.Logging;

namespace Devon4Net.Infrastructure.AWS.SQS.Handlers
{
    public abstract class SqsMessageConsumerHandler<T> : ISqsMessageConsumerHandler<T> where T : class
    {
        private CancellationTokenSource TokenSource { get; set; }
        protected SqsQueue SqsQueue { get; }
        protected abstract Task<bool> ProcessMessage(Message message);
        protected ISqsClientHandler SqsClientHandler { get; }
        protected IServiceCollection Services { get; set; }

        protected SqsMessageConsumerHandler(IServiceCollection services, ISqsClientHandler sqsClientHandler, SqsQueue sqsQueue)
        {
            SqsClientHandler = sqsClientHandler;
            Services = services;
            SqsQueue = sqsQueue;
        }

        protected SqsMessageConsumerHandler(IServiceCollection services, SqsQueue sqsQueue, AWSCredentials awsCredentials = null, RegionEndpoint regionEndpoint = null)
        {
            SqsClientHandler = new SqsClientHandler(awsCredentials, regionEndpoint);
            Services = services;
            SqsQueue = sqsQueue;
        }

        public Task Start()
        {
            TokenSource = new CancellationTokenSource();
            return Process();
        }

        public void Stop()
        {
            if (IsConsuming())
            {
                TokenSource.Cancel();
            }
        }

        private bool IsConsuming()
        {
            return TokenSource?.Token.IsCancellationRequested == false;
        }

        private async Task Process()
        {
            try
            {
                while (!TokenSource.Token.IsCancellationRequested)
                {
                    var messages = await SqsClientHandler.GetSqsMessages(SqsQueue.Url, SqsQueue.RedrivePolicy.MaxReceiveCount, SqsQueue.ReceiveMessageWaitTimeSeconds, TokenSource.Token).ConfigureAwait(false);
                    foreach (var message in messages)
                    {
                        var messageType = message.MessageAttributes.GetMessageTypeAttributeValue();

                        if (messageType == null)
                        {
                            throw new ArgumentNullException($"No MessageType attribute present in message {JsonSerializer.Serialize(message)}");
                        }

                        if (messageType.Equals(typeof(T).Name))
                        {
                            var processed = await ProcessMessage(message).ConfigureAwait(false);
                            if (processed)
                            {
                                _ = await SqsClientHandler.DeleteMessage(SqsQueue.Url, message.ReceiptHandle, TokenSource.Token).ConfigureAwait(false);
                            }
                        }
                    }
                }
            }
            catch (OperationCanceledException ex)
            {
                Devon4NetLogger.Information("The message operation process has been cancelled");
                Devon4NetLogger.Information(ex);
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error(ex);
                throw;
            }
        }
    }
}
