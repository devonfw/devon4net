using Confluent.Kafka;

namespace Devon4Net.Infrastructure.Kafka.Handlers.Producer
{
    public interface IKafkaProducerHandler<TKey, TValue> where TKey : class where TValue : class
    {
        IProducer<TKey, TValue> GetProducerBuilder(string producerId);
        Task<DeliveryResult<TKey, TValue>> DeliverMessage(TKey key, TValue value, string producerId);
        Task<DeliveryResult<TKey, TValue>> SendMessage(TKey key, TValue value);
    }
}