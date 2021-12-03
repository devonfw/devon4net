using System.Runtime.Serialization;
using Devon4Net.Infrastructure.Common.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Devon4Net.Infrastructure.SmaxHcm.Exceptions.CreateOfferingExceptions
{
    [Serializable]
    public class SmaxHcmSwitchActivationOfferingException : Exception, IWebApiException
    {
        public int StatusCode => StatusCodes.Status500InternalServerError;

        public bool ShowMessage => false;

        public SmaxHcmSwitchActivationOfferingException()
        {
        }

        public SmaxHcmSwitchActivationOfferingException(string message)
            : base(message)
        {
        }

        protected SmaxHcmSwitchActivationOfferingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
