using System.Runtime.Serialization;

namespace Devon4Net.Domain.UnitOfWork.Exceptions;

[Serializable]
public class RepositoryNotFoundException : Exception
{
    public RepositoryNotFoundException()
    {
    }

    public RepositoryNotFoundException(string message) : base(message)
    {
    }

    public RepositoryNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected RepositoryNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : base(serializationInfo, streamingContext)
    {
    }
}