using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Devon4Net.Infrastructure.Azure.BlobStorage.Models;
using Devon4Net.Infrastructure.Common;
using System.Net;

namespace Devon4Net.Infrastructure.Azure.BlobStorage.Handlers
{
    /// <summary>
    /// Blob Storage Handler
    /// </summary>
    public class BlobStorageHandler : IBlobStorageHandler
    {
        private BlobServiceClient BlobServiceClient { get; }

        public BlobStorageHandler(BlobServiceClient blobServiceClient)
        {
            BlobServiceClient = blobServiceClient;
        }

        /// <summary>
        /// Get object
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="keyName"></param>
        /// <param name="cancellationToken"></param>
        public async Task<Stream> GetObject(string containerName, string keyName, CancellationToken cancellationToken = default)
        {
            var blobClient = GetBlobClient(BlobServiceClient, containerName, keyName);
            var response = await blobClient.DownloadContentAsync(cancellationToken).ConfigureAwait(false);
            return response.Value.Content.ToStream();
        }

        /// <summary>
        /// Get Object properties
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="keyName"></param>
        /// <param name="cancellationToken"></param>
        public async Task<BlobProperties> GetObjectProperties(string containerName, string keyName, CancellationToken cancellationToken = default)
        {
            var blobClient = GetBlobClient(BlobServiceClient, containerName, keyName);
            return await blobClient.GetPropertiesAsync(cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Get Object Tags
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="keyName"></param>
        /// <param name="cancellationToken"></param>
        public async Task<GetBlobTagResult> GetObjectTags(string containerName, string keyName, CancellationToken cancellationToken = default)
        {
            var blobClient = GetBlobClient(BlobServiceClient, containerName, keyName);
            return await blobClient.GetTagsAsync(cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Get all objects
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="pageSizeHint"></param>
        /// <param name="prefix"></param>
        /// <param name="continuationToken"></param>
        /// <param name="cancellationToken"></param>
        public async Task<GetAllBlobItems> GetAllObjects(string containerName, int pageSizeHint = 1000, string prefix = null, string continuationToken = null, CancellationToken cancellationToken = default)
        {
            var containerClient = BlobServiceClient.GetBlobContainerClient(containerName);

            var page = containerClient.GetBlobsAsync(prefix: prefix, cancellationToken:cancellationToken).AsPages(continuationToken, pageSizeHint: pageSizeHint);
            var blobList = page.GetAsyncEnumerator(cancellationToken);
            await blobList.MoveNextAsync();
           
            return new GetAllBlobItems
            {
                Blobs = blobList.Current.Values,
                ContinuationToken = blobList.Current.ContinuationToken
            };
        }

        /// <summary>
        /// Check if object exists
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="keyName"></param>
        /// <param name="cancellationToken"></param>
        public async Task<bool> CheckObjectExists(string containerName, string keyName, CancellationToken cancellationToken = default)
        {
            var blobClient = GetBlobClient(BlobServiceClient, containerName, keyName);
            var exists = await blobClient.ExistsAsync(cancellationToken);
            return exists.Value;
        }

        /// <summary>
        /// Upload Object
        /// </summary>
        /// <param name="uploadObjectBlobStorage"></param>
        /// <param name="autoCloseStream"></param>
        /// <param name="cancellationToken"></param>
        public async Task<bool> UploadObject(UploadObjectBlobStorage uploadObjectBlobStorage, bool autoCloseStream = false, CancellationToken cancellationToken = default)
        {
            try
            {
                CheckUploadObjectParams(uploadObjectBlobStorage.Stream, uploadObjectBlobStorage.KeyName, uploadObjectBlobStorage.ContainerName, uploadObjectBlobStorage.ContentType);

                var blobClient = GetBlobClient(BlobServiceClient, uploadObjectBlobStorage.ContainerName, uploadObjectBlobStorage.KeyName);

                BlobHttpHeaders headers = new()
                {
                    ContentType = uploadObjectBlobStorage.ContentType,
                };

                var upload = await blobClient.UploadAsync(uploadObjectBlobStorage.Stream, headers, cancellationToken: cancellationToken);

                var uploadResult = upload.GetRawResponse().Status == (int)HttpStatusCode.Created;

                var resultsetSetMetadataAndTags = await SetMetadataAndTags(uploadObjectBlobStorage.Metadata, uploadObjectBlobStorage.Tags, blobClient, cancellationToken);

                return uploadResult && resultsetSetMetadataAndTags;
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Fatal(ex);
                throw;
            }
        }

        /// <summary>
        /// Delete object
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="keyName"></param>
        /// <param name="cancellationToken"></param>
        public async Task<bool> Delete(string containerName, string keyName, CancellationToken cancellationToken = default)
        {
            try
            {
                var blobClient = GetBlobClient(BlobServiceClient, containerName, keyName);
                var resultDelete = await blobClient.DeleteAsync(cancellationToken: cancellationToken);
                return resultDelete.Status == (int)HttpStatusCode.Accepted;
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Fatal(ex);
                throw;
            }
        }

        private static BlobClient GetBlobClient(BlobServiceClient blobServiceClient, string containerName, string objectKey)
        {
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            return containerClient.GetBlobClient(objectKey);
        }

        private static void CheckUploadObjectParams(Stream streamFile, string keyName, string containerName, string contentType)
        {
            if (streamFile == null || streamFile.Length == 0 || !streamFile.CanRead)
            {
                Devon4NetLogger.Fatal("No object to be uploaded to Blob Storage");
                throw new ArgumentException("No object to be uploaded to Blob Storage");
            }

            if (string.IsNullOrEmpty(keyName))
            {
                Devon4NetLogger.Fatal("No keyName provided to be uploaded to Blob Storage");
                throw new ArgumentException("No keyName provided to be uploaded to Blob Storage");
            }

            if (string.IsNullOrEmpty(containerName))
            {
                Devon4NetLogger.Fatal("No ContainerName provided to be uploaded to Blob Storage");
                throw new ArgumentException("No ContainerName provided to be uploaded to Blob Storage");
            }

            if (string.IsNullOrEmpty(contentType))
            {
                Devon4NetLogger.Fatal("No contentType provided to upload object to Blob Storage");
                throw new ArgumentException("No contentType provided to upload object to Blob Storage");
            }
        }

        private static async Task<bool> SetMetadataAndTags(IDictionary<string, string> metadata, IDictionary<string, string> tags, BlobClient blobClient, CancellationToken cancellationToken)
        {
            bool resultSetMetadata = true;
            bool resultSetTags = true;

            if (metadata != null)
            {
                var setmetadata = await blobClient.SetMetadataAsync(metadata, cancellationToken: cancellationToken);
                resultSetMetadata = setmetadata.GetRawResponse().Status == (int)HttpStatusCode.OK;
            }

            if (tags != null)
            {
                var setTags = await blobClient.SetTagsAsync(tags, cancellationToken: cancellationToken);
                resultSetTags = setTags.Status == (int)HttpStatusCode.NoContent;
            }

            return resultSetMetadata && resultSetTags;
        }
    }
}
