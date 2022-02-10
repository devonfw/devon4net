using Devon4Net.Infrastructure.Kafka.Handlers;
using Devon4Net.Infrastructure.Logger.Logging;

namespace Devon4Net.Application.Kafka.Business.KafkaManagement.Handlers
{
    public class MessageConsumerHandler : KafkaConsumerHandler<string, string>
    {
        public MessageConsumerHandler(IServiceCollection services, IKakfkaHandler kafkaHandler, string consumerId, bool commit = false, int commitPeriod = 5) : base(services, kafkaHandler, consumerId, commit, commitPeriod)
        {
        }

        public override void HandleCommand(string key, string value)
        {
            Devon4NetLogger.Information($"Received message key: {key} | value: {value}");
        }
    }
}
