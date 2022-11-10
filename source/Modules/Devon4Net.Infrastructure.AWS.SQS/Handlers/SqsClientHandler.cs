using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using Devon4Net.Infrastructure.AWS.Common.Options;
using Devon4Net.Infrastructure.AWS.SQS.Dto;
using Devon4Net.Infrastructure.AWS.SQS.Helper;
using Devon4Net.Infrastructure.AWS.SQS.Interfaces;
using Devon4Net.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Devon4Net.Infrastructure.AWS.SQS.Handlers
{
    public class SqsClientHandler : ISqsClientHandler, IDisposable
    {
        private AWSCredentials AWSCredentials { get; }
        private RegionEndpoint RegionEndpoint { get; }
        private AmazonSQSClient AmazonSQSClient { get; }

        public SqsClientHandler(AWSCredentials awsCredentials = null, RegionEndpoint regionEndpoint = null)
        {
            AWSCredentials = awsCredentials;
            RegionEndpoint = regionEndpoint;
            AmazonSQSClient = CreateSQSClient();
        }

        public SqsClientHandler(AmazonSQSClient amazonSQSClient, AWSCredentials awsCredentials = null, RegionEndpoint regionEndpoint = null)
        {
            AWSCredentials = awsCredentials;
            RegionEndpoint = regionEndpoint;
            AmazonSQSClient = amazonSQSClient;
        }

        public AmazonSQSClient CreateSQSClient()
        {
            var sqsConfig = new AmazonSQSConfig
            {
                RegionEndpoint = RegionEndpoint
            };

            return new AmazonSQSClient(AWSCredentials, sqsConfig);
        }

        public async Task<List<string>> GetSqsQueues()
        {
            try
            {
                var result = await AmazonSQSClient.ListQueuesAsync(string.Empty).ConfigureAwait(false);
                return result.QueueUrls;
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error(ex);
                throw;
            }
        }

        public async Task<string> CreateSqsQueue(SqsQueueOptions sqsQueueOptions)
        {
            var queueAttrs = new Dictionary<string, string>();

            try
            {
                queueAttrs.Add(QueueAttributeName.FifoQueue, sqsQueueOptions.UseFifo.ToString().ToLowerInvariant());
                queueAttrs.Add(QueueAttributeName.DelaySeconds, sqsQueueOptions.DelaySeconds.ToString().ToLowerInvariant());
                queueAttrs.Add(QueueAttributeName.MaximumMessageSize, sqsQueueOptions.MaximumMessageSize.ToString().ToLowerInvariant());
                queueAttrs.Add(QueueAttributeName.ReceiveMessageWaitTimeSeconds, sqsQueueOptions.ReceiveMessageWaitTimeSeconds.ToString().ToLowerInvariant());

                if (sqsQueueOptions.RedrivePolicy != null && !string.IsNullOrEmpty(sqsQueueOptions.RedrivePolicy.DeadLetterQueueUrl) && sqsQueueOptions.RedrivePolicy.MaxReceiveCount > 0)
                {
                    queueAttrs.Add(QueueAttributeName.RedrivePolicy, await GetRedrivePolicy(sqsQueueOptions.RedrivePolicy).ConfigureAwait(false));
                }

                var responseCreate = await AmazonSQSClient.CreateQueueAsync(new CreateQueueRequest { QueueName = sqsQueueOptions.QueueName, Attributes = queueAttrs }).ConfigureAwait(false);
                return responseCreate.QueueUrl;
            }
            catch (QueueNameExistsException ex)
            {
                Devon4NetLogger.Error($"Error creating the queue: {sqsQueueOptions.QueueName}. The queue name already exists");
                Devon4NetLogger.Error(ex);
                throw;
            }
            catch (QueueDeletedRecentlyException ex)
            {
                Devon4NetLogger.Error($"Error creating the queue: {sqsQueueOptions.QueueName}. The queue has been deleted recently");
                Devon4NetLogger.Error(ex);
                throw;
            }
        }

        public async Task<SqsQueueStatus> GetQueueStatus(string queueName)
        {
            CheckQueueName(queueName);
            var queueUrl = await GetQueueUrl(queueName).ConfigureAwait(false);

            try
            {
                var attributes = new List<string> { QueueAttributeName.ApproximateNumberOfMessages, QueueAttributeName.ApproximateNumberOfMessagesNotVisible, QueueAttributeName.LastModifiedTimestamp, QueueAttributeName.ApproximateNumberOfMessagesDelayed};
                var response = await AmazonSQSClient.GetQueueAttributesAsync(new GetQueueAttributesRequest(queueUrl, attributes)).ConfigureAwait(false);

                return new SqsQueueStatus
                {
                    IsHealthy = response.HttpStatusCode == HttpStatusCode.OK,
                    QueueName = queueName,
                    ApproximateNumberOfMessages = response.ApproximateNumberOfMessages,
                    ApproximateNumberOfMessagesNotVisible = response.ApproximateNumberOfMessagesNotVisible,
                    LastModifiedTimestamp = response.LastModifiedTimestamp,
                    ApproximateNumberOfMessagesDelayed = response.ApproximateNumberOfMessagesDelayed,
                    IsFifo = response.FifoQueue
                };
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error($"Failed to GetNumberOfMessages for queue {queueName}: {ex.Message}");
                Devon4NetLogger.Error(ex);
                throw;
            }
        }

        public async Task<List<Message>> GetSqsMessages(string queueUrl, int maxNumberOfMessagesToRetrievePerCall = 1, int ReceiveMessageWaitTimeSeconds = 0, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await AmazonSQSClient.ReceiveMessageAsync(new ReceiveMessageRequest
                {
                    QueueUrl = queueUrl,
                    MaxNumberOfMessages = maxNumberOfMessagesToRetrievePerCall,
                    WaitTimeSeconds = ReceiveMessageWaitTimeSeconds,
                    AttributeNames = new List<string> { QueueAttributeName.All },
                    MessageAttributeNames = new List<string> { QueueAttributeName.All }
                }, cancellationToken).ConfigureAwait(false);

                return result?.Messages;
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error($"Failed to GetSqsMessages for queue {queueUrl}: {ex.Message}");
                Devon4NetLogger.Error(ex);
                throw;
            }
        }

        public async Task SendMessage<T>(string queueUrl, object message, bool isFifo)
        {
            try
            {
                var sendMessageRequest = new SendMessageRequest
                {
                    QueueUrl = queueUrl,
                    MessageBody = JsonSerializer.Serialize(message),
                    MessageAttributes = SqsMessageTypeAttributehelper.CreateAttributes<T>()
                };

                if (isFifo)
                {
                    sendMessageRequest.MessageGroupId = typeof(T).Name;
                    sendMessageRequest.MessageDeduplicationId = Guid.NewGuid().ToString();
                }

                await AmazonSQSClient.SendMessageAsync(sendMessageRequest).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error($"Failed to PostMessagesAsync for queue {queueUrl}: {ex.Message}");
                Devon4NetLogger.Error(ex);
                throw;
            }
        }

        public async Task<bool> DeleteMessage(string queueUrl, string receiptHandle, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await AmazonSQSClient.DeleteMessageAsync(queueUrl, receiptHandle, cancellationToken).ConfigureAwait(false);
                return result.HttpStatusCode == HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error($"Failed to DeleteMessage for queue {queueUrl}: {ex.Message}");
                Devon4NetLogger.Error(ex);
                throw;
            }
        }

        public async Task DeleteSqsQueue(string queueUrl, bool waitForDeletion = false)
        {
            try
            {
                await AmazonSQSClient.DeleteQueueAsync(queueUrl).ConfigureAwait(false);

                if (!waitForDeletion) return;

                var queueExists = true;

                do
                {
                    var queues = await GetSqsQueues().ConfigureAwait(false);
                    queueExists = queues.Any(q => q == queueUrl);
                    await Task.Delay(1000);
                } while (queueExists);
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error($"Error deleting the queue: {queueUrl}");
                Devon4NetLogger.Error(ex);
            }
        }

        public async Task UpdateSqsQueueAttribute(string queueUrl, string attribute, string value)
        {
            try
            {
                await AmazonSQSClient.SetQueueAttributesAsync(queueUrl, new Dictionary<string, string> { { attribute, value } }).ConfigureAwait(false);
            }
            catch (InvalidAttributeNameException ex)
            {
                Devon4NetLogger.Error($"Error updating the attribute: {attribute}. The queue has been deleted recently name");
                Devon4NetLogger.Error(ex);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<string> GetQueueArn(string queueUrl)
        {
            try
            {
                var responseGetAtt = await AmazonSQSClient.GetQueueAttributesAsync(queueUrl, new List<string> { QueueAttributeName.QueueArn }).ConfigureAwait(false);
                return responseGetAtt.QueueARN;
            }
            catch (InvalidAttributeNameException ex)
            {
                Devon4NetLogger.Error($"Error getting the queue arn: {queueUrl}");
                Devon4NetLogger.Error(ex);
            }

            return null;
        }

        public async Task<string> GetQueueUrl(string queueName)
        {
            CheckQueueName(queueName);

            try
            {
                var response = await AmazonSQSClient.GetQueueUrlAsync(queueName).ConfigureAwait(false);
                return response.QueueUrl;
            }
            catch (QueueDoesNotExistException ex)
            {
                Devon4NetLogger.Error(ex);
                throw new InvalidOperationException($"Could not retrieve the URL for the queue {queueName} as it does not exist or check if you do not have access to the queue", ex);
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            AmazonSQSClient.Dispose();
        }

        private static void CheckQueueName(string queueName)
        {
            if (string.IsNullOrEmpty(queueName))
            {
                Devon4NetLogger.Error("Queue name can not be null or empty");
                throw new ArgumentException("Queue name can not be null or empty");
            }
        }

        private async Task<string> GetRedrivePolicy(RedrivePolicyOptions redrivePolicyOptions)
        {
            return JsonSerializer.Serialize(new RedrivePolicy
            {
                DeadLetterQueueUrl = await GetQueueArn(redrivePolicyOptions.DeadLetterQueueUrl).ConfigureAwait(false),
                MaxReceiveCount = redrivePolicyOptions.MaxReceiveCount
            });
        }
    }
}
