using System;
using System.Runtime.Serialization;
using Devon4Net.Infrastructure.Common.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Devon4Net.Infrastructure.SmaxHcm.Exceptions.FillDesignExceptions
{
    [Serializable]
    public class SmaxHcmApplyComponentTemplateToComponentException : Exception, IWebApiException
    {
        public int StatusCode => StatusCodes.Status500InternalServerError;

        public bool ShowMessage => false;

        public SmaxHcmApplyComponentTemplateToComponentException()
        {
        }

        public SmaxHcmApplyComponentTemplateToComponentException(string message)
            : base(message)
        {
        }

        protected SmaxHcmApplyComponentTemplateToComponentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
