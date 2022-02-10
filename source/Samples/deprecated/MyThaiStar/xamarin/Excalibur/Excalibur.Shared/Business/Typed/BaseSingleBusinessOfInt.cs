using Excalibur.Shared.Services;
using Excalibur.Shared.Storage;

// ReSharper disable once CheckNamespace
namespace Excalibur.Shared.Business
{
    /// <inheritdoc />
    public class BaseSingleBusinessOfInt<TDomain> : BaseSingleBusinessOfInt<TDomain, IServiceBase<TDomain>>
        where TDomain : StorageDomainOfInt, new()
    {
    }

    /// <inheritdoc />
    public class BaseSingleBusinessOfInt<TDomain, TService> : BaseSingleBusiness<int, TDomain, TService>
        where TDomain : StorageDomainOfInt, new()
        where TService : class, IServiceBase<TDomain>
    {
    }
}