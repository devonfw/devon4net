using Excalibur.Shared.Observable;

namespace Excalibur.Shared.Presentation
{
    /// <inheritdoc />
    public interface ISinglePresentationOfInt<TSelectedObservable> : ISinglePresentation<int, TSelectedObservable>
        where TSelectedObservable : ObservableBaseOfInt, new()
    {
    }
}