using Devon4Net.Infrastructure.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Devon4Net.WebAPI.Implementation.Business.EmployeeManagement.Exceptions
{
    /// <summary>
    /// Custom exception EmployeeNotFoundException
    /// </summary>
    [Serializable]
    public class EmployeeNotFoundException : Exception, IWebApiException
    {
        /// <summary>
        /// The forced http status code to be fired on the exception manager
        /// </summary>
        public int StatusCode => StatusCodes.Status404NotFound;

        /// <summary>
        /// Show the message on the response?
        /// </summary>
        public bool ShowMessage => true;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeNotFoundException"/> class.
        /// </summary>
        public EmployeeNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public EmployeeNotFoundException(string message)
            : base(message)
        {
        }
    }
}
