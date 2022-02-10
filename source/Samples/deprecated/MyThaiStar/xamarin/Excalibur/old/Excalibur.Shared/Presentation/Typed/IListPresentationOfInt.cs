using Excalibur.Shared.Observable;

namespace Excalibur.Shared.Presentation
{
    /// <inheritdoc />
    public interface IListPresentationOfInt<TObservable> : IListPresentationOfInt<TObservable, TObservable>
        where TObservable : ObservableBaseOfInt, new()
    {
    }

    /// <inheritdoc />
    public interface IListPresentationOfInt<TObservable, TSelectedObservable> : IListPresentation<int, TObservable, TSelectedObservable>
        where TObservable : ObservableBaseOfInt, new()
        where TSelectedObservable : ObservableBaseOfInt, new()
    {
    }
}