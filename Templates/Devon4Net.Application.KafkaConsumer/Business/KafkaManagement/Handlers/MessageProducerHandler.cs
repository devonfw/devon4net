using Devon4Net.Infrastructure.Kafka.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Application.KafkaConsumer.Business.KafkaManagement.Handlers
{
    public class MessageProducerHandler : KafkaProducerHandler<string,string>
    {
        public MessageProducerHandler(IServiceCollection services, IKakfkaHandler kafkaHandler, string producerId) : base(services, kafkaHandler, producerId)
        {
        }
    }
}
