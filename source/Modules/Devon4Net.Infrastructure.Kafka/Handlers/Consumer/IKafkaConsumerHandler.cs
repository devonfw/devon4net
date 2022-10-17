using Confluent.Kafka;

namespace Devon4Net.Infrastructure.Kafka.Handlers.Consumer
{
    public interface IKafkaConsumerHandler<TKey, TValue> where TKey : class where TValue : class
    {
        IConsumer<TKey, TValue> GetConsumerBuilder(string consumerId);
        void EnableConsumer(bool startConsumer = true);
        void DisableConsumer();
    }
}