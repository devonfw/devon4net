using Devon4Net.Infrastructure.Common.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Devon4Net.Infrastructure.Kafka.Exceptions
{
    /// <summary>
    /// Custom exception ConsumerNotFoundException
    /// </summary>
    [Serializable]
    public class ConsumerNotFoundException : Exception, IWebApiException
    {
        public int StatusCode => StatusCodes.Status500InternalServerError;

        /// <summary>
        /// Show the message on the response?
        /// </summary>
        public bool ShowMessage => true;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsumerNotFoundException"/> class.
        /// </summary>
        public ConsumerNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsumerNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ConsumerNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsumerNotFoundException"/> class.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ConsumerNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
