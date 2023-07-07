using MediatR;

namespace Devon4Net.Infrastructure.MediatR.Common
{
    public record ActionBase<T> : IRequest<T> where T : class
    {
        public DateTime Timestamp { get; }
        public string MessageType { get; }
        public Guid InternalMessageIdentifier { get; }

        protected ActionBase()
        {
            Timestamp = DateTime.Now;
            InternalMessageIdentifier = Guid.NewGuid();
            MessageType = GetType().Name;
        }
    }
}