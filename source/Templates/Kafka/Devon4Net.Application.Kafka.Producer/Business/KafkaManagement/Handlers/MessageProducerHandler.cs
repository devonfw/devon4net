using Devon4Net.Application.Kafka.Producer.Business.FileManagement.Dto;
using Devon4Net.Infrastructure.Kafka.Handlers.Producer;
using Devon4Net.Infrastructure.Kafka.Options;

namespace Devon4Net.Application.Kafka.Producer.Business.KafkaManagement.Handlers
{
    public class MessageProducerHandler : KafkaProducerHandler<string, DataPieceDto<byte[]>>
    {
        public MessageProducerHandler(IServiceCollection services, KafkaOptions kafkaOptions, string producerId) : base(services, kafkaOptions, producerId)
        {
        }
    }
}
