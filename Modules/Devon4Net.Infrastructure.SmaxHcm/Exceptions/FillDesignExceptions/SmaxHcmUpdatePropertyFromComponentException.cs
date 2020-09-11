using System;
using System.Runtime.Serialization;
using Devon4Net.Infrastructure.Common.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Devon4Net.Infrastructure.SmaxHcm.Exceptions.FillDesignExceptions
{
    [Serializable]
    public class SmaxHcmUpdatePropertyFromComponentException : Exception, IWebApiException
    {
        public int StatusCode => StatusCodes.Status500InternalServerError;

        public bool ShowMessage => false;

        public SmaxHcmUpdatePropertyFromComponentException()
        {
        }

        public SmaxHcmUpdatePropertyFromComponentException(string message)
            : base(message)
        {
        }

        protected SmaxHcmUpdatePropertyFromComponentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
