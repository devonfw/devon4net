namespace Devon4Net.Infrastructure.RabbitMQ.Events
{
    public abstract class Message 
    {
        public string MessageType { get; private set; }
        public Guid InternalMessageIdentifier { get; set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}
