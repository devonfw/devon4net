using Devon4Net.Infrastructure.Common.Exceptions;
using System.Runtime.Serialization;

namespace Devon4Net.Application.WebAPI.Business.EmployeeManagement.Exceptions
{
    /// <summary>
    /// Custom exception EmployeeNotFoundException
    /// </summary>
    [Serializable]
    public class EmployeeNotFoundException : WebApiException
    {
        /// <summary>
        /// Gets the forced http status code to be fired on the exception manager.
        /// </summary>
        public override int StatusCode => StatusCodes.Status404NotFound;

        /// <summary>
        /// Gets a value indicating whether show the message on the response?.
        /// </summary>
        public override bool ShowMessage => true;

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

        protected EmployeeNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base()
        {
        }
    }
}
