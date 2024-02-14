using Devon4Net.Infrastructure.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Runtime.Serialization;

namespace Devon4Net.Application.Exceptions;

/// <summary>
/// Custom exception EmployeeNotFoundException
/// </summary>
[Serializable]
public class EmployeeNotFoundException : WebApiException
{
    /// <summary>
    /// Gets the forced http status code to be fired on the exception manager.
    /// </summary>
    public override int StatusCode => StatusCodes.Status404NotFound;

    /// <summary>
    /// Gets a value indicating whether show the message on the response?.
    /// </summary>
    public override bool ShowMessage => true;

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

    protected EmployeeNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext)
    : base()
    {
    }
}