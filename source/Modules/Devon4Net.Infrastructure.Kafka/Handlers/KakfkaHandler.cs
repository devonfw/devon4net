using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Devon4Net.Infrastructure.Kafka.Common.Const;
using Devon4Net.Infrastructure.Kafka.Exceptions;
using Devon4Net.Infrastructure.Kafka.Options;
using Devon4Net.Infrastructure.Logger.Logging;
using Microsoft.Extensions.Options;

namespace Devon4Net.Infrastructure.Kafka.Handlers
{
    public class KakfkaHandler : IKakfkaHandler
    {
        private KafkaOptions KafkaOptions { get; }

        public KakfkaHandler(IOptions<KafkaOptions> kafkaOptions)
        {
            KafkaOptions = kafkaOptions?.Value;
        }

        #region Producer
        public async Task<DeliveryResult<T, TV>> DeliverMessage<T, TV>(T key, TV value, string producerId) where T : class where TV : class
        {
            DeliveryResult<T, TV> result;
            var producerOptions = GetProducerOptions(producerId);
            using var producer = GetProducerBuilder<T, TV>(producerId);
            try
            {
                result = await producer.ProduceAsync(producerOptions.Topic, new Message<T, TV> { Key = key, Value = value }).ConfigureAwait(false);
            }
            catch (ProduceException<string, string> e)
            {
                Devon4NetLogger.Error(e);
                throw;
            }
            finally
            {
                producer?.Flush();
                producer?.Dispose();
            }

            return result;
        }

        public IProducer<T, TV> GetProducerBuilder<T, TV>(string producerId) where T : class where TV : class
        {
            var producerOptions = GetProducerOptions(producerId);

            var configuration = GetDefaultKafkaProducerConfiguration(producerOptions);

            var producer = GetProducerBuilderInstance<T, TV>(configuration);

            var result = producer.Build();

            return result;
        }

        private static ProducerBuilder<T, TV> GetProducerBuilderInstance<T, TV>(ProducerConfig configuration) where T : class where TV : class
        {
            var producer = new ProducerBuilder<T, TV>(configuration);

            producer = producer.SetErrorHandler((_, e) => Devon4NetLogger.Error(new ConsumerException($"Error code {e.Code} : {e.Reason}")));
            producer = producer.SetStatisticsHandler((_, json) => Devon4NetLogger.Information($"Statistics: {json}"));
            producer = producer.SetLogHandler((_, partitions) => Devon4NetLogger.Information($"Kafka log handler: [{string.Join(", ", partitions)}]"));
            return producer;
        }

        private Producer GetProducerOptions(string producerId)
        {
            if (string.IsNullOrEmpty(producerId))
            {
                throw new ProducerNotFoundException("The producerId param can not be null or empty");
            }

            var producerOptions = KafkaOptions.Producers.Find(p => p.ProducerId == producerId);

            if (producerOptions == null)
            {
                throw new ConsumerNotFoundException($"Could not find producer configuration with ConsumerId {producerId}");
            }

            return producerOptions;
        }

        private static ProducerConfig GetDefaultKafkaProducerConfiguration(Producer producer)
        {
            var result =  new ProducerConfig
            {
                BootstrapServers = producer.Servers,
                ClientId = producer.ClientId,
                CompressionLevel = producer.CompressionLevel ?? KafkaDefaultValues.CompressionLevel,
                CompressionType = GetCompressionType(producer.CompressionType),
                EnableSslCertificateVerification = producer.EnableSslCertificateVerification,
                CancellationDelayMaxMs = producer.CancellationDelayMaxMs ?? KafkaDefaultValues.CancellationDelayMaxMs,
                Acks = GetAck(producer.Ack),
                BrokerAddressTtl = producer.BrokerAddressTtl ?? KafkaDefaultValues.BrokerAddressTtl,
                BatchNumMessages = producer.BatchNumMessages ?? KafkaDefaultValues.BatchNumMessages,
                EnableIdempotence = producer.EnableIdempotence,
                MaxInFlight = producer.MaxInFlight ?? KafkaDefaultValues.MaxInFlight,
                MessageSendMaxRetries = producer.MessageSendMaxRetries ?? KafkaDefaultValues.MessageSendMaxRetries,
                BatchSize = producer.BatchSize ?? KafkaDefaultValues.BatchSize,
                MessageMaxBytes = producer.MessageMaxBytes ?? KafkaDefaultValues.MessageMaxBytes,
                ReceiveMessageMaxBytes = producer.ReceiveMessageMaxBytes ?? KafkaDefaultValues.ReceiveMessageMaxBytes
            };

            if (!string.IsNullOrEmpty(producer.Debug))
            {
                result.Debug = producer.Debug;
            }

            return result;
        }

        #endregion

        #region Consumer

        public IConsumer<T, TV> GetConsumerBuilder<T, TV>(string consumerId) where T : class where TV : class
        {
            if (string.IsNullOrEmpty(consumerId))
            {
                throw new ConsumerNotFoundException("The consumerId param can not be null or empty");
            }

            var consumerOptions = KafkaOptions.Consumers.Find(p => p.ConsumerId == consumerId);

            if (consumerOptions == null)
            {
                throw new ConsumerNotFoundException($"Could not find consumer configuration with ConsumerId {consumerId}");
            }

            var configuration = GetDefaultKafkaConsumerConfiguration(consumerOptions);

            var consumer = new ConsumerBuilder<T, TV>(configuration);

            IConsumer<T, TV> result = null;

            try
            {
                consumer.SetErrorHandler((_, e) => Devon4NetLogger.Error(new ConsumerException($"Error code {e.Code} : {e.Reason}")));
                consumer.SetStatisticsHandler((_, json) => Devon4NetLogger.Information($"Statistics: {json}"));
                consumer.SetPartitionsAssignedHandler((_, partitions) => Devon4NetLogger.Information($"Assigned partitions: [{string.Join(", ", partitions)}]"));
                consumer.SetPartitionsRevokedHandler((_, partitions) => Devon4NetLogger.Information($"Revoking assignment: [{string.Join(", ", partitions)}]"));

                result = consumer.Build();
                if (!string.IsNullOrEmpty(consumerOptions.Topics)) result.Subscribe(consumerOptions.GetTopics());
            }
            catch (InvalidOperationException ex)
            {
                Devon4NetLogger.Error(ex);
            }

            return result;
        }

        private static ConsumerConfig GetDefaultKafkaConsumerConfiguration(Consumer consumer)
        {
            var result =  new ConsumerConfig
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
                EnableSslCertificateVerification = consumer.EnableSslCertificateVerification,
            };

            if (!string.IsNullOrEmpty(consumer.Debug))
            {
                result.Debug = consumer.Debug;
            }

            return result;
        }

        #endregion

        #region Admin
        public async Task<bool> CreateTopic(string adminId, string topicName, short replicationFactor = 1, int numPartitions = 1)
        {
            using var adminClient = GetAdminClientBuilder(adminId);
            try
            {
                await adminClient.CreateTopicsAsync(new[] {new TopicSpecification { Name = topicName, ReplicationFactor = replicationFactor, NumPartitions = numPartitions } }).ConfigureAwait(false);
                return true;
            }
            catch (CreateTopicsException ex)
            {
                Devon4NetLogger.Error($"An error occured creating topic {ex.Results[0].Topic}: {ex.Results[0].Error.Reason}");
                Devon4NetLogger.Error(ex);
                throw;
            }
        }

        public async Task<bool> DeleteTopic(string adminId, List<string> topicsName)
        {
            using var adminClient = GetAdminClientBuilder(adminId);
            try
            {
                await adminClient.DeleteTopicsAsync(topicsName).ConfigureAwait(false);
                return true;
            }
            catch (CreateTopicsException ex)
            {
                Devon4NetLogger.Error($"An error occured creating topic {ex.Results[0].Topic}: {ex.Results[0].Error.Reason}");
                Devon4NetLogger.Error(ex);
                throw;
            }
        }

        private IAdminClient GetAdminClientBuilder(string adminId)
        {
            var adminOptions = GetAdminOptions(adminId);
            var adminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = adminOptions.Servers }).Build();
            return adminClient;
        }

        private Administration GetAdminOptions(string adminId)
        {
            if (string.IsNullOrEmpty(adminId))
            {
                throw new AdminNotFoundException("The adminId param can not be null or empty");
            }

            var adminOptions = KafkaOptions.Administration.Find(p => p.AdminId == adminId);

            if (adminOptions == null)
            {
                throw new AdminNotFoundException($"Could not find admin configuration with ConsumerId {adminId}");
            }

            return adminOptions;
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
                "gzip" => CompressionType.Gzip,
                "snappy" => CompressionType.Snappy,
                "lz4" => CompressionType.Lz4,
                "zstd" => CompressionType.Zstd,
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
