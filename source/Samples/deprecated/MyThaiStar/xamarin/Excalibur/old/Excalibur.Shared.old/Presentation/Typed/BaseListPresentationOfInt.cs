using Excalibur.Shared.Observable.Typed;
using Excalibur.Shared.Storage.Typed;

namespace Excalibur.Shared.Presentation.Typed
{
    /// <inheritdoc cref="BaseListPresentationOfInt{TDomain,TObservable,TObservable}"/>
    public class BaseListPresentationOfInt<TDomain, TObservable> : BaseListPresentationOfInt<TDomain, TObservable, TObservable>, IListPresentationOfInt<TObservable>
        where TDomain : StorageDomainOfInt
        where TObservable : ObservableBaseOfInt, new()
    {
    }

    /// <inheritdoc cref="BaseListPresentation{TId,TDomain,TObservable,TSelectedObservable}"/>
    public class BaseListPresentationOfInt<TDomain, TObservable, TSelectedObservable> : BaseListPresentation<int, TDomain, TObservable, TSelectedObservable>, IListPresentationOfInt<TObservable, TSelectedObservable>
        where TDomain : StorageDomainOfInt
        where TObservable : ObservableBaseOfInt, new()
        where TSelectedObservable : ObservableBaseOfInt, new()
    {
    }
}