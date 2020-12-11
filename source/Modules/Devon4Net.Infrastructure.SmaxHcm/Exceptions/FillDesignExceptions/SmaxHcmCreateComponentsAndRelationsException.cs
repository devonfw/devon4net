using System;
using System.Runtime.Serialization;
using Devon4Net.Infrastructure.Common.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Devon4Net.Infrastructure.SmaxHcm.Exceptions.FillDesignExceptions
{
    [Serializable]
    public class SmaxHcmCreateComponentsAndRelationsException : Exception, IWebApiException
    {
        public int StatusCode => StatusCodes.Status500InternalServerError;

        public bool ShowMessage => false;

        public SmaxHcmCreateComponentsAndRelationsException()
        {
        }

        public SmaxHcmCreateComponentsAndRelationsException(string message)
            : base(message)
        {
        }

        protected SmaxHcmCreateComponentsAndRelationsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
