using System.Collections.Generic;
using Excalibur.Shared.Collections;
using Excalibur.Shared.Observable;
using Excalibur.Shared.Presentation;
using MvvmCross.Core.ViewModels;
using XLabs.Ioc;

namespace Excalibur.Cross.ViewModels
{
    /// <inheritdoc />
    public abstract class ListViewModel<TId, TObservable, TPresentation, TDetailViewModel> : ListViewModel<TId, TObservable, TObservable, TPresentation, TDetailViewModel>
        where TObservable : ObservableBase<TId>, new()
        where TPresentation : class, IListPresentation<TId, TObservable>
        where TDetailViewModel : IMvxViewModel
    {
    }

    /// <summary>
    /// Base ListViewModel implementation
    /// This will try to resolve the corresponding Presentation that will be used to bind various things
    /// 
    /// The base will provide some default methods that can be used for navigation like a <see cref="GoToDetailCommand"/> that will navigation to a detail based
    /// on a command binding or selection.
    /// </summary>
    /// <typeparam name="TId">  The type of Identifier to use for the database object. Ints, guids,
    ///                         etc. </typeparam>
    /// <typeparam name="TObservable">The type that should be used for the collections of objects</typeparam>
    /// <typeparam name="TSelectedObservable">The type that should be used for details information</typeparam>
    /// <typeparam name="TPresentation">The type that should be used to resolve Observables</typeparam>
    /// <typeparam name="TDetailViewModel">The type that should be used as detail view model</typeparam>
    public abstract class ListViewModel<TId, TObservable, TSelectedObservable, TPresentation, TDetailViewModel> : BaseViewModel
        where TObservable : ObservableBase<TId>, new()
        where TSelectedObservable : ObservableBase<TId>, new()
        where TPresentation : class, IListPresentation<TId, TObservable, TSelectedObservable>
        where TDetailViewModel : IMvxViewModel
    {
        private TSelectedObservable _selectedObservable = new TSelectedObservable();
        private IObservableCollection<TObservable> _observables = new ExObservableCollection<TObservable>(new List<TObservable>());
        private bool _isLoading;
        private IMvxAsyncCommand<TObservable> _goToDetailCommand;

        /// <summary>
        /// Initializes a new instance of ListViewModel 
        /// This will resolve the presentation and bind to various properties.
        /// </summary>
        protected ListViewModel()
        {
            var presentation = Resolver.Resolve<TPresentation>();
            Observables = presentation.Observables;
            SelectedObservable = presentation.SelectedObservable;
            IsLoading = presentation.IsLoading;
        }

        /// <summary>
        /// The observable collection that contains mapped domain objects
        /// </summary>
        public IObservableCollection<TObservable> Observables
        {
            get { return _observables; }
            set { SetProperty(ref _observables, value); }
        }

        /// <summary>
        /// Property used for indicating that the presentation is currently busy
        /// </summary>
        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }

        /// <summary>
        /// The selected observable when needing access to it on the list view
        /// </summary>
        public TSelectedObservable SelectedObservable
        {
            get { return _selectedObservable; }
            set { SetProperty(ref _selectedObservable, value); }
        }

        /// <summary>
        /// A navigation to detail command 
        /// This will set the <see cref="SelectedObservable"/> to the one selected and will navigate to the <see cref="TDetailViewModel"/>
        /// </summary>
        public virtual IMvxAsyncCommand<TObservable> GoToDetailCommand
        {
            get
            {
                _goToDetailCommand = _goToDetailCommand ?? new MvxAsyncCommand<TObservable>((selected) =>
                {
                    var presentation = Resolver.Resolve<TPresentation>();
                    presentation.SetSelectedObservable(selected.Id);

                    return NavigationService.Navigate<TDetailViewModel>();
                });

                return _goToDetailCommand;
            }
        }
    }
}