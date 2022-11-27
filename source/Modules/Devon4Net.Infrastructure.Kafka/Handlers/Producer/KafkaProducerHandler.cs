using Confluent.Kafka;
using Devon4Net.Infrastructure.Kafka.Common.Const;
using Devon4Net.Infrastructure.Kafka.Common.Converters;
using Devon4Net.Infrastructure.Kafka.Exceptions;
using Devon4Net.Infrastructure.Kafka.Options;
using Devon4Net.Infrastructure.Kafka.Serialization;
using Devon4Net.Infrastructure.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Infrastructure.Kafka.Handlers.Producer
{
    public class KafkaProducerHandler<TKey, TValue> : KafkaHandler, IKafkaProducerHandler<TKey, TValue> where TKey : class where TValue : class
    {
        private readonly ISerializer<TKey> KeySerializer;
        private readonly ISerializer<TValue> ValueSerializer;

        public KafkaProducerHandler(IServiceCollection services, KafkaOptions kafkaOptions, string producerId, ISerializer<TKey> keySerializer = null, ISerializer<TValue> valueSerializer = null) : base(services, kafkaOptions, producerId)
        {
            KeySerializer = keySerializer;
            ValueSerializer = valueSerializer;
        }

        public Task<DeliveryResult<TKey, TValue>> SendMessage(TKey key, TValue value)
        {
            var result = DeliverMessage(key, value, HandlerId);
            var date = result.Result.Timestamp.UtcDateTime;
            Devon4NetLogger.Information($"Message delivered. Key: {result.Result.Key} | Value : {result.Result.Value} | Topic: {result.Result.Topic} | UTC TimeStamp : {date.ToShortDateString()}-{date.ToLongTimeString()} | Status: {result.Result.Status}");
            return result;
        }

        public async Task<DeliveryResult<TKey, TValue>> DeliverMessage(TKey key, TValue value, string producerId)
        {
            DeliveryResult<TKey, TValue> result;
            var producerOptions = GetProducerOptions(producerId);
            using var producer = GetProducerBuilder(producerId);
            try
            {
                result = await producer.ProduceAsync(producerOptions.Topic, new Message<TKey, TValue> { Key = key, Value = value }).ConfigureAwait(false);
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

        #region ProcucerConfiguration
        public IProducer<TKey, TValue> GetProducerBuilder(string producerId) 
        {
            var producerOptions = GetProducerOptions(producerId);

            var configuration = GetDefaultKafkaProducerConfiguration(producerOptions);
            var producer = GetProducerBuilderInstance(configuration);

            producer.SetKeySerializer(KeySerializer ?? GetSerializerForType<TKey>());
            producer.SetValueSerializer(ValueSerializer ?? GetSerializerForType<TValue>());

            var result = producer.Build();

            return result;
        }

        private static ProducerBuilder<TKey, TValue> GetProducerBuilderInstance(ProducerConfig configuration) 
        {
            var producer = new ProducerBuilder<TKey, TValue>(configuration);
            
            producer = producer.SetErrorHandler((_, e) => Devon4NetLogger.Error(new ConsumerException($"Error code {e.Code} : {e.Reason}")));
            producer = producer.SetStatisticsHandler((_, json) => Devon4NetLogger.Information($"Statistics: {json}"));
            producer = producer.SetLogHandler((_, partitions) => Devon4NetLogger.Information($"Kafka log handler: [{string.Join(", ", partitions)}]"));
            return producer;
        }

        private static ISerializer<T> GetSerializerForType<T>()
        {
            var type = typeof(T);

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.String:
                    return (ISerializer<T>) Serializers.Utf8;
                case TypeCode.Double:
                    return (ISerializer<T>) Serializers.Double;
                case TypeCode.Single:
                    return (ISerializer<T>) Serializers.Single;
                case TypeCode.Int32:
                    return (ISerializer<T>) Serializers.Int32;
                case TypeCode.Int64:
                    return (ISerializer<T>) Serializers.Int64;
                case TypeCode.Object:
                    return type == typeof(byte[])
                        ? (ISerializer<T>)Serializers.ByteArray
                        : new DefaultKafkaSerializer<T>();
                default:
                    return new DefaultKafkaSerializer<T>();
            }
        }

        private ProducerOptions GetProducerOptions(string producerId)
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

        private static ProducerConfig GetDefaultKafkaProducerConfiguration(ProducerOptions producer)
        {
            var result = new ProducerConfig
            {
                BootstrapServers = producer.Servers,
                ClientId = producer.ClientId,
                CompressionLevel = producer.CompressionLevel ?? KafkaDefaultValues.CompressionLevel,
                CompressionType = KafkaConverters.GetCompressionType(producer.CompressionType),
                EnableSslCertificateVerification = producer.EnableSslCertificateVerification,
                CancellationDelayMaxMs = producer.CancellationDelayMaxMs ?? KafkaDefaultValues.CancellationDelayMaxMs,
                Acks = KafkaConverters.GetAck(producer.Ack),
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
    }
}