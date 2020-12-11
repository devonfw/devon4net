using System.Collections.Generic;
using System.Threading.Tasks;
using Excalibur.Shared.Services;
using Excalibur.Shared.Storage;

namespace Excalibur.Shared.Business
{
    /// <inheritdoc />
    public class BaseListBusiness<TId, TDomain> : BaseListBusiness<TId, TDomain, IServiceBase<IList<TDomain>>>
        where TDomain : StorageDomain<TId>, new()
    {
    }

    /// <summary>
    /// Base implementation of a business entity that manages multiple objects or domain objects.
    /// This class will manage storage, service communication etc. 
    /// 
    /// Also see <see cref="BusinessBase{TId,TDomain,TService}"/>
    /// </summary>
    /// <typeparam name="TId">  The type of Identifier to use for the database object. Ints, guids,
    ///                         etc. </typeparam>
    /// <typeparam name="TDomain">The type of the object that wants to be stored</typeparam>
    /// <typeparam name="TService">The type of the service that should be used for communications</typeparam>
    public class BaseListBusiness<TId, TDomain, TService> : BusinessBase<TId, TDomain, TService>, IListBusiness<TId, TDomain>
        where TDomain : StorageDomain<TId>, new()
        where TService : class, IServiceBase<IList<TDomain>>
    {
        /// <summary>
        /// Gets all domain objects managed by this entity
        /// </summary>
        /// <returns>An await able Task with all domain objects as result</returns>
        public virtual async Task<IList<TDomain>> GetAllAsync()
        {
            return await Storage.GetRangeAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Get a single domain object by Id
        /// </summary>
        /// <param name="id">The id for the object to retrieve</param>
        /// <returns>An await able Task with the domain object as result</returns>
        public virtual async Task<TDomain> GetByIdAsync(TId id)
        {
            return await Storage.GetAsync(id).ConfigureAwait(false);
        }

        /// <summary>
        /// Updates the domain object from service using <see cref="TService"/>
        /// </summary>
        /// <returns>An await-able task</returns>
        public override async Task UpdateFromServiceAsync()
        {
            var result = await Service.SyncDataAsync().ConfigureAwait(false) ?? new List<TDomain>();

            await StoreItemsAsync(result).ConfigureAwait(false);

            PublishListUpdated();
        }

        /// <summary>
        /// Publish a message to subscribers that do not contain the actual objects. They have to be retrieved separately. 
        /// Note: Might add an initial range when loading the first time
        /// </summary>
        /// <returns>An await-able task</returns>
        public override async Task PublishFromStorageAsync()
        {
            // todo Add initial range when loading from storage
            PublishListUpdated();
        }

        /// <summary>
        /// Stores <see cref="objectsToStore"/> using <see cref="Storage"/>. 
        /// This stores all entities.
        /// </summary>
        /// <param name="objectsToStore">The objects to store</param>
        /// <returns>An await-able task</returns>
        protected async Task StoreItemsAsync(IList<TDomain> objectsToStore)
        {
            await Storage.StoreRangeAsync(objectsToStore).ConfigureAwait(false);
        }

        /// <summary>
        /// Deletes a domain object by a certain id
        /// </summary>
        /// <param name="id">The id of the object that should be deleted</param>
        /// <returns>An await-able task</returns>
        public async Task DeleteItemAsync(TId id)
        {
            await Storage.DeleteAsync(id).ConfigureAwait(false);

            PublishListUpdated();
        }
    }
}