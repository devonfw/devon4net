using Amazon.S3.Model;

namespace Devon4Net.Infrastructure.AWS.S3
{
    public interface IAwsS3Handler
    {
        Task<bool> UploadObject(Stream streamFile, string keyName, string bucketName, string contentType, bool autoCloseStream = false, List<Tag> tagList = null);
        Task<bool> UploadObjectWithMetadata(Stream streamFile, string keyName, string bucketName, string contentType, IDictionary<string, string> metadata, bool autoCloseStream = false);
        Task<Stream> GetObject(string bucketName, string objectKey);
        Task<GetObjectMetadataResponse> GetObjectMetadata(string key, string bucketName);
        Task<bool> CheckObjectExists(string key, string bucketName);
        Task<bool> DeleteFiles(string key, string bucketName);
        Task<List<string>> GetDirectoriesNameFromBucket(string bucketName, List<string> foldersInBucket);
        Task CopyObject(string sourceBucket, string sourceKey, string destinationBucket, string destinationKey, Dictionary<string, string> newMetadata);
        Task<string> GetDirectoryNameFromBucket(string bucketName, string prefix);
        Task<List<S3Object>> GetAllFiles(string bucketName, int maxKeys = 1000, string prefix = null);
        Stream GetObjectSync(string bucketName, string objectKey);
        bool UploadObjectSync(Stream streamFile, string keyName, string bucketName, string contentType, bool autoCloseStream = false, List<Tag> tagList = null);
        bool UploadObjectWithMetadataSync(Stream streamFile, string keyName, string bucketName, string contentType, IDictionary<string, string> metadata, bool autoCloseStream = false);
        GetObjectMetadataResponse GetObjectMetadataSync(string key, string bucketName);
        bool CheckObjectExistsSync(string key, string bucketName);
        List<S3Object> GetAllFilesSync(string bucketName, int maxKeys = 1000, string prefix = null);
        bool DeleteFilesSync(string key, string bucketName);
        void CopyObjectSync(string sourceBucket, string sourceKey, string destinationBucket, string destinationKey, Dictionary<string, string> newMetadata);
        List<string> GetDirectoriesNameFromBucketSync(string bucketName, List<string> foldersInBucket);
        string GetDirectoryNameFromBucketSync(string bucketName, string prefix);
    }
}
