using System;

namespace Devon4Net.Infrastructure.RabbitMQ.Events
{
    public abstract class Message 
    {
        public string MessageType { get; protected set; }
        public Guid InternalMessageIdentifier { get; protected set; }

        protected Message()
        {
            MessageType = GetType().Name;
            InternalMessageIdentifier = Guid.NewGuid();
        }
    }
}
