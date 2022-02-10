using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Amazon.S3.Model;

namespace Devon4Net.Infrastructure.AWS.S3
{
    public interface IAwsS3Handler
    {
        Task<bool> UploadObject(Stream streamFile, string keyName, string bucketName, string contentType, bool autoCloseStream = false, List<Tag> tagList = null);
        Task<Stream> GetObject(string bucketName, string objectKey);
        Task<GetObjectMetadataResponse> GetObjectMetadata(string key, string bucketName);
        Task<bool> CheckObjectExists(string key, string bucketName);
    }
}
