using System;
using System.Runtime.Serialization;
using Devon4Net.Infrastructure.Common.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Devon4Net.Infrastructure.SmaxHcm.Exceptions
{
    [Serializable]
    public class SmaxHcmUnauthorizedException : Exception, IWebApiException
    {
        public int StatusCode => StatusCodes.Status401Unauthorized;

        public bool ShowMessage => false;

        public SmaxHcmUnauthorizedException()
        {
        }

        public SmaxHcmUnauthorizedException(string message)
            : base(message)
        {
        }

        protected SmaxHcmUnauthorizedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
