using System.Runtime.Serialization;
using Devon4Net.Infrastructure.Common.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Devon4Net.Infrastructure.SmaxHcm.Exceptions.CreateRequestExceptions
{
    [Serializable]
    public class SmaxHcmCreateRequestException : Exception, IWebApiException
    {
        public int StatusCode => StatusCodes.Status500InternalServerError;

        public bool ShowMessage => false;

        public SmaxHcmCreateRequestException()
        {
        }

        public SmaxHcmCreateRequestException(string message)
            : base(message)
        {
        }

        protected SmaxHcmCreateRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
