using Confluent.Kafka;
using Devon4Net.Infrastructure.Kafka.Common.Const;
using Devon4Net.Infrastructure.Kafka.Common.Converters;
using Devon4Net.Infrastructure.Kafka.Exceptions;
using Devon4Net.Infrastructure.Kafka.Options;
using Devon4Net.Infrastructure.Kafka.Serialization;
using Devon4Net.Infrastructure.Logger.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Devon4Net.Infrastructure.Kafka.Handlers.Consumer
{
    public abstract class KafkaConsumerHandler<TKey, TValue> : KafkaHandler, IKafkaConsumerHandler<TKey, TValue> where TKey : class where TValue : class
    {
        private bool EnableConsumerFlag { get; set; }
        private bool Commit { get; }
        private int CommitPeriod { get; }
        public abstract void HandleCommand(TKey key, TValue value);

        protected KafkaConsumerHandler(IServiceCollection services, KafkaOptions kafkaOptions, string consumerId, bool commit = false, int commitPeriod = 5, bool enableConsumerFlag = true) : base(services, kafkaOptions, consumerId)
        {
            Commit = commit;
            CommitPeriod = commitPeriod;
            EnableConsumerFlag = enableConsumerFlag;
            if(EnableConsumerFlag) Consume(Commit, CommitPeriod);
        }

        public void EnableConsumer(bool startConsumer = true)
        {
            EnableConsumerFlag = true;
            Devon4NetLogger.Debug("The EnableConsumerFlag is set to true");
            if (startConsumer) Consume(Commit, CommitPeriod);
        }

        public void DisableConsumer()
        {
            EnableConsumerFlag = false;
            Devon4NetLogger.Debug("The EnableConsumerFlag is set to false");
        }

        /// <summary>
        /// If commit is set to true, the Commit method sends a "commit offsets" request to the Kafka cluster and synchronously waits for the response.
        /// This is very slow compared to the rate at which the consumer is capable of consuming messages.
        /// A high performance application will typically commit offsets relatively infrequently and be designed handle duplicate messages in the event of failure.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TV"></typeparam>
        /// <param name="commit"></param>
        /// <param name="commitPeriod"></param>
        private void Consume(bool commit, int commitPeriod)
        {
            var cancellationToken = new CancellationTokenSource();

            Task.Run(() =>
            {
                try
                {
                    using var consumer = GetConsumerBuilder(HandlerId);
                    while (EnableConsumerFlag)
                    {
                        var consumeResult = consumer?.Consume(cancellationToken.Token);
                        if (consumeResult?.Message == null) continue;

                        HandleCommand(consumeResult.Message.Key, consumeResult.Message.Value);

                        if (consumeResult.IsPartitionEOF)
                        {
                            Devon4NetLogger.Information($"Reached end of topic {consumeResult.Topic}, partition {consumeResult.Partition}, offset {consumeResult.Offset}.");
                            continue;
                        }

                        Devon4NetLogger.Debug($"Received message at {consumeResult.TopicPartitionOffset}: {consumeResult.Message.Value}");

                        if (!commit || consumeResult.Offset % commitPeriod != 0) continue;

                        consumer?.Commit(consumeResult);
                        consumer?.Close();
                    }
                }
                catch (Exception ex)
                {
                    Devon4NetLogger.Error(ex);
                }
            }, cancellationToken.Token);
        }

        #region ConsumerConfiguration
        public IConsumer<TKey, TValue> GetConsumerBuilder(string consumerId)
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
            var consumer = new ConsumerBuilder<TKey, TValue>(configuration);
            consumer.SetValueDeserializer(new DefaultKafkaDeserializer<TValue>());

            IConsumer<TKey, TValue> result = null;

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

        private static ConsumerConfig GetDefaultKafkaConsumerConfiguration(ConsumerOptions consumer)
        {
            var result = new ConsumerConfig
            {
                BootstrapServers = consumer.Servers,
                ClientId = consumer.ClientId,
                GroupId = consumer.GroupId,
                EnableAutoCommit = consumer.AutoCommit,
                StatisticsIntervalMs = consumer.StatisticsIntervalMs ?? KafkaDefaultValues.StatisticsIntervalMs,
                SessionTimeoutMs = consumer.SessionTimeoutMs ?? KafkaDefaultValues.SessionTimeoutMs,
                AutoOffsetReset = KafkaConverters.GetAutoOffsetReset(consumer.AutoOffsetReset),
                EnablePartitionEof = consumer.EnablePartitionEof,
                IsolationLevel = KafkaConverters.GetIsolationLevel(consumer.IsolationLevel),
                EnableSslCertificateVerification = consumer.EnableSslCertificateVerification,
            };

            if (!string.IsNullOrEmpty(consumer.Debug))
            {
                result.Debug = consumer.Debug;
            }

            return result;
        }
        #endregion
    }
}
