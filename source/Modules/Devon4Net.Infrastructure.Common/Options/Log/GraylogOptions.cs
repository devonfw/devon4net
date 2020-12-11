namespace Devon4Net.Infrastructure.Common.Options.Log
{
    public class GraylogOptions
    {
        public string GrayLogHost { get; set; }
        public int GrayLogPort { get; set; }
        public string GrayLogProtocol { get; set; }
        public bool UseSecureConnection { get; set; }
        public bool UseAsyncLogging { get; set; }
        public int RetryCount { get; set; }
        public int RetryIntervalMs { get; set; }
        public int MaxUdpMessageSize { get; set; }
    }
}
