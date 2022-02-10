using System.Runtime.Serialization;
using Devon4Net.Infrastructure.Common.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Devon4Net.Infrastructure.SmaxHcm.Exceptions.PublishDesignException
{
    [Serializable]
    public class SmaxHcmPublishDesignVersionException : Exception, IWebApiException
    {
        public int StatusCode => StatusCodes.Status500InternalServerError;

        public bool ShowMessage => false;

        public SmaxHcmPublishDesignVersionException()
        {
        }

        public SmaxHcmPublishDesignVersionException(string message)
            : base(message)
        {
        }

        protected SmaxHcmPublishDesignVersionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
