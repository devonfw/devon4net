using Devon4Net.Infrastructure.AWS.Common.Consts;
using Devon4Net.Infrastructure.AWS.Common.Options;
using Devon4Net.Infrastructure.AWS.SQS.Handlers;
using Devon4Net.Infrastructure.Common.Handlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Devon4Net.Infrastructure.AWS.SQS.Dto;
using System.Threading.Tasks;

namespace Devon4Net.Infrastructure.AWS.SQS
{
    public static class SqsConfiguration
    {
        private static AwsOptions AwsOptions { get; set; }

        public static void SetupSqs(this IServiceCollection services, IConfiguration configuration)
        {
            AwsOptions = services.GetTypedOptions<AwsOptions>(configuration, ConfigurationConsts.AwsOptionsNodeName);
            if (AwsOptions == null || AwsOptions.SqSQueueList == null || AwsOptions.SqSQueueList == null || AwsOptions.SqSQueueList.Count == 0) return;
            services.AddSingleton<ISqsClientHandler, SqsClientHandler>();
        }

        public static void AddSqsConsumer<T>(this IServiceCollection services, string sqsQueueName) where T : class
        {
            if (AwsOptions == null || AwsOptions.SqSQueueList == null || AwsOptions.SqSQueueList == null || AwsOptions.SqSQueueList.Count == 0) return;

            using var sp = services.BuildServiceProvider();
            var sqsClientHandler = sp.GetService<ISqsClientHandler>();
            var obj = (T)Activator.CreateInstance(typeof(T), services, sqsClientHandler, GetSqsQueueFromSqsQueueNameAsync(sqsClientHandler, sqsQueueName).Result);

            services.AddSingleton(obj);
        }

        private static async Task<SqsQueue> GetSqsQueueFromSqsQueueNameAsync(ISqsClientHandler sqsClientHandler, string queueName)
        {
            var queueOptions = GetSqsOptions(queueName);

            return new SqsQueue
            {
                QueueName = queueName,
                DelaySeconds = queueOptions.DelaySeconds,
                MaximumMessageSize = queueOptions.MaximumMessageSize,
                NumberOfThreads = queueOptions.NumberOfThreads,
                ReceiveMessageWaitTimeSeconds = queueOptions.ReceiveMessageWaitTimeSeconds,
                Url = string.IsNullOrEmpty(queueOptions.Url) ? await sqsClientHandler.GetQueueUrl(queueOptions.QueueName).ConfigureAwait(false) : queueOptions.Url,
                UseFifo = queueOptions.UseFifo,
                RedrivePolicy = new QueueRedrivePolicy
                {
                    DeadLetterQueueUrl = queueOptions.RedrivePolicy.DeadLetterQueueUrl,
                    MaxReceiveCount = queueOptions.RedrivePolicy.MaxReceiveCount
                }
            };
        }

        private static SqsQueueOptions GetSqsOptions(string queueName)
        {
            var queueOptions = AwsOptions.SqSQueueList.Find(q => q.QueueName == queueName);
            if (queueOptions == null) throw new ArgumentException($"No configuration options for the queue {queueName}");
            return queueOptions;
        }
    }
}
