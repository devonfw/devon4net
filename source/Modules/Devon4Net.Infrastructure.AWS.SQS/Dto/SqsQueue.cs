namespace Devon4Net.Infrastructure.AWS.SQS.Dto
{
    public class SqsQueue
    {
        public string QueueName { get; set; }
        public bool UseFifo { get; set; }
        public int MaximumMessageSize { get; set; }
        public int NumberOfThreads { get; set; }
        public int DelaySeconds { get; set; }
        public int ReceiveMessageWaitTimeSeconds { get; set; }
        public QueueRedrivePolicy RedrivePolicy { get; set; }
        public string Url { get; set; }
    }

    public class QueueRedrivePolicy
    {
        public int MaxReceiveCount { get; set; }
        public string DeadLetterQueueUrl { get; set; }
    }
}
