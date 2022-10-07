namespace Devon4Net.Infrastructure.Kafka.Handlers
{
    public interface IKafkaHandler
    {
        TS GetInstance<TS>();
    }
}