using Amazon.SQS.Model;
using Devon4Net.Infrastructure.AWS.Common.Options;
using Devon4Net.Infrastructure.AWS.SQS.ConsoleApplication.Business.SQSManagement.Dto;
using Devon4Net.Infrastructure.AWS.SQS.ConsoleApplication.Common.Consts;
using Devon4Net.Infrastructure.AWS.SQS.Handlers;
using Devon4Net.Infrastructure.AWS.SQS.Interfaces;
using Devon4Net.Infrastructure.Common;
using Microsoft.Extensions.Options;

namespace Devon4Net.Infrastructure.AWS.SQS.ConsoleApplication
{
    public class SqsSample
    {
        private ISqsClientHandler SqsClientHandler { get; }
        private AwsOptions AwsOptions { get; }
        public SqsSample(ISqsClientHandler sqsClientHandler, IOptions<AwsOptions> options)
        {
            SqsClientHandler = sqsClientHandler;
            AwsOptions = options.Value;
        }

        public Task StartSending()
        {
            return SendMessages();
        }

        private async Task SendMessages()
        {
            try
            {
                var queue = AwsOptions.SqSQueueList.Find(q => q.QueueName == SqsSampleConsts.QueueName);
                var url = await SqsClientHandler.GetQueueUrl(queue.QueueName).ConfigureAwait(false);
                for (int i = 1; i < 100000; i++)
                {
                    Devon4NetLogger.Debug($"Sending message with Id: {i}");
                    await SqsClientHandler.SendMessage<SqSMessage>(url, new SqSMessage { Id = i }, queue.UseFifo).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error(ex);
            }
        }
    }
}
