namespace Excalibur.Shared.Storage.Typed
{
    /// <inheritdoc />
    public interface IObjectStorageProviderOfInt<T> : IObjectStorageProvider<int, T>
        where T : StorageDomainOfInt
    {
    }
}