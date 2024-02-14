using Devon4Net.Infrastructure.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Runtime.Serialization;

namespace Devon4Net.Application.WebAPI.Business.MediatRManagement.Exceptions
{
    /// <summary>
    /// Custom exception TodoNotFoundException
    /// </summary>
    [Serializable]
    public class TodoNotFoundException : WebApiException
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
        protected TodoNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base()
        {
        }
    }
}
