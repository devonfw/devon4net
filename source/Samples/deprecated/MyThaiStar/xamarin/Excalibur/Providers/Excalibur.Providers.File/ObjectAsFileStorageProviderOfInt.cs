using Excalibur.Shared.Storage;

namespace Excalibur.Providers.File
{
    /// <inheritdoc />
    /// <summary>
    /// <see cref="ObjectAsFileStorageProvider{TId,T}"/> but where TId is set to int
    /// </summary>
    public class ObjectAsFileStorageProviderOfInt<T> : ObjectAsFileStorageProvider<int, T>
        where T : StorageDomainOfInt
    {
    }
}