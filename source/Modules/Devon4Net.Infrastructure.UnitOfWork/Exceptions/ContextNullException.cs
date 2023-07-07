using System.Runtime.Serialization;

namespace Devon4Net.Domain.UnitOfWork.Exceptions;

[Serializable]
public class ContextNullException : Exception
{
    public ContextNullException()
    {
    }

    public ContextNullException(string message) : base(message)
    {
    }

    public ContextNullException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected ContextNullException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : base(serializationInfo, streamingContext)
    {
    }
}