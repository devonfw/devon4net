using System.Threading.Tasks;
using Confluent.Kafka;

namespace Devon4Net.Infrastructure.Kafka.Handler
{
    public interface IKakfkaHandler
    {
        IProducer<T, V> GetProducerBuilder<T, V>(string clientId) where T : class where V : class;
        Task<DeliveryResult<T, V>> DeliverMessage<T, V>(T key, V value, string clientId, string topicName) where T : class where V : class;
    }
}