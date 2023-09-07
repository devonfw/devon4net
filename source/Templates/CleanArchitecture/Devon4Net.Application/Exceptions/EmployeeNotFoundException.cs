using Devon4Net.Infrastructure.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Runtime.Serialization;

namespace Devon4Net.Application.Exceptions;

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

    public EmployeeNotFoundException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmployeeNotFoundException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public EmployeeNotFoundException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmployeeNotFoundException"/> class with a specified error message
    /// and a reference to the inner exception that caused this exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that caused this exception.</param>
    public EmployeeNotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmployeeNotFoundException"/> class with serialized data.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected EmployeeNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}