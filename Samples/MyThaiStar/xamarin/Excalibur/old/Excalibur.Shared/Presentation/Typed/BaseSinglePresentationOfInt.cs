using Excalibur.Shared.Observable;
using Excalibur.Shared.Storage;

namespace Excalibur.Shared.Presentation
{
    ///  <inheritdoc />
    public class BaseSinglePresentationOfInt<TDomain, TObservable> : BaseSinglePresentation<int, TDomain, TObservable>
        where TDomain : StorageDomainOfInt
        where TObservable : ObservableBaseOfInt, new()
    {
    }
}