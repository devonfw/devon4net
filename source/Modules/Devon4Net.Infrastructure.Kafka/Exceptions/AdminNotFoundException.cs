using Devon4Net.Infrastructure.Common.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Devon4Net.Infrastructure.Kafka.Exceptions
{
    /// <summary>
    /// Custom exception AdminNotFoundException
    /// </summary>
    [Serializable]
    public class AdminNotFoundException : Exception, IWebApiException
    {
        public int StatusCode => StatusCodes.Status500InternalServerError;

        /// <summary>
        /// Show the message on the response?
        /// </summary>
        public bool ShowMessage => true;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminNotFoundException"/> class.
        /// </summary>
        public AdminNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public AdminNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsumerNotFoundException"/> class.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public AdminNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminNotFoundException"/> class.
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected AdminNotFoundException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}
