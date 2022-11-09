using Confluent.Kafka;
using Devon4Net.Infrastructure.Common.Constants;
using Devon4Net.Infrastructure.Common.Handlers;
using Devon4Net.Infrastructure.Kafka.Handlers.Administration;
using Devon4Net.Infrastructure.Kafka.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Streamiz.Kafka.Net.SerDes;

namespace Devon4Net.Infrastructure.Kafka
{
    public static class KafkaConfiguration
    {
        private static KafkaOptions KafkaOptions;

        public static void SetupKafka(this IServiceCollection services, IConfiguration configuration)
        {
            KafkaOptions = services.GetTypedOptions<KafkaOptions>(configuration, OptionsDefinition.Kafka);

            if (KafkaOptions?.EnableKafka != true || KafkaOptions.Administration?.Any() != true) return;

            services.AddTransient(typeof(IKafkaAdministrationHandler), typeof(KafkaAdministrationHandler));
        }

        public static void AddKafkaConsumer<T, TKey, TValue>(this IServiceCollection services, string consumerId, IDeserializer<TKey> keyDeserializer = null, IDeserializer<TValue> valueDeserializer = null, bool commit = false, int commitPeriod = 5, bool enableConsumerFlag = true) where T : class
        {
            var memberInfo = typeof(T).BaseType;
            if (memberInfo?.Name.Contains("KafkaConsumerHandler") == false)
            {
                throw new ArgumentException($"The provided type {typeof(T).FullName} does not inherit from KafkaConsumerHandler");
            }

            var instance = (T) Activator.CreateInstance(typeof(T), services, KafkaOptions, consumerId, keyDeserializer, valueDeserializer, commit, commitPeriod, enableConsumerFlag);

            services.AddSingleton(instance);
        }

        public static void AddKafkaProducer<T, TKey, TValue>(this IServiceCollection services, string producerId, ISerializer<TKey> keySerializer = null, ISerializer<TValue> valueSerializer = null) where T : class
        {
            var memberInfo = typeof(T).BaseType;
            if (memberInfo?.Name.Contains("KafkaProducerHandler") == false)
            {
                throw new ArgumentException($"The provided type {typeof(T).FullName} does not inherit from KafkaProducerHandler");
            }

            var instance = (T) Activator.CreateInstance(typeof(T), services, KafkaOptions, producerId, keySerializer, valueSerializer);
            services.AddSingleton(instance);
        }

        public static void AddKafkaStreamService<T, TKey, TValue>(this IServiceCollection services, string applicationId, ISerDes<TKey> keySerDes = null, ISerDes<TValue> valueSerDes = null) where T : BackgroundService
        {
            var memberInfo = typeof(T).BaseType;
            if (memberInfo?.Name.Contains("KafkaStreamService") == false)
            {
                throw new ArgumentException($"The provided type {typeof(T).FullName} does not inherit from KafkaStreamService");
            }

            var instance = (T)Activator.CreateInstance(typeof(T), services, KafkaOptions, applicationId, keySerDes, valueSerDes);
            services.AddHostedService(_ => instance);
        }

    }
}