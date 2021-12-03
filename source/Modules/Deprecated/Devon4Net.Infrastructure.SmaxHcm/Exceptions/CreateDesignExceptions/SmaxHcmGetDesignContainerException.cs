using System.Runtime.Serialization;
using Devon4Net.Infrastructure.Common.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Devon4Net.Infrastructure.SmaxHcm.Exceptions.CreateDesignExceptions
{
    [Serializable]
    public class SmaxHcmGetDesignContainerException : Exception, IWebApiException
    {
        public int StatusCode => StatusCodes.Status500InternalServerError;

        public bool ShowMessage => false;

        public SmaxHcmGetDesignContainerException()
        {
        }

        public SmaxHcmGetDesignContainerException(string message)
            : base(message)
        {
        }

        protected SmaxHcmGetDesignContainerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
