using System.Linq;
using Confluent.Kafka;
using Devon4Net.Infrastructure.Common.Options;
using Devon4Net.Infrastructure.Common.Options.Kafka;
using Devon4Net.Infrastructure.Kafka.Handlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Infrastructure.Kafka
{
    public static class KafkaConfiguration
    {
        private static ProducerConfig KafkaOptions { get; set; }

        public static void SetupKafka(this IServiceCollection services, ref IConfiguration configuration)
        {
            var kafkaOptions = services.GetTypedOptions<KafkaOptions>(configuration, "Kafka");

            if (kafkaOptions == null || !kafkaOptions.EnableKafka || kafkaOptions.Producers == null || !kafkaOptions.Producers.Any()) return;

            services.AddTransient(typeof(IKakfkaHandler), typeof(KakfkaHandler));
        }
    }
}