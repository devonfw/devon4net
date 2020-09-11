using System;
using System.Runtime.Serialization;
using Devon4Net.Infrastructure.Common.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Devon4Net.Infrastructure.SmaxHcm.Exceptions.CreateOfferingExceptions
{
    [Serializable]
    public class SmaxHcmGetOfferingProvidersException : Exception, IWebApiException
    {
        public int StatusCode => StatusCodes.Status500InternalServerError;

        public bool ShowMessage => false;

        public SmaxHcmGetOfferingProvidersException()
        {
        }

        public SmaxHcmGetOfferingProvidersException(string message)
            : base(message)
        {
        }

        protected SmaxHcmGetOfferingProvidersException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
