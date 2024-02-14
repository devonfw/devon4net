using Devon4Net.Infrastructure.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Runtime.Serialization;

namespace Devon4Net.Infrastructure.Kafka.Exceptions
{
    /// <summary>
    /// Custom exception ConsumerException
    /// </summary>
    [Serializable]
    public class ConsumerException : WebApiException
    {
        /// <summary>
        /// Gets the forced http status code to be fired on the exception manager.
        /// </summary>
        public override int StatusCode => StatusCodes.Status500InternalServerError;

        /// <summary>
        /// Gets a value indicating whether show the message on the response?.
        /// </summary>
        public override bool ShowMessage => true;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsumerException"/> class.
        /// </summary>
        public ConsumerException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsumerException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ConsumerException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsumerException"/> class.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ConsumerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ConsumerException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base()
        {
        }
    }
}
