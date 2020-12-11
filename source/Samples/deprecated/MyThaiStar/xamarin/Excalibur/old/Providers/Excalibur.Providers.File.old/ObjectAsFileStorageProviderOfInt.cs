using Excalibur.Shared.Storage;
using Excalibur.Shared.Storage.Typed;

namespace Excalibur.Providers.File
{
    public class ObjectAsFileStorageProviderOfInt<T> : ObjectAsFileStorageProvider<int, T>
        where T : StorageDomainOfInt
    {
    }
}