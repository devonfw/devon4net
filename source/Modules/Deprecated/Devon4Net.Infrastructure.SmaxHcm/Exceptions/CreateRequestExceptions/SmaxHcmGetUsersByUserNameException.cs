using System.Runtime.Serialization;
using Devon4Net.Infrastructure.Common.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Devon4Net.Infrastructure.SmaxHcm.Exceptions.CreateRequestExceptions
{
    [Serializable]
    public class SmaxHcmGetUsersByUserNameException : Exception, IWebApiException
    {
        public int StatusCode => StatusCodes.Status500InternalServerError;

        public bool ShowMessage => false;

        public SmaxHcmGetUsersByUserNameException()
        {
        }

        public SmaxHcmGetUsersByUserNameException(string message)
            : base(message)
        {
        }

        protected SmaxHcmGetUsersByUserNameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
