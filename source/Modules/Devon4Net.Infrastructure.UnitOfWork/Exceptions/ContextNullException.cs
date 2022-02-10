using System;

namespace Devon4Net.Domain.UnitOfWork.Exceptions
{
    [Serializable]
    public class ContextNullException : Exception
    {
        public ContextNullException() : base()
        {
        }

        public ContextNullException(string message) : base(message)
        {
        }

        public ContextNullException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ContextNullException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}
