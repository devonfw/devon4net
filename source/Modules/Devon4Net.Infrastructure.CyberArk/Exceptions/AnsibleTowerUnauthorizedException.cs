using System;
using Devon4Net.Infrastructure.Common.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Devon4Net.Infrastructure.CyberArk.Exceptions
{
    public class CyberArkUnauthorizedException : Exception, IWebApiException
    {
        public int StatusCode => StatusCodes.Status401Unauthorized;

        public bool ShowMessage => false;

        public CyberArkUnauthorizedException()
        {
        }

        public CyberArkUnauthorizedException(string message)
            : base(message)
        {
        }
    }
}
