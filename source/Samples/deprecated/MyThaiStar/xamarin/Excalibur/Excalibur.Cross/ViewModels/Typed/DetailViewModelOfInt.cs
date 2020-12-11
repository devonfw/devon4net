using Excalibur.Shared.Observable;
using Excalibur.Shared.Presentation;

// ReSharper disable once CheckNamespace
namespace Excalibur.Cross.ViewModels
{
    /// <inheritdoc />
    public abstract class DetailViewModelOfInt<TSelectedObservable, TPresentation> : DetailViewModel<int, TSelectedObservable, TPresentation>
        where TSelectedObservable : ObservableBaseOfInt, new()
        where TPresentation : class, ISinglePresentationOfInt<TSelectedObservable>
    {
    }
}