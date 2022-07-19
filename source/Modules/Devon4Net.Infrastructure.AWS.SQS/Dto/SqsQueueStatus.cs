using System;

namespace Devon4Net.Infrastructure.AWS.SQS.Dto
{
    public class SqsQueueStatus
    {
        public bool IsConsuming { get; set; }
        public bool IsHealthy { get; set; }
        public int ApproximateNumberOfMessagesDelayed { get; set; }
        public string QueueName { get; set; }
        public int ApproximateNumberOfMessages { get; set; }
        public int ApproximateNumberOfMessagesNotVisible { get; set; }
        public bool IsFifo { get; set; }
        public DateTime LastModifiedTimestamp { get; set; }
    }
}
