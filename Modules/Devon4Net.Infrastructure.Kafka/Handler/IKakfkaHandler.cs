using System.Threading.Tasks;
using Confluent.Kafka;

namespace Devon4Net.Infrastructure.Kafka.Handler
{
    public interface IKakfkaHandler
    {
        IProducer<T, TV> GetProducerBuilder<T, TV>(string clientId) where T : class where TV : class;
        IConsumer<T, TV> GetConsumerBuilder<T, TV>(string consumerId) where T : class where TV : class;
        Task<DeliveryResult<T, V>> DeliverMessage<T, V>(T key, V value, string clientId, string topicName) where T : class where V : class;
    }
}