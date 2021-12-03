using Amazon.CDK;
using Amazon.CDK.AWS.SQS;

namespace Devon4Net.Infrastructure.AWS.CDK.Handlers
{
    public class AwsCdkQueueHandler : AwsCdkDefaultHandler
    {
        public AwsCdkQueueHandler(Construct scope, string applicationName, string environmentName) : base(scope, applicationName, environmentName)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="queueName"></param>
        /// <param name="visibilityTimeoutMinutes"></param>
        /// <param name="deliveryDelayMinutes">Max 15 min</param>
        /// <param name="fifo"></param>
        /// <param name="contentBasedDuplication"></param>
        /// <returns></returns>
        public IQueue Create(string id, string queueName, int visibilityTimeoutMinutes = 1, int deliveryDelayMinutes = 1, bool fifo = true, bool contentBasedDuplication = true)
        {
            if (deliveryDelayMinutes>15) throw new ArgumentException("The deliveryDelayMinutes param must be less or equal to 15");

            return new Queue(Scope,$"{ApplicationName}{EnvironmentName}{id}", new QueueProps
            {
                QueueName = $"{ApplicationName}{EnvironmentName}{queueName}",
                Fifo = fifo,
                VisibilityTimeout = Duration.Minutes(visibilityTimeoutMinutes),
                ContentBasedDeduplication = contentBasedDuplication,
                DeliveryDelay = Duration.Minutes(deliveryDelayMinutes)
            });
        }

        public IQueue Locate(string id, string arn)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(arn))
            {
                throw new ArgumentException("The SQS queue params id|arn can not be null or empty");
            }

            return Queue.FromQueueArn(Scope, id, arn);
        }
    }
}
