
namespace Devon4Net.Infrastructure.Kafka.Options
{
    public class StreamOptions
    {
        public string ApplicationId { get; set; }
        public string Servers { get; set; }
        public string AutoOffsetReset { get; set; }
        public string StateDir { get; set; }
        public long? CommitIntervalMs { get; set; }
        public string Guarantee { get; set; }
        public string MetricsRecording { get; set; }
    }
}
