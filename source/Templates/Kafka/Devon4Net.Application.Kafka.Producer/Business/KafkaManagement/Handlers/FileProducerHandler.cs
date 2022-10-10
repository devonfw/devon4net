using Confluent.Kafka;
using Devon4Net.Application.Kafka.Producer.Business.FileManagement.Dto;
using Devon4Net.Infrastructure.Kafka.Handlers.Producer;
using Devon4Net.Infrastructure.Kafka.Options;

namespace Devon4Net.Application.Kafka.Producer.Business.KafkaManagement.Handlers
{
    public class FileProducerHandler : KafkaProducerHandler<string, DataPieceDto<byte[]>>
    {
        public FileProducerHandler(IServiceCollection services, KafkaOptions kafkaOptions, string producerId, ISerializer<string> keySerializer = null, ISerializer<DataPieceDto<byte[]>> valueSerializer = null) : base(services, kafkaOptions, producerId, keySerializer, valueSerializer)
        {
        }
    }
}
