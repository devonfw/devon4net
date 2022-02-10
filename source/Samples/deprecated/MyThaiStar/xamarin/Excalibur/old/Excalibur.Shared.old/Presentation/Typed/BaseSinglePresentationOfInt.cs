using Excalibur.Shared.Observable.Typed;
using Excalibur.Shared.Storage.Typed;

namespace Excalibur.Shared.Presentation.Typed
{
    ///  <inheritdoc />
    public class BaseSinglePresentationOfInt<TDomain, TObservable> : BaseSinglePresentation<int, TDomain, TObservable>
        where TDomain : StorageDomainOfInt
        where TObservable : ObservableBaseOfInt, new()
    {
    }
}