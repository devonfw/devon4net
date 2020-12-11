using Excalibur.Shared.Observable;
using Excalibur.Shared.Observable.Typed;
using Excalibur.Shared.Presentation;
using Excalibur.Shared.Presentation.Typed;
using MvvmCross.Core.ViewModels;

namespace Excalibur.Cross.ViewModels.Typed
{
    /// <inheritdoc />
    public abstract class ListViewModelOfInt<TObservable, TPresentation, TDetailViewModel> : ListViewModelOfInt<TObservable, TObservable, TPresentation, TDetailViewModel>
        where TObservable : ObservableBaseOfInt, new()
        where TPresentation : class, IListPresentationOfInt<TObservable>
        where TDetailViewModel : IMvxViewModel
    {
    }

    /// <inheritdoc />
    public abstract class ListViewModelOfInt<TObservable, TSelectedObservable, TPresentation, TDetailViewModel> : ListViewModel<int, TObservable, TSelectedObservable, TPresentation, TDetailViewModel>
        where TObservable : ObservableBaseOfInt, new()
        where TSelectedObservable : ObservableBaseOfInt, new()
        where TPresentation : class, IListPresentationOfInt<TObservable, TSelectedObservable>
        where TDetailViewModel : IMvxViewModel
    {
    }
}