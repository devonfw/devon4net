using System;
using System.Collections.Generic;
using System.Linq;
using Excalibur.Shared.Business;
using Excalibur.Shared.Collections;
using Excalibur.Shared.Comparers;
using Excalibur.Shared.ObjectConverter;
using Excalibur.Shared.Observable;
using Excalibur.Shared.Storage;
using Excalibur.Shared.Utils;
using PubSub;
using XLabs.Ioc;

namespace Excalibur.Shared.Presentation
{
    // Todo change to BasePresentation and implement some methods differently...

    public abstract class BaseSortedPresentation<TId, TDomain, TObservable, TSelectedObservable, TComparer> : BasePresentation<TId, TDomain, TSelectedObservable>, ISortedPresentation<TId, TObservable, TSelectedObservable>
        where TDomain : StorageDomain<TId>
        where TObservable : ObservableBase<TId>, new()
        where TSelectedObservable : ObservableBase<TId>, new()
        where TComparer : BaseComparer<TObservable>, new()
    {
        private ISortedObservableCollection<TObservable> _observables = new ExSortedObservableCollection<TObservable>(new TComparer());
        protected IObjectMapper<TDomain, TObservable> DomainObservableMapper { get; set; }
        protected IObjectMapper<TObservable, TSelectedObservable> ObservableSelectedMapper { get; set; }

        protected BaseSortedPresentation()
        {
            // retrieve mappers
            this.Subscribe<MessageBase<IList<TDomain>>>(ListUpdatedHandler);
            this.Subscribe<MessageBase<TDomain>>(ItemUpdatedHandler);

            DomainObservableMapper = Resolver.Resolve<IObjectMapper<TDomain, TObservable>>();
            ObservableSelectedMapper = Resolver.Resolve<IObjectMapper<TObservable, TSelectedObservable>>();
        }

        protected virtual async void ListUpdatedHandler(MessageBase<IList<TDomain>> messageBase)
        {
            // Might need to add new threads and main thread requests.
            IsLoading = true;

            var objects = await Resolver.Resolve<IListBusiness<TId, TDomain>>().GetAllAsync().ConfigureAwait(false);

            var deleteIds = 0;
            try
            {
                deleteIds = Observables.Select(x => x.Id).Except(objects.Select(x => x.Id)).Count();
            }
            catch (Exception)
            {
            }

            var count = objects.Count + deleteIds;
            VerifyAndResetCountdown(count);

            var dispatcher = Resolver.Resolve<IExMainThreadDispatcher>();

            foreach (var observable in Observables.Reverse())
            {
                TObservable tmpObservable = observable;
                dispatcher.InvokeOnMainThread(() =>
                {
                    Observables.Remove(tmpObservable);
                    SignalCde();
                });
            }

            foreach (var domainObject in objects)
            {
                if (ObservablesContainsId(domainObject.Id))
                {
                    var observable = Observables.First(x => x.Id.Equals(domainObject.Id));
                    DomainObservableMapper.UpdateDestination(domainObject, observable);
                    SignalCde();
                }
                else
                {
                    var observable = DomainObservableMapper.Map(domainObject);
                    dispatcher.InvokeOnMainThread(() =>
                    {
                        Observables.InsertItem(observable);
                        SignalCde();
                    });
                }
            }

            if (SelectedObservable.IsTransient() && Observables.Any())
            {
                ObservableSelectedMapper.UpdateDestination(Observables.First(), SelectedObservable);
            }

            IsLoading = false;
        }

        protected virtual void ItemUpdatedHandler(MessageBase<TDomain> messageBase)
        {
            // Update item in the list
            var itemInList = Observables.FirstOrDefault(x => x.Id.Equals(messageBase.Object.Id));
            if (itemInList != null)
            {
                DomainObservableMapper.UpdateDestination(messageBase.Object, itemInList);
            }

            // Update the selected item only if updated and is selected
            if (SelectedObservable.Id.Equals(messageBase.Object.Id) && messageBase.State == EDomainState.Updated)
            {
                DomainSelectedMapper.UpdateDestination(messageBase.Object, SelectedObservable);
            }
        }

        public ISortedObservableCollection<TObservable> Observables
        {
            get { return _observables; }
            set { SetProperty(ref _observables, value); }
        }

        protected virtual bool ObservablesContainsId(TId id)
        {
            return Observables.FirstOrDefault(x => x.Id.Equals(id)) != null;
        }

        public virtual void SetSelectedObservable(TId observableId)
        {
            try
            {
                if (Observables.Any())
                {
                    var usedObservable = Observables.FirstOrDefault(x => x.Id.Equals(observableId));
                    if (usedObservable != null)
                    {
                        ObservableSelectedMapper.UpdateDestination(usedObservable, SelectedObservable);
                    }
                    else
                    {
                        var result = Resolver.Resolve<IListBusiness<TId, TDomain>>().GetByIdAsync(observableId).Result; // Todo make method async?
                        if (result != null)
                        {
                            DomainSelectedMapper.UpdateDestination(result, SelectedObservable);
                        }
                    }
                }
            }
            catch (Exception)
            {
                // todo logging
            }
        }
    }
}
