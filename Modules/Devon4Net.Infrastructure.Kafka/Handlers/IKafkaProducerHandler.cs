using System.Threading.Tasks;
using Confluent.Kafka;

namespace Devon4Net.Infrastructure.Kafka.Handlers
{
    public interface IKafkaProducerHandler<T, TV>
    {
        T GetInstance<T>();
        Task<DeliveryResult<T, TV>> SendMessage(T key, TV value);
    }
}