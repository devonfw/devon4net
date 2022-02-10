using Confluent.Kafka;

namespace Devon4Net.Infrastructure.Kafka.Handlers
{
    public interface IKafkaProducerHandler<T, TV>
    {
        TS GetInstance<TS>();
        Task<DeliveryResult<T, TV>> SendMessage(T key, TV value);
    }
}