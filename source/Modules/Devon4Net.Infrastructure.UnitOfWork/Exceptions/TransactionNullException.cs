using System.Runtime.Serialization;

namespace Devon4Net.Infrastructure.UnitOfWork.Exceptions
{
    [Serializable]
    public class TransactionNullException : Exception
    {
        public TransactionNullException() : base()
        {
        }

        public TransactionNullException(string message) : base(message)
        {
        }

        public TransactionNullException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TransactionNullException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}