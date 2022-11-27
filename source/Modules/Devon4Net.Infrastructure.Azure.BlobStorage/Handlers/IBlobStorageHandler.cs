using Azure.Storage.Blobs.Models;
using Devon4Net.Infrastructure.Azure.BlobStorage.Models;

namespace Devon4Net.Infrastructure.Azure.BlobStorage.Handlers
{
    public interface IBlobStorageHandler
    {
        /// <summary>
        /// Get object
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="keyName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Stream> GetObject(string containerName, string keyName, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Get Object properties
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="keyName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<BlobProperties> GetObjectProperties(string containerName, string keyName, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Get Object Tags
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="keyName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<GetBlobTagResult> GetObjectTags(string containerName, string keyName, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Get all objects
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="pageSizeHint"></param>
        /// <param name="prefix"></param>
        /// <param name="continuationToken"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<GetAllBlobItems> GetAllObjects(string containerName, int pageSizeHint = 1000, string prefix = null,
            string continuationToken = null, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Check if object exists
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="keyName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> CheckObjectExists(string containerName, string keyName, CancellationToken cancellationToken = default);

        /// <summary>
        /// Upload Object
        /// </summary>
        /// <param name="uploadObjectBlobStorage"></param>
        /// <param name="autoCloseStream"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> UploadObject(UploadObjectBlobStorage uploadObjectBlobStorage, bool autoCloseStream = false, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Delete object
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="keyName"></param>
        /// <param name="cancellationToken"></param>
        Task<bool> Delete(string containerName, string keyName, CancellationToken cancellationToken = default);
    }
}
