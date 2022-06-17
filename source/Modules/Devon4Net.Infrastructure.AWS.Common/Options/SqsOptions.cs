namespace Devon4Net.Infrastructure.AWS.Common.Options
{
    public class SqsQueueOptions
    {
        public string QueueName { get; set; }
        public string Url { get; set; }
        public bool UseFifo { get; set; }
        public int MaximumMessageSize { get; set; }
        public int NumberOfThreads { get; set; }
        public int DelaySeconds { get; set; }
        public int ReceiveMessageWaitTimeSeconds { get; set; }
        public RedrivePolicyOptions RedrivePolicy { get; set; }
    }

    public class RedrivePolicyOptions
    {
        public int MaxReceiveCount { get; set; }
        public string DeadLetterQueueUrl { get; set; }
    }
}
