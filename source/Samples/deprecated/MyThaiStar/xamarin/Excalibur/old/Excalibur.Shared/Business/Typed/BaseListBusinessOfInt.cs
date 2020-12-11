using System.Collections.Generic;
using Excalibur.Shared.Services;
using Excalibur.Shared.Storage;

namespace Excalibur.Shared.Business
{
    /// <inheritdoc />
    public class BaseListBusinessOfInt<TDomain> : BaseListBusinessOfInt<TDomain, IServiceBase<IList<TDomain>>> 
        where TDomain : StorageDomainOfInt, new()
    {
    }

    /// <inheritdoc cref="BaseListBusiness{TId, TDomain, TService}" />
    public class BaseListBusinessOfInt<TDomain, TService> : BaseListBusiness<int, TDomain, TService>, IListBusinessOfInt<TDomain>
        where TDomain : StorageDomainOfInt, new()
        where TService : class, IServiceBase<IList<TDomain>>
    {
    }
}