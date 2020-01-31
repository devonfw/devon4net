using Excalibur.Shared.Observable;

// ReSharper disable once CheckNamespace
namespace Excalibur.Shared.Presentation
{
    /// <inheritdoc />
    public interface ISinglePresentationOfInt<TSelectedObservable> : ISinglePresentation<int, TSelectedObservable>
        where TSelectedObservable : ObservableBaseOfInt, new()
    {
    }
}