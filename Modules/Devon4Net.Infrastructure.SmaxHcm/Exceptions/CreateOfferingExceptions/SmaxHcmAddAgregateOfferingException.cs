using System;
using System.Runtime.Serialization;
using Devon4Net.Infrastructure.Common.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Devon4Net.Infrastructure.SmaxHcm.Exceptions.CreateOfferingExceptions
{
    [Serializable]
    public class SmaxHcmAddAgregateOfferingException : Exception, IWebApiException
    {
        public int StatusCode => StatusCodes.Status500InternalServerError;

        public bool ShowMessage => false;

        public SmaxHcmAddAgregateOfferingException()
        {
        }

        public SmaxHcmAddAgregateOfferingException(string message)
            : base(message)
        {
        }

        protected SmaxHcmAddAgregateOfferingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
