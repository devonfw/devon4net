namespace Devon4Net.Infrastructure.RabbitMQ.Common
{
    public enum QueueActions
    {
        SetUp = 0,
        Sent,
        Handled,
        Error
    }
}
