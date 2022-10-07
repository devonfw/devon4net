using Devon4Net.Application.Kafka.Business.Messages;
using Devon4Net.Infrastructure.Kafka.Options;
using Devon4Net.Infrastructure.Kafka.Streams.Services;

namespace Devon4Net.Application.Kafka.Business.KafkaManagement.Services
{
    public class FileTransferStreamService : KafkaStreamService<string, DataPieceDto<byte[]>>
    {
        public FileTransferStreamService(IServiceCollection services, KafkaOptions kafkaOptions, string applicationId) : base(services, kafkaOptions, applicationId)
        {
        }

        public override void CreateStreamBuilder()
        {
            StreamBuilder.Stream<string, DataPieceDto<byte[]>>("input")
               .Peek((k, v) => Console.WriteLine($"Stream says -> File: {v.FileName} {v.Position}/{v.TotalParts}"))
               .To("output");
        }
    }
}
