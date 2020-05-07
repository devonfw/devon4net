using System;
using System.Text.Json;
using Devon4Net.Infrastructure.RabbitMQ.Events;

namespace Devon4Net.Infrastructure.RabbitMQ.Commands
{
    public class Command : Message
    {
        public DateTime Timestamp { get; protected set; }
        protected Command()
        {
            Timestamp = DateTime.Now;
            InternalMessageIdentifier = Guid.NewGuid();
        }

        public string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }

        public T Deserialize<T>(string messageContent)
        {
            return JsonSerializer.Deserialize<T>(messageContent);
        }
    }
}
