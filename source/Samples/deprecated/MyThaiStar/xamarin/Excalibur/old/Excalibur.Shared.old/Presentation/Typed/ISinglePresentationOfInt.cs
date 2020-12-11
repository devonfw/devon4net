using Excalibur.Shared.Observable.Typed;

namespace Excalibur.Shared.Presentation.Typed
{
    /// <inheritdoc />
    public interface ISinglePresentationOfInt<TSelectedObservable> : ISinglePresentation<int, TSelectedObservable>
        where TSelectedObservable : ObservableBaseOfInt, new()
    {
    }
}