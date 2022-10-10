using Confluent.Kafka;
using Devon4Net.Infrastructure.Kafka.Handlers.Consumer;
using Devon4Net.Infrastructure.Kafka.Options;
using Devon4Net.Infrastructure.Logger.Logging;

namespace Devon4Net.Application.Kafka.Consumer.Business.KafkaManagement.Handlers
{
    public class MessageConsumerHandler : KafkaConsumerHandler<string, string>
    {

        public MessageConsumerHandler(IServiceCollection services, KafkaOptions kafkaOptions, string consumerId, IDeserializer<string> keyDeserializer = null, IDeserializer<string> valueDeserializer = null, bool commit = false, int commitPeriod = 5, bool enableConsumerFlag = true) : base(services, kafkaOptions, consumerId, keyDeserializer, valueDeserializer, commit, commitPeriod, enableConsumerFlag)
        {
        }

        public override void HandleCommand(string key, string value)
        {
            Devon4NetLogger.Information($"Consumer receives -> Key: {key} | Value: {value}");
        }
    }
}
