using System;
using System.Runtime.Serialization;
using Devon4Net.Infrastructure.Common.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Devon4Net.Infrastructure.SmaxHcm.Exceptions.CreateDesignExceptions
{
    [Serializable]
    public class SmaxHcmGetDesignVersionException : Exception, IWebApiException
    {
        public int StatusCode => StatusCodes.Status500InternalServerError;

        public bool ShowMessage => false;

        public SmaxHcmGetDesignVersionException()
        {
        }

        public SmaxHcmGetDesignVersionException(string message)
            : base(message)
        {
        }

        protected SmaxHcmGetDesignVersionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
