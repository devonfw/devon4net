using System.Threading.Tasks;
using Excalibur.Shared.Services;

namespace Excalibur.Shared.Business
{
    /// <summary>
    /// Business entity interface for Excalibur
    /// This class should manage storage, communication via services etc.
    /// </summary>
    public interface IBusiness
    {
        /// <summary>
        /// Updates the storage entities using a <see cref="IServiceBase{T}"/>. 
        /// Afterwards this should publish a message to all subscribers
        /// </summary>
        /// <returns>An await able Task</returns>
        Task UpdateFromServiceAsync();
        /// <summary>
        /// Publish a message to notify subscribers. Entities should be loaded (if needed) from storage, not from services.
        /// </summary>
        /// <returns>An await able Task</returns>
        Task PublishFromStorageAsync();
    }
}
