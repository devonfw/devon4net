namespace Excalibur.Shared.Storage
{
    /// <inheritdoc />
    public interface IObjectStorageProviderOfInt<T> : IObjectStorageProvider<int, T>
        where T : StorageDomainOfInt
    {
    }
}