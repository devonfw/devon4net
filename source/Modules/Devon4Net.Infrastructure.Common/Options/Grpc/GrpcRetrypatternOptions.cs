namespace Devon4Net.Infrastructure.Common.Options
{
    public class GrpcRetrypatternOptions
    {
        public int MaxAttempts { get; set; }
        public int InitialBackoffSeconds { get; set; }
        public int MaxBackoffSeconds { get; set; }
        public double BackoffMultiplier { get; set; }
        public string RetryableStatus { get; set; }
    }

}
