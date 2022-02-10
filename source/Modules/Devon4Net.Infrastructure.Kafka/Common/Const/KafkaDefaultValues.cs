using Confluent.Kafka;

namespace Devon4Net.Infrastructure.Kafka.Common.Const
{
    public static class KafkaDefaultValues
    {
        public const int MessageMaxBytes = 1000000;
        public const CompressionType CompressionType = Confluent.Kafka.CompressionType.Gzip;
        public const int CompressionLevel = -1;
        public const int ReceiveMessageMaxBytes = 100000000;
        public const bool EnableSslCertificateVerification = false ;
        public const int CancellationDelayMaxMs = 100;
        public const Acks Ack = Acks.None;
        public const string Debug = "";
        public const int BrokerAddressTtl = 1000;
        public const int BatchNumMessages = 1000000;
        public const bool EnableIdempotence = false;
        public const int MaxInFlight = 5;
        public const int MessageSendMaxRetries = 5;
        public const int BatchSize = 1000000;
        public const int StatisticsIntervalMs = 0;
        public const int SessionTimeoutMs = 3000;
    }
}
