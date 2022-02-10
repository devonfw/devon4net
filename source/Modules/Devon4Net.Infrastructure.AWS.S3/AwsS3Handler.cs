using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Devon4Net.Infrastructure.Log;

namespace Devon4Net.Infrastructure.AWS.S3
{
    public class AwsS3Handler : IAwsS3Handler
    {
        private string AwsRegion { get; }
        private string AwsSecretAccessKeyId { get; }
        private string AwsSecretAccessKey { get; }

        public AwsS3Handler(string awsRegion, string awsSecretAccessKeyId, string awsSecretAccessKey)
        {
            AwsRegion = awsRegion;
            AwsSecretAccessKey = awsSecretAccessKey;
            AwsSecretAccessKeyId = awsSecretAccessKeyId;
        }

        public async Task<Stream> GetObject(string bucketName, string objectKey)
        {
            var request = new GetObjectRequest { BucketName = bucketName, Key = objectKey };
            using var s3Client = GetS3Client(AwsRegion, AwsSecretAccessKeyId, AwsSecretAccessKey);
            var response = await s3Client.GetObjectAsync(request).ConfigureAwait(false);
            return response.ResponseStream;
        }

        public async Task<bool> UploadObject(Stream streamFile, string keyName, string bucketName, string contentType, bool autoCloseStream = false, List<Tag> tagList = null)
        {
            try
            {
                if (streamFile == null || streamFile.Length == 0 || !streamFile.CanRead)
                {
                    Devon4NetLogger.Fatal("No base64Image to create the S3 upload");
                    throw new ArgumentException("No base64Image to create the S3 upload");
                }

                if (string.IsNullOrEmpty(keyName))
                {
                    Devon4NetLogger.Fatal("No keyName provided to create the S3 upload");
                    throw new ArgumentException("No keyName provided to create the S3 upload");
                }

                if (string.IsNullOrEmpty(bucketName))
                {
                    Devon4NetLogger.Fatal("No bucketName provided to create the S3 upload");
                    throw new ArgumentException("No bucketName provided to create the S3 upload");
                }

                using var s3Client = GetS3Client(AwsRegion, AwsSecretAccessKeyId, AwsSecretAccessKey);
                using var fileTransferUtility = new TransferUtility(s3Client);
                
                var transferUtilityUploadRequest = new TransferUtilityUploadRequest
                {
                    InputStream = streamFile,
                    Key = keyName,
                    BucketName = bucketName,
                    CannedACL = S3CannedACL.BucketOwnerFullControl,
                    ContentType = contentType,
                    AutoCloseStream = autoCloseStream,
                    TagSet = tagList ?? new List<Tag>()
                };

                await fileTransferUtility.UploadAsync(transferUtilityUploadRequest).ConfigureAwait(false);
                if (autoCloseStream == false) streamFile.Position = 0;
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Fatal(ex);
                throw;
            }

            return true;
        }

        private IAmazonS3 GetS3Client(string awsRegion, string awsSecretAccessKeyId, string awsSecretAccessKey)
        {
            if (string.IsNullOrEmpty(awsRegion))
            {
                Devon4NetLogger.Fatal("No region provided to create the S3 client");
                throw new ArgumentException("No region provided to create the S3 client");
            }

            if (string.IsNullOrEmpty(awsSecretAccessKeyId))
            {
                Devon4NetLogger.Fatal("No awsSecretAccessKeyId provided to create the S3 client");
                throw new ArgumentException("No awsSecretAccessKeyId provided to create the S3 client");
            }

            if (string.IsNullOrEmpty(awsSecretAccessKey))
            {
                Devon4NetLogger.Fatal("No awsSecretAccessKey provided to create the S3 client");
                throw new ArgumentException("No awsSecretAccessKey provided to create the S3 client");
            }

            var region = RegionEndpoint.GetBySystemName(awsRegion);

            return new AmazonS3Client(awsSecretAccessKeyId, awsSecretAccessKey, region);
        }

        public async Task<GetObjectMetadataResponse> GetObjectMetadata(string key, string bucketName)
        {
            try
            {
                using var s3Client = GetS3Client(AwsRegion, AwsSecretAccessKeyId, AwsSecretAccessKey);
                return await s3Client.GetObjectMetadataAsync(new GetObjectMetadataRequest {Key = key, BucketName = bucketName});
            }
            catch (AmazonS3Exception ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound) return null;
                throw;
            }
        }

        public async Task<bool> CheckObjectExists(string key, string bucketName)
        {
            try
            {
                using var s3Client = GetS3Client(AwsRegion, AwsSecretAccessKeyId, AwsSecretAccessKey);
                var response = await s3Client.GetObjectMetadataAsync(new GetObjectMetadataRequest { Key = key, BucketName = bucketName });
                if (response.HttpStatusCode == System.Net.HttpStatusCode.NotFound) return false;
                return response.HttpStatusCode == System.Net.HttpStatusCode.OK && response.LastModified != DateTime.MinValue && response.LastModified != DateTime.MaxValue && response.LastModified != default;
            }
            catch (AmazonS3Exception ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound) return false;
                throw;
            }
        }
    }
}
