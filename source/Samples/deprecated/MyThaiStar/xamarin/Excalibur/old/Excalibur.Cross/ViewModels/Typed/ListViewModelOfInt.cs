using Excalibur.Shared.Observable;
using Excalibur.Shared.Presentation;
using MvvmCross.Core.ViewModels;

namespace Excalibur.Cross.ViewModels
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