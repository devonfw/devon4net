using Excalibur.Shared.Observable;
using Excalibur.Shared.Observable.Typed;
using Excalibur.Shared.Presentation;
using Excalibur.Shared.Presentation.Typed;

namespace Excalibur.Cross.ViewModels.Typed
{
    /// <inheritdoc />
    public abstract class DetailViewModelOfInt<TSelectedObservable, TPresentation> : DetailViewModel<int, TSelectedObservable, TPresentation>
        where TSelectedObservable : ObservableBaseOfInt, new()
        where TPresentation : class, ISinglePresentationOfInt<TSelectedObservable>
    {
    }
}