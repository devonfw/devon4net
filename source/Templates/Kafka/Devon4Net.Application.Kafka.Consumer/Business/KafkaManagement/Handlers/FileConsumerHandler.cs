using Confluent.Kafka;
using Devon4Net.Application.Kafka.Consumer.Business.FileManagement.Dto;
using Devon4Net.Application.Kafka.Consumer.Business.FileManagement.Services;
using Devon4Net.Infrastructure.Kafka.Handlers.Consumer;
using Devon4Net.Infrastructure.Kafka.Options;
using Devon4Net.Infrastructure.Logger.Logging;

namespace Devon4Net.Application.Kafka.Consumer.Business.KafkaManagement.Handlers
{
    public class FileConsumerHandler : KafkaConsumerHandler<string, DataPieceDto<byte[]>>
    {
        private readonly IFileService FileService;

        public FileConsumerHandler(IServiceCollection services, KafkaOptions kafkaOptions, string consumerId, IDeserializer<string> keyDeserializer = null, IDeserializer<DataPieceDto<byte[]>> valueDeserializer = null, bool commit = false, int commitPeriod = 5, bool enableConsumerFlag = true) : base(services, kafkaOptions, consumerId, keyDeserializer, valueDeserializer, commit, commitPeriod, enableConsumerFlag)
        {
            var serviceProvider = services.BuildServiceProvider();
            FileService = serviceProvider.GetService<IFileService>();
        }

        public override void HandleCommand(string key, DataPieceDto<byte[]> value)
        {
            Devon4NetLogger.Information($"Consumer receives -> File: {value.FileName} {value.Position}/{value.TotalParts}");
            FileService.CreateFile(value);
        }
    }
}
