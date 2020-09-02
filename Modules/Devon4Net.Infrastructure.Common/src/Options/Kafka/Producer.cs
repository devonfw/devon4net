namespace Devon4Net.Infrastructure.Common.Options.Kafka
{
    public class Producer
    {
        public string ProducerId { get; set; }
        public string Servers { get; set; }
        public string ClientId { get; set; }
        public int? MessageMaxBytes { get; set; }
        public int? CompressionLevel { get; set; }
        public string CompressionType { get; set; }
        public int? ReceiveMessageMaxBytes { get; set; }
        public bool EnableSslCertificateVerification { get; set; }
        public int? CancellationDelayMaxMs { get; set; }
        public string Ack { get; set; }
        public string Debug { get; set; }
        public int? BrokerAddressTtl { get; set; }
        public int? BatchNumMessages { get; set; }
        public bool EnableIdempotence { get; set; }
        public int? MaxInFlight { get; set; }
        public int? MessageSendMaxRetries { get; set; }
        public int? BatchSize { get; set; }
    }
}