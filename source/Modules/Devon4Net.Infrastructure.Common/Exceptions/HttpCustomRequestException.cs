using Microsoft.AspNetCore.Http;
using System.Runtime.Serialization;

namespace Devon4Net.Infrastructure.Common.Exceptions
{
    [Serializable]
    public class HttpCustomRequestException : WebApiException
    {
        /// <summary>
        /// Gets the forced http status code to be fired on the exception manager.
        /// </summary>
        public override int StatusCode => StatusCodes.Status404NotFound;

        /// <summary>
        /// Gets a value indicating whether show the message on the response?.
        /// </summary>
        public override bool ShowMessage => true;


        public HttpCustomRequestException()
        {
        }

        public HttpCustomRequestException(string message)
            : base(message)
        {
        }

        public HttpCustomRequestException(string message, int statusCode)
            : base(message)
        {
        }

        public HttpCustomRequestException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected HttpCustomRequestException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base()
        {
        }
    }
}
