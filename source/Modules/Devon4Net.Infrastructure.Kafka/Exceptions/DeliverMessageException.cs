using Devon4Net.Infrastructure.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Runtime.Serialization;

namespace Devon4Net.Infrastructure.Kafka.Exceptions
{
    /// <summary>
    /// Custom exception DeliverMessageException
    /// </summary>
    [Serializable]
    public class DeliverMessageException : WebApiException
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
        /// Initializes a new instance of the <see cref="DeliverMessageException"/> class.
        /// </summary>
        public DeliverMessageException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeliverMessageException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public DeliverMessageException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeliverMessageException"/> class.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public DeliverMessageException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DeliverMessageException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base()
        {
        }
    }
}
