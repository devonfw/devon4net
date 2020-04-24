using System;
using Devon4Net.Infrastructure.RabbitMQ.Events;

namespace Devon4Net.Infrastructure.RabbitMQ.Commands
{
    public abstract class Command : Message
    {
        public DateTime Timestamp { get; protected set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }
    }
}
