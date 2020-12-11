using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excalibur.Shared.Storage
{
    /// <summary>
    /// A object storage provider that provides a general data storage interface. 
    /// This class will use TId as a type identifier for objects
    /// </summary>
    /// <typeparam name="TId">  The type of Identifier to use for the database object. Ints, guids,
    ///                         etc. </typeparam>
    /// <typeparam name="TDomain">The type of the object that wants to be stored</typeparam>
    public interface IObjectStorageProvider<in TId, TDomain> 
        where TDomain : StorageDomain<TId>
    {
        /// <summary>
        /// Store a range of T
        /// </summary>
        /// <param name="objectsToStore">The objects to store</param>
        /// <returns>An await able Task</returns>
        Task StoreRangeAsync(IList<TDomain> objectsToStore);
        /// <summary>
        /// Gets a range of TDomain from storage
        /// </summary>
        /// <returns>An await able Task with the range as result</returns>
        Task<IList<TDomain>> GetRangeAsync(); // todo: change to take skip, returning all for now

        /// <summary>
        /// GetAsync a certain TDomain
        /// </summary>
        /// <param name="id">The id of the object that will be retrieved</param>
        /// <returns>An await able Task with the requested object as result</returns>
        Task<TDomain> GetAsync(TId id);
        /// <summary>
        /// Adds or updates an object to storage
        /// </summary>
        /// <param name="objectToStore">The object to store</param>
        /// <returns>An await able Task with the success as result</returns>
        Task<bool> AddOrUpdateAsync(TDomain objectToStore);
        /// <summary>
        /// Deletes an object with a certain Id
        /// </summary>
        /// <param name="id">The id of the object that will be deleted</param>
        /// <returns>An await able Task with the success as result</returns>
        Task<bool> DeleteAsync(TId id);

        // GetRangeAsync
        // SetRange
        // Create
        // Update
        // Read
        // DeleteAsync
    }
}
