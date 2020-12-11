using System;
using Devon4Net.Infrastructure.Common.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Devon4Net.Infrastructure.AnsibleTower.Exceptions
{
    public class AnsibleTowerUnauthorizedException: Exception, IWebApiException
    {
        public int StatusCode => StatusCodes.Status401Unauthorized;

        public bool ShowMessage => false;

        public AnsibleTowerUnauthorizedException()
        {
        }

        public AnsibleTowerUnauthorizedException(string message)
            : base(message)
        {
        }
    }
}
