using System.Net.Http;
using System.Threading.Tasks;

namespace Excalibur.Shared.Services
{
    /// <summary>
    /// Base class for services.
    ///
    /// This class provides a static HttpClient as <see cref="SharedClient"/>.
    /// </summary>
    public abstract class ServiceBase
    {
        /// <summary>
        /// Static HttpClient that should be used when making webrequests.
        ///
        /// Might introduce a HttpClient per Type of service, but for now, just the one.
        /// </summary>
        protected static HttpClient SharedClient { get; set; } = new HttpClient(new AutomaticDecompressionHandler());
    }

    /// <summary>
    /// Abstract base class for services. 
    /// This might be extended with custom checks and method that might be useful for sharing.
    /// </summary>
    /// <typeparam name="T">The type of the object that is used for communication</typeparam>
    public abstract class ServiceBase<T> : ServiceBase, IServiceBase<T>
    {
        /// <summary>
        /// Base method for syncing data.
        /// </summary>
        /// <returns>An await able Task with the resulting objects as result</returns>
        public abstract Task<T> SyncDataAsync();
    }
}