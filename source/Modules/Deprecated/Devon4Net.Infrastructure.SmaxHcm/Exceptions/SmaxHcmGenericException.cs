using System.Runtime.Serialization;
using Devon4Net.Infrastructure.Common.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Devon4Net.Infrastructure.SmaxHcm.Exceptions
{
    [Serializable]
    public class SmaxHcmGenericException : Exception, IWebApiException
    {
        public int StatusCode => StatusCodes.Status500InternalServerError;

        public bool ShowMessage => false;

        public SmaxHcmGenericException()
        {
        }

        public SmaxHcmGenericException(string message)
            : base(message)
        {
        }

        protected SmaxHcmGenericException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
