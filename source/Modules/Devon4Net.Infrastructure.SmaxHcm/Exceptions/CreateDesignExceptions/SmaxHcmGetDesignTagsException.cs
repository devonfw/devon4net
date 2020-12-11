using System;
using System.Runtime.Serialization;
using Devon4Net.Infrastructure.Common.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Devon4Net.Infrastructure.SmaxHcm.Exceptions.CreateDesignExceptions
{
    [Serializable]
    public class SmaxHcmGetDesignTagsException : Exception, IWebApiException
    {
        public int StatusCode => StatusCodes.Status500InternalServerError;

        public bool ShowMessage => false;

        public SmaxHcmGetDesignTagsException()
        {
        }

        public SmaxHcmGetDesignTagsException(string message)
            : base(message)
        {
        }

        protected SmaxHcmGetDesignTagsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
