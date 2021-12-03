using System.Runtime.Serialization;
using Devon4Net.Infrastructure.Common.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Devon4Net.Infrastructure.SmaxHcm.Exceptions.FillDesignExceptions
{
    [Serializable]
    public class SmaxHcmGetServiceDesignMetamodelException : Exception, IWebApiException
    {
        public int StatusCode => StatusCodes.Status500InternalServerError;

        public bool ShowMessage => false;

        public SmaxHcmGetServiceDesignMetamodelException()
        {
        }

        public SmaxHcmGetServiceDesignMetamodelException(string message)
            : base(message)
        {
        }

        protected SmaxHcmGetServiceDesignMetamodelException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
