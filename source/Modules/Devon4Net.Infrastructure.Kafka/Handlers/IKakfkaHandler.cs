using Confluent.Kafka;

namespace Devon4Net.Infrastructure.Kafka.Handlers
{
    public interface IKakfkaHandler
    {
        IProducer<T, TV> GetProducerBuilder<T, TV>(string producerId) where T : class where TV : class;
        IConsumer<T, TV> GetConsumerBuilder<T, TV>(string consumerId) where T : class where TV : class;
        Task<DeliveryResult<T, TV>> DeliverMessage<T, TV>(T key, TV value, string producerId) where T : class where TV : class;
        Task<bool> CreateTopic(string adminId, string topicName, short replicationFactor = 1, int numPartitions = 1);
        Task<bool> DeleteTopic(string adminId, List<string> topicsName);
    }
}