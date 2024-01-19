namespace Devon4Net.Infrastructure.UnitOfWork.Exceptions
{
    [Serializable]
    public class RepositoryNotFoundException : Exception
    {
        public RepositoryNotFoundException() : base()
        {
        }

        public RepositoryNotFoundException(string message) : base(message)
        {
        }

        public RepositoryNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RepositoryNotFoundException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}
