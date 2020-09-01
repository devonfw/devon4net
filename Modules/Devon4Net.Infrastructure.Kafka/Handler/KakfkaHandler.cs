using System.Linq;
using System.Threading.Tasks;
using Confluent.Kafka;
using Devon4Net.Infrastructure.Common.Options.Kafka;
using Devon4Net.Infrastructure.Log;

namespace Devon4Net.Infrastructure.Kafka.Handler
{
    public class KakfkaHandler : IKakfkaHandler
    {
        private KafkaOptions KafkaOptions { get; }

        public KakfkaHandler(KafkaOptions kafkaOptions)
        {
            KafkaOptions = kafkaOptions;
        }

        public IProducer<T, V> GetProducerBuilder<T,V>(string clientId) where T : class where V : class
        {
            var producerOptions = KafkaOptions.Producers.FirstOrDefault(p => p.ClientId == clientId);
            var configuration = GetDefaultKafkaConfiguration(producerOptions);

            return new ProducerBuilder<T,V>(configuration).Build();
        }

        public async Task<DeliveryResult<T, V>> DeliverMessage<T, V>(T key, V value, string clientId, string topicName) where T : class where V : class
        {
            DeliveryResult<T, V> result;
            using var producer = GetProducerBuilder<T, V>(clientId);
            try
            {
                result = await producer.ProduceAsync(topicName, new Message<T, V> { Key = key, Value = value }).ConfigureAwait(false);
                producer.Flush();
                producer.Dispose();
            }
            catch (ProduceException<string, string> e)
            {
                Devon4NetLogger.Error(e);
                throw;
            }

            return result;
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
