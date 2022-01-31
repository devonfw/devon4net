namespace Devon4Net.Infrastructure.RabbitMQ.Events
{
    public abstract class Message
    {
        public string MessageType { get; }
        public Guid InternalMessageIdentifier { get; set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}
