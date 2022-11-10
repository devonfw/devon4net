namespace Devon4Net.Infrastructure.AWS.Common.Options
{
    public class AwsOptions
    {
        public bool EnableAws { get; set; }
        public AwsLogOptions AwsLogOptions { get; set; }
        public bool UseSecrets { get; set; }
        public bool UseParameterStore { get; set; }
        public bool UseSqs { get; set; }
        public List<SqsQueueOptions> SqSQueueList { get; set; }
        public Credentials Credentials { get; set; }
    }
}