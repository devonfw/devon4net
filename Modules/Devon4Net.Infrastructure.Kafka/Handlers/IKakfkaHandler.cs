using System.Threading.Tasks;
using Confluent.Kafka;

namespace Devon4Net.Infrastructure.Kafka.Handlers
{
    public interface IKakfkaHandler
    {
        IProducer<T, TV> GetProducerBuilder<T, TV>(string clientId) where T : class where TV : class;
        IConsumer<T, TV> GetConsumerBuilder<T, TV>(string consumerId) where T : class where TV : class;
        Task<DeliveryResult<T, TV>> DeliverMessage<T, TV>(T key, TV value, string producerId) where T : class where TV : class;
    }
}