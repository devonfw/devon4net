using Devon4Net.Infrastructure.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Runtime.Serialization;

namespace Devon4Net.Application.Exceptions
{
    /// <summary>
    /// Custom exception EmployeeNotFoundException
    /// </summary>
    [Serializable]
    public class EmployeeNotFoundException : Exception, IWebApiException
    {
        /// <summary>
        /// The forced http status code to be fired on the exception manager
        /// </summary>
        public int StatusCode => StatusCodes.Status404NotFound;

        /// <summary>
        /// Show the message on the response?
        /// </summary>
        public bool ShowMessage => true;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeNotFoundException"/> class.
        /// </summary>
        public EmployeeNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public EmployeeNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeNotFoundException"/> class.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public EmployeeNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeNotFoundException"/> class.
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected EmployeeNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo.GetString("Message"))
        {
        }

        /// <summary>
        /// Implements the ISerializable interface for custom serialization.
        /// </summary>
        /// <param name="info">The SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The StreamingContext that contains contextual information about the source or destination.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            // Serialize the message property
            info.AddValue("Message", this.Message);

            // Call the base class implementation to save any other data that needs to be serialized.
            base.GetObjectData(info, context);
        }
    }
}