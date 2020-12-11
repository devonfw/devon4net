using System;

namespace Devon4Net.Infrastructure.MediatR.Model
{
    public class ModelBase
    {
        public DateTime Timestamp { get; protected set; }
        public string MessageType { get; private set; }
        public Guid InternalMessageIdentifier { get; set; }

        public ModelBase()
        {
            Timestamp = DateTime.Now;
            InternalMessageIdentifier = Guid.NewGuid();
            MessageType = GetType().Name;
        }
    }
}
