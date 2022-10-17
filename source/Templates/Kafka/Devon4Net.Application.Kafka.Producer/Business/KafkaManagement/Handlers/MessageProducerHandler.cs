using Confluent.Kafka;
using Devon4Net.Infrastructure.Kafka.Handlers.Producer;
using Devon4Net.Infrastructure.Kafka.Options;

namespace Devon4Net.Application.Kafka.Producer.Business.KafkaManagement.Handlers
{
    public class MessageProducerHandler : KafkaProducerHandler<string, string>
    {
        public MessageProducerHandler(IServiceCollection services, KafkaOptions kafkaOptions, string producerId, ISerializer<string> keySerializer = null, ISerializer<string> valueSerializer = null) : base(services, kafkaOptions, producerId, keySerializer, valueSerializer)
        {
        }
    }
}
