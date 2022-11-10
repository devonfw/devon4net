using Amazon.SQS;
using Amazon.SQS.Model;
using Devon4Net.Infrastructure.AWS.Common.Options;
using Devon4Net.Infrastructure.AWS.SQS.Dto;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Devon4Net.Infrastructure.AWS.SQS.Interfaces
{
    public interface ISqsClientHandler
    {
        AmazonSQSClient CreateSQSClient();
        Task<List<string>> GetSqsQueues();
        Task<string> CreateSqsQueue(SqsQueueOptions sqsQueueOptions);
        Task UpdateSqsQueueAttribute(string queueUrl, string attribute, string value);
        Task<SqsQueueStatus> GetQueueStatus(string queueName);
        Task DeleteSqsQueue(string queueUrl, bool waitForDeletion = false);
        Task<List<Message>> GetSqsMessages(string queueUrl, int maxNumberOfMessagesToRetrievePerCall = 1, int ReceiveMessageWaitTimeSeconds = 0, CancellationToken cancellationToken = default);
        Task SendMessage<T>(string queueUrl, object message, bool isFifo);
        Task<bool> DeleteMessage(string queueUrl, string receiptHandle, CancellationToken cancellationToken = default);
        Task<string> GetQueueArn(string queueUrl);
        Task<string> GetQueueUrl(string queueName);
    }
}