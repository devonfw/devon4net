namespace Devon4Net.Infrastructure.Kafka.Handlers
{
    public interface IKafkaConsumerHandler
    {
        T GetInstance<T>();
        void EnableConsumer(bool startConsumer = true);
        void DisableConsumer();
    }
}