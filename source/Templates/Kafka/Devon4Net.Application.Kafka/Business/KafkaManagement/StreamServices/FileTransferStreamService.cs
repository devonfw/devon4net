using Devon4Net.Application.Kafka.Business.Messages;
using Devon4Net.Infrastructure.Kafka.Options;
using Devon4Net.Infrastructure.Kafka.Streams.Services;
using Streamiz.Kafka.Net.SerDes;

namespace Devon4Net.Application.Kafka.Business.KafkaManagement.Services
{
    public class FileTransferStreamService : KafkaStreamService<string, DataPieceDto<byte[]>>
    {
        public FileTransferStreamService(IServiceCollection services, KafkaOptions kafkaOptions, string applicationId, ISerDes<string> keySerDes = null, ISerDes<DataPieceDto<byte[]>> valueSerDes = null) : base(services, kafkaOptions, applicationId, keySerDes, valueSerDes)
        {
        }

        public override void CreateStreamBuilder()
        {
            StreamBuilder.Stream<string, DataPieceDto<byte[]>>("file_producer")
               .Peek((k, v) => Console.WriteLine($"FileStream says -> File: {v.FileName} {v.Position}/{v.TotalParts}"))
               .To("file_consumer");
        }
    }
}
