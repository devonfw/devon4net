using System.Linq;
using System.Threading.Tasks;
using Confluent.Kafka;
using Devon4Net.Infrastructure.Common.Options.Kafka;
using Devon4Net.Infrastructure.Kafka.Common.Const;
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

        public IProducer<T, TV> GetProducerBuilder<T,TV>(string clientId) where T : class where TV : class
        {
            var producerOptions = KafkaOptions.Producers.FirstOrDefault(p => p.ClientId == clientId);
            var configuration = GetDefaultKafkaConfiguration(producerOptions);

            return new ProducerBuilder<T,TV>(configuration).Build();
        }

        public async Task<DeliveryResult<T, TV>> DeliverMessage<T, TV>(T key, TV value, string clientId, string topicName) where T : class where TV : class
        {
            DeliveryResult<T, TV> result;
            using var producer = GetProducerBuilder<T, TV>(clientId);
            try
            {
                result = await producer.ProduceAsync(topicName, new Message<T, TV> { Key = key, Value = value }).ConfigureAwait(false);
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
                CompressionLevel = producer.CompressionLevel ?? KafkaDefaultValues.CompressionLevel,
                CompressionType = GetCompressionType(producer.CompressionType),
                EnableSslCertificateVerification = producer.EnableSslCertificateVerification,
                CancellationDelayMaxMs = producer.CancellationDelayMaxMs ?? KafkaDefaultValues.CancellationDelayMaxMs,
                Acks = GetAck(producer.Ack),
                Debug = producer.Debug,
                BrokerAddressTtl = producer.BrokerAddressTtl ?? KafkaDefaultValues.BrokerAddressTtl,
                BatchNumMessages = producer.BatchNumMessages ?? KafkaDefaultValues.BatchNumMessages,
                EnableIdempotence = producer.EnableIdempotence,
                MaxInFlight = producer.MaxInFlight ?? KafkaDefaultValues.MaxInFlight,
                MessageSendMaxRetries = producer.MessageSendMaxRetries ?? KafkaDefaultValues.MessageSendMaxRetries,
                BatchSize = producer.BatchSize ?? KafkaDefaultValues.BatchSize,
                MessageMaxBytes = producer.MessageMaxBytes ?? KafkaDefaultValues.MessageMaxBytes,
                ReceiveMessageMaxBytes = producer.ReceiveMessageMaxBytes ?? KafkaDefaultValues.ReceiveMessageMaxBytes
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
