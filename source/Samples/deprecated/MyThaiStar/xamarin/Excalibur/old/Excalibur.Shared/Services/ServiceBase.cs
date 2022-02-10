using System.Threading.Tasks;

namespace Excalibur.Shared.Services
{
    /// <summary>
    /// Abstract base class for services. 
    /// This might be extended with custom checks and method that might be useful for sharing.
    /// </summary>
    /// <typeparam name="T">The type of the object that is used for communication</typeparam>
    public abstract class ServiceBase<T> : IServiceBase<T>
    {
        /// <summary>
        /// Base method for syncing data.
        /// </summary>
        /// <returns>An await able Task with the resulting objects as result</returns>
        public abstract Task<T> SyncDataAsync();
    }
}