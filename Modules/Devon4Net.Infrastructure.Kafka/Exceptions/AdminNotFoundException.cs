using System;
using Devon4Net.Infrastructure.Common.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Devon4Net.Infrastructure.Kafka.Exceptions
{
    public class AdminNotFoundException : Exception, IWebApiException
    {
        public int StatusCode { get; }
        public bool ShowMessage { get; }

        public AdminNotFoundException()
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }

        public AdminNotFoundException(string message) : base(message)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}
