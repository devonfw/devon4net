using Devon4Net.Infrastructure.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Runtime.Serialization;

namespace Devon4Net.Infrastructure.Kafka.Exceptions
{
    /// <summary>
    /// Custom exception AdminNotFoundException
    /// </summary>
    [Serializable]
    public class AdminNotFoundException : WebApiException
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

        protected AdminNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base()
        {
        }
    }
}
