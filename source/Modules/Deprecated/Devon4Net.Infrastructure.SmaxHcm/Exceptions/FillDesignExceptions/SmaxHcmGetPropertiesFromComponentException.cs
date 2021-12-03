using System.Runtime.Serialization;
using Devon4Net.Infrastructure.Common.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Devon4Net.Infrastructure.SmaxHcm.Exceptions.FillDesignExceptions
{
    [Serializable]
    public class SmaxHcmGetPropertiesFromComponentException : Exception, IWebApiException
    {
        public int StatusCode => StatusCodes.Status500InternalServerError;

        public bool ShowMessage => false;

        public SmaxHcmGetPropertiesFromComponentException()
        {
        }

        public SmaxHcmGetPropertiesFromComponentException(string message)
            : base(message)
        {
        }

        protected SmaxHcmGetPropertiesFromComponentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
