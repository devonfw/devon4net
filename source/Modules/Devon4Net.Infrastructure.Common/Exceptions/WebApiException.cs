namespace Devon4Net.Infrastructure.Common.Exceptions
{
    /// <summary>Interface for webapi exceptions.</summary>
    public abstract class WebApiException : Exception
    {
        protected WebApiException()
        {
        }

        protected WebApiException(string message)
            : base(message)
        {
        }

        protected WebApiException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>Gets the status code.</summary>
        /// <value>The status code.</value>
        public virtual int StatusCode { get; }

        /// <summary>Gets a value indicating whether [show message].</summary>
        /// <value>
        ///   <c>true</c> if [show message]; otherwise, <c>false</c>.</value>
        public virtual bool ShowMessage { get; }
    }
}
