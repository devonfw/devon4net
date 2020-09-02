using System.Linq;
using System.Threading.Tasks;
using Confluent.Kafka;
using Devon4Net.Infrastructure.Common.Options.Kafka;
using Devon4Net.Infrastructure.Kafka.Common.Const;
using Devon4Net.Infrastructure.Kafka.Exceptions;
using Devon4Net.Infrastructure.Log;

namespace Devon4Net.Infrastructure.Kafka.Handlers
{
    public class KakfkaHandler : IKakfkaHandler
    {
        private KafkaOptions KafkaOptions { get; }

        public KakfkaHandler(KafkaOptions kafkaOptions)
        {
            KafkaOptions = kafkaOptions;
        }

        #region Producer

        public IProducer<T, TV> GetProducerBuilder<T, TV>(string producerId) where T : class where TV : class
        {
            if (string.IsNullOrEmpty(producerId))
            {
                throw new ProducerNotFoundException($"The producerId param can not be null or empty");
            }

            var producerOptions = KafkaOptions.Producers.FirstOrDefault(p => p.ProducerId == producerId);

            if (producerOptions == null)
            {
                throw new ConsumerNotFoundException($"Could not find producer configuration with ConsumerId {producerId}");
            }

            var configuration = GetDefaultKafkaProducerConfiguration(producerOptions);

            var producer = new ProducerBuilder<T, TV>(configuration);

            producer.SetErrorHandler((_, e) => Devon4NetLogger.Error(new ConsumerException($"Error code {e.Code} : {e.Reason}")));
            producer.SetStatisticsHandler((_, json) => Devon4NetLogger.Information($"Statistics: {json}"));
            producer.SetLogHandler((c, partitions) =>
            {
                Devon4NetLogger.Information($"Kafka log handler: [{string.Join(", ", partitions)}]");
            });

            var result = producer.Build();

            return result;
        }

        private static ProducerConfig GetDefaultKafkaProducerConfiguration(Producer producer)
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
        #endregion

        #region Consumer

        public IConsumer<T, TV> GetConsumerBuilder<T, TV>(string consumerId) where T : class where TV : class
        {
            if (string.IsNullOrEmpty(consumerId))
            {
                throw new ConsumerNotFoundException($"The consumerId param can not be null or empty");
            }

            var consumerOptions = KafkaOptions.Consumers.FirstOrDefault(p => p.ConsumerId == consumerId);

            if (consumerOptions == null)
            {
                throw new ConsumerNotFoundException($"Could not find consumer configuration with ConsumerId {consumerId}");
            }

            var configuration = GetDefaultKafkaConsumerConfiguration(consumerOptions);

            var consumer = new ConsumerBuilder<T, TV>(configuration);

            consumer.SetErrorHandler((_, e) => Devon4NetLogger.Error(new ConsumerException($"Error code {e.Code} : {e.Reason}")));
            consumer.SetStatisticsHandler((_, json) => Devon4NetLogger.Information($"Statistics: {json}"));
            consumer.SetPartitionsAssignedHandler((c, partitions) =>
            {
                Devon4NetLogger.Information($"Assigned partitions: [{string.Join(", ", partitions)}]");
            });
            consumer.SetPartitionsRevokedHandler((c, partitions) =>
            {
                Devon4NetLogger.Information($"Revoking assignment: [{string.Join(", ", partitions)}]");
            });

            var result = consumer.Build();
            if (!string.IsNullOrEmpty(consumerOptions.Topics)) result.Subscribe(consumerOptions.GetTopics());

            return result;
        }

        private static ConsumerConfig GetDefaultKafkaConsumerConfiguration(Consumer consumer)
        {
            return new ConsumerConfig
            {
                BootstrapServers = consumer.Servers,
                ClientId = consumer.ClientId,
                GroupId = consumer.GroupId,
                EnableAutoCommit = consumer.AutoCommit,
                StatisticsIntervalMs = consumer.StatisticsIntervalMs ?? KafkaDefaultValues.StatisticsIntervalMs,
                SessionTimeoutMs = consumer.SessionTimeoutMs ?? KafkaDefaultValues.SessionTimeoutMs,
                AutoOffsetReset = GetAutoOffsetReset(consumer.AutoOffsetReset),
                EnablePartitionEof = consumer.EnablePartitionEof,
                IsolationLevel = GetIsolationLevel(consumer.IsolationLevel),
                EnableSslCertificateVerification = consumer.EnableSslCertificateVerification
            };
        }

        #endregion

        #region Converters

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

        private static AutoOffsetReset? GetAutoOffsetReset(string consumerAutoOffsetReset)
        {
            return consumerAutoOffsetReset.ToLower() switch
            {
                "latest" => AutoOffsetReset.Latest,
                "earliest" => AutoOffsetReset.Earliest,
                "error" => AutoOffsetReset.Error,
                _ => AutoOffsetReset.Latest
            };
        }

        private static IsolationLevel GetIsolationLevel(string isolationLevel)
        {
            return isolationLevel.ToLower() switch
            {
                "readuncommitted" => IsolationLevel.ReadUncommitted,
                "readcommitted" => IsolationLevel.ReadCommitted,
                _ => IsolationLevel.ReadCommitted
            };
        }

        #endregion
    }
}
