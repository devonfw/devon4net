using System;
using Excalibur.Shared.ObjectConverter;
using Excalibur.Shared.Observable;
using Excalibur.Shared.Storage;
using Excalibur.Shared.Utils;
using PubSub;
using XLabs.Ioc;

namespace Excalibur.Shared.Presentation
{
    /// <summary>
    /// Presentation will make it possible to use one entity for sharing observable objects. 
    /// Presentation base will map domain objects to observable object which can be passed by reference to view models. 
    /// 
    /// Using a presentation sharing of lists and updating views is easier, since mapping and managing will be done in one entity.
    /// 
    /// This base provides an implementation for Domain objects based on a single object.
    /// 
    /// Single presentation will manage the one object. 
    /// The selected observable will be used by the List implementation <see cref="IListPresentation{TId,TObservable,TSelectedObservable}"/> as the reference
    /// that will be updated when selecting a Observable.
    /// </summary>
    /// <typeparam name="TId">  The type of Identifier to use for the database object. Ints, guids,
    ///                         etc. </typeparam>
    /// <typeparam name="TDomain">The type of the object that wants to be stored</typeparam>
    /// <typeparam name="TObservable">The type that should be used for details information</typeparam>
    public class BaseSinglePresentation<TId, TDomain, TObservable> : ObservableObjectBase, ISinglePresentation<TId, TObservable>
    where TDomain : StorageDomain<TId>
    where TObservable : ObservableBase<TId>, new()
    {
        private bool _isLoading = true;
        private TObservable _selectedObservable = new TObservable();
        protected IObjectMapper<TDomain, TObservable> DomainSelectedMapper { get; set; }

        /// <summary>
        /// Initializes a new BaseSinglePresentation 
        /// This Resolves the Domain to Selected mapper
        /// Also subscribes to Single item publish message
        /// </summary>
        public BaseSinglePresentation()
        {
            // retrieve mappers
            this.Subscribe<MessageBase<TDomain>>(ItemUpdatedHandler);

            DomainSelectedMapper = Resolver.Resolve<IObjectMapper<TDomain, TObservable>>();
        }

        /// <summary>
        /// Property used for indicating that the presentation is currently busy
        /// </summary>
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        /// <summary>
        /// The selected observable
        /// </summary>
        public TObservable SelectedObservable
        {
            get => _selectedObservable;
            set => SetProperty(ref _selectedObservable, value);
        }

        /// <inheritdoc />
        public virtual void Initialize()
        {
        }

        /// <summary>
        /// Handler that manages single object updates.
        /// 
        /// This will update an object as the selected observable.
        /// </summary>
        /// <param name="messageBase"></param>
        protected virtual void ItemUpdatedHandler(MessageBase<TDomain> messageBase)
        {
            DomainSelectedMapper.UpdateDestination(messageBase.Object, SelectedObservable);
        }

        /// <inheritdoc />
        ~BaseSinglePresentation()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                this.Unsubscribe<TDomain>();
            }
        }
    }
}
