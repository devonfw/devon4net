using System.Threading.Tasks;

namespace Excalibur.Shared.Storage
{
    /// <summary>
    /// Base implementation for the <see cref="IStorageService"/> used for storing files. 
    /// </summary>
    public abstract class StorageServiceBase : IStorageService
    {
        /// <inheritdoc />
        public abstract Task<string> StoreAsync(string folder, string fullName, string contentAsString);
        /// <inheritdoc />
        public abstract Task<string> StoreAsync(string folder, string fullName, byte[] contentAsBytes);
        /// <inheritdoc />
        public abstract Task<string> ReadAsTextAsync(string folder, string fullName);
        /// <inheritdoc />
        public abstract Task<byte[]> ReadAsBinaryAsync(string folder, string fullName);
        /// <inheritdoc />
        public abstract void DeleteFile(string folder, string fullName);
        /// <inheritdoc />
        public abstract bool Exists(string folder, string fullName);
    }
}
