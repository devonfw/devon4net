using System.Collections.Generic;
using System.Linq;

namespace Devon4Net.Infrastructure.Common.Options.Kafka
{
    public class Consumer
    {
        public string ConsumerId { get; set; }
        public string Servers { get; set; }
        public string ClientId { get; set; }
        public string GroupId { get; set; }
        public string Topics { get; set; }
        public bool AutoCommit { get; set; }
        public int? StatisticsIntervalMs { get; set; }
        public int? SessionTimeoutMs { get; set; }
        public string AutoOffsetReset { get; set; }
        public bool EnablePartitionEof { get; set; }
        public string IsolationLevel { get; set; }
        public bool EnableSslCertificateVerification { get; set; }

        public List<string> GetTopics()
        {
            return string.IsNullOrEmpty(Topics) ? new List<string>() : Topics.Split(',').ToList();
        }
    }
}