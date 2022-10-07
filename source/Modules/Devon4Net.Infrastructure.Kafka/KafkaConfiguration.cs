using Devon4Net.Infrastructure.Common.Handlers;
using Devon4Net.Infrastructure.Kafka.Handlers.Administration;
using Devon4Net.Infrastructure.Kafka.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Devon4Net.Infrastructure.Kafka
{
    public static class KafkaConfiguration
    {
        private static KafkaOptions KafkaOptions { get; set; }

        public static void SetupKafka(this IServiceCollection services, IConfiguration configuration)
        {
            KafkaOptions = services.GetTypedOptions<KafkaOptions>(configuration, "Kafka");

            if (KafkaOptions?.EnableKafka != true || KafkaOptions.Administration?.Any() != true) return;

            services.AddTransient(typeof(IKafkaAdministrationHandler), typeof(KafkaAdministrationHandler));
        }

        public static void AddKafkaConsumer<T>(this IServiceCollection services, IConfiguration configuration, string consumerId, bool commit = false, int commitPeriod = 5, bool enableConsumerFlag = true) where T : class
        {
            var memberInfo = typeof(T).BaseType;
            if (memberInfo?.Name.Contains("KafkaConsumerHandler") == false)
            {
                throw new ArgumentException($"The provided type {typeof(T).FullName} does not inherit from KafkaConsumerHandler");
            }

            //var kafkaOptions = services.GetKafkaOptions();

            var instance = (T) Activator.CreateInstance(typeof(T), services, KafkaOptions, consumerId, commit, commitPeriod, enableConsumerFlag);

            services.AddSingleton(instance);
        }

        public static void AddKafkaProducer<T>(this IServiceCollection services, IConfiguration configuration, string producerId) where T : class
        {
            var memberInfo = typeof(T).BaseType;
            if (memberInfo?.Name.Contains("KafkaProducerHandler") == false)
            {
                throw new ArgumentException($"The provided type {typeof(T).FullName} does not inherit from KafkaProducerHandler");
            }

            //var kafkaOptions = services.GetKafkaOptions();
            var instance = (T) Activator.CreateInstance(typeof(T), services, KafkaOptions, producerId);
            services.AddSingleton(instance);
        }

        public static void AddKafkaStreamService<T>(this IServiceCollection services, IConfiguration configuration, string applicationId) where T : BackgroundService
        {
            var memberInfo = typeof(T).BaseType;

            if (memberInfo?.Name.Contains("KafkaStreamService") == false)
            {
                throw new ArgumentException($"The provided type {typeof(T).FullName} does not inherit from KafkaStreamService");
            }

            //var kafkaOptions = services.GetKafkaOptions();
            var instance = (T)Activator.CreateInstance(typeof(T), services, KafkaOptions, applicationId);
            services.AddHostedService(_ => instance);
        }

        private static KafkaOptions GetKafkaOptions(this IServiceCollection services)
        {
            using var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetService<IOptions<KafkaOptions>>()?.Value;
        }
    }
}