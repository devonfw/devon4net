using System.Collections.Generic;
using System.Threading.Tasks;
using Excalibur.Shared.Storage;
using Excalibur.Shared.Utils;
using PubSub;
using XLabs.Ioc;

namespace Excalibur.Shared.Business
{
    /// <summary>
    /// Abstract base for business entities. 
    /// This entity provides various Publish methods (using <see cref="PubSub"/>) and methods that should be implemented.
    /// 
    /// The constructor will resolve a corresponding TService and <see cref="IObjectStorageProvider{TId,T}"/> for the 
    /// given TDomain />.
    /// </summary>
    /// <typeparam name="TId">  The type of Identifier to use for the database object. Ints, guids,
    ///                         etc. </typeparam>
    /// <typeparam name="TDomain">The type of the object that wants to be stored</typeparam>
    /// <typeparam name="TService">The type of the service that should be used for communications</typeparam>
    public abstract class BusinessBase<TId, TDomain, TService> : IBusiness
        where TDomain : StorageDomain<TId>, new()
        where TService : class
    {
        /// <summary>
        /// Instance of a givin TService that will and can be used for web requests.
        /// For example, this instance will be used in by <see cref="UpdateFromServiceAsync"/>
        /// </summary>
        protected TService Service { get; set; }

        /// <summary>
        /// Instance of a givin <see cref="IObjectStorageProvider{TId,TDomain}"/> that will be used for storing entities
        /// </summary>
        protected IObjectStorageProvider<TId, TDomain> Storage { get; set; }

        /// <summary>
        /// Initializes the instance.
        /// Resolves a Service and Storage that will be used.
        /// </summary>
        protected BusinessBase()
        {
            Service = Resolver.Resolve<TService>();
            Storage = Resolver.Resolve<IObjectStorageProvider<TId, TDomain>>();
        }

        /// <summary>
        /// Publishes a message using <see cref="MessageBase{T}"/> to notify subscribers underlying objects have changed.
        /// This is a general message. It does not contain the objects.
        /// </summary>
        protected void PublishListUpdated()
        {
            this.Publish<MessageBase<IList<TDomain>>>();
        }

        /// <summary>
        /// Publishes a message using <see cref="MessageBase{T}"/> to notify subscribers that one object has changed. 
        /// This also includes which object was updated and why it was updated.
        /// </summary>
        /// <param name="updatedObject">The object that was updated.</param>
        /// <param name="state">The state of the object.</param>
        protected void PublishUpdated(TDomain updatedObject, EDomainState state = EDomainState.Updated)
        {
            this.Publish(new MessageBase<TDomain>(updatedObject));
        }

        /// <summary>
        /// Stores and persists an object using <see cref="Storage"/>.
        /// </summary>
        /// <param name="objectToStore">The object that should be stored</param>
        /// <returns>An await-able task</returns>
        protected async Task StoreItemAsync(TDomain objectToStore)
        {
            await Storage.AddOrUpdateAsync(objectToStore).ConfigureAwait(false);

            PublishListUpdated();
        }

        /// <inheritdoc />
        public abstract Task UpdateFromServiceAsync();

        /// <inheritdoc />
        public abstract Task PublishFromStorageAsync();
    }
}