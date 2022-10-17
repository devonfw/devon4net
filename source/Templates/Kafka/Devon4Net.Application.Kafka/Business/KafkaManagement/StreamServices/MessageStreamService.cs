using Devon4Net.Infrastructure.Kafka.Options;
using Devon4Net.Infrastructure.Kafka.Streams.Services;
using Streamiz.Kafka.Net.SerDes;

namespace Devon4Net.Application.Kafka.Business.KafkaManagement.Services
{
    public class MessageStreamService : KafkaStreamService<string, string>
    {
        public MessageStreamService(IServiceCollection services, KafkaOptions kafkaOptions, string applicationId, ISerDes<string> keySerDes = null, ISerDes<string> valueSerDes = null) : base(services, kafkaOptions, applicationId, keySerDes, valueSerDes)
        {
        }

        public override void CreateStreamBuilder()
        {
            StreamBuilder.Stream<string, string>("message_producer")
               .Peek((k, v) => Console.WriteLine($"MessageStream says -> Key: {k} | Value: {v}"))
               .To("message_consumer");
        }
    }
}
