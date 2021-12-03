using Devon4Net.Infrastructure.Common.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Devon4Net.Application.WebAPI.Implementation.Business.MediatRManagement.Exceptions
{
    /// <summary>
    /// Custom exception TodoNotFoundException
    /// </summary>
    [Serializable]
    public class TodoNotFoundException : Exception, IWebApiException
    {
        /// <summary>
        /// The forced http status code to be fired on the exception manager
        /// </summary>
        public int StatusCode => StatusCodes.Status204NoContent;

        /// <summary>
        /// Show the message on the response?
        /// </summary>
        public bool ShowMessage => true;

        /// <summary>
        /// Initializes a new instance of the <see cref="TodoNotFoundException"/> class.
        /// </summary>
        public TodoNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TodoNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public TodoNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TodoNotFoundException"/> class.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public TodoNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TodoNotFoundException"/> class.
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected TodoNotFoundException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}
