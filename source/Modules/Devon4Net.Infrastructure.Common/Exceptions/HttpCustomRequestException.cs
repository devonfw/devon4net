using Microsoft.AspNetCore.Http;

namespace Devon4Net.Infrastructure.Common.Exceptions
{
    [Serializable]
    public class HttpCustomRequestException : Exception, IWebApiException
    {
        public int StatusCode { get; }

        public bool ShowMessage => false;


        public HttpCustomRequestException()
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }

        public HttpCustomRequestException(string message)
            : base(message)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }

        public HttpCustomRequestException(string message, int statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }

        public HttpCustomRequestException(string message, Exception inner)
            : base(message, inner)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }

        protected HttpCustomRequestException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}
