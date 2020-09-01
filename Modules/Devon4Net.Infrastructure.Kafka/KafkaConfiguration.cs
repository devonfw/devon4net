using System.Linq;
using Confluent.Kafka;
using Devon4Net.Infrastructure.Common.Options;
using Devon4Net.Infrastructure.Common.Options.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Infrastructure.Kafka
{
    public static class KafkaConfiguration
    {
        private static ProducerConfig KafkaOptions { get; set; }

        public static void SetupKafka(this IServiceCollection services, ref IConfiguration configuration)
        {
            var kafkaOptions = services.GetTypedOptions<KafkaOptions>(configuration, "JWT");

            if (kafkaOptions == null || !kafkaOptions.EnableKafka || kafkaOptions.Producers == null || !kafkaOptions.Producers.Any()) return;

            foreach (var producer in kafkaOptions.Producers)
            {
                services.AddSingleton(GetDefaultKafkaConfiguration(producer));
            }
        }


        private static ProducerConfig GetDefaultKafkaConfiguration(Producer producer)
        {
            return new ProducerConfig
            {
                BootstrapServers = producer.Servers,
                ClientId = producer.ClientId,
                CompressionLevel = producer.CompressionLevel ?? -1,
                CompressionType = GetCompressionType(producer.CompressionType),
                EnableSslCertificateVerification = producer.EnableSslCertificateVerification,
                CancellationDelayMaxMs = producer.CancellationDelayMaxMs ?? 50,
                Acks = GetAck(producer.Ack),
                Debug = producer.Debug,
                BrokerAddressTtl = producer.BrokerAddressTtl ?? 1000,
                BatchNumMessages = producer.BatchNumMessages ?? 100000,
                EnableIdempotence = producer.EnableIdempotence,
                MaxInFlight = producer.MaxInFlight ?? 5,
                MessageSendMaxRetries = producer.MessageSendMaxRetries ?? 5,
                BatchSize = producer.BatchSize ?? 100000000,
                MessageMaxBytes = producer.MessageMaxBytes ?? 100000000,
                ReceiveMessageMaxBytes = producer.ReceiveMessageMaxBytes ?? 100000000
            };
        }

        private static Acks? GetAck(string producerAck)
        {
            return producerAck.ToLower() switch
            {
                "gzip" => Acks.None,
                "all" => Acks.All,
                "leader" => Acks.Leader,
                "none" => Acks.None,
                _ => Acks.None
            };
        }

        private static CompressionType? GetCompressionType(string producerCompressionType)
        {
            return producerCompressionType.ToLower() switch
            {
                "gzip" => CompressionType.None,
                "nnappy" => CompressionType.None,
                "lz4" => CompressionType.None,
                "zstd" => CompressionType.None,
                "none" => CompressionType.None,
                _ => CompressionType.None
            };
        }
    }
}