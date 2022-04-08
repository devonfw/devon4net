using Amazon.S3.Model;

namespace Devon4Net.Infrastructure.AWS.S3
{
    public interface IAwsS3Handler
    {
        Task<bool> UploadObject(Stream streamFile, string keyName, string bucketName, string contentType, bool autoCloseStream = false, List<Tag> tagList = null);
        Task<Stream> GetObject(string bucketName, string objectKey);
        Task<GetObjectMetadataResponse> GetObjectMetadata(string key, string bucketName);
        Task<bool> CheckObjectExists(string key, string bucketName);
        Stream GetObjectSync(string bucketName, string objectKey);
        bool UploadObjectSync(Stream streamFile, string keyName, string bucketName, string contentType, bool autoCloseStream = false, List<Tag> tagList = null);
        GetObjectMetadataResponse GetObjectMetadataSync(string key, string bucketName);
        bool CheckObjectExistsSync(string key, string bucketName);
    }
}
