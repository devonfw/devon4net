using System.Runtime.Serialization;

namespace Devon4Net.Infrastructure.Common.Exceptions
{
    /// <summary>Interface for webapi exceptions</summary>
    public interface IWebApiException : ISerializable
    {
        /// <summary>Gets the status code.</summary>
        /// <value>The status code.</value>
        int StatusCode { get; }
        /// <summary>Gets a value indicating whether [show message].</summary>
        /// <value>
        ///   <c>true</c> if [show message]; otherwise, <c>false</c>.</value>
        bool ShowMessage { get; }
    }
}