using Excalibur.Shared.Storage;

namespace Excalibur.Providers.File
{
    public class ObjectAsFileStorageProviderOfInt<T> : ObjectAsFileStorageProvider<int, T>
        where T : StorageDomainOfInt
    {
    }
}