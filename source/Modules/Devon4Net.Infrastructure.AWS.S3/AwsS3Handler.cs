using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Devon4Net.Infrastructure.Logger.Logging;

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
        #region Async methods
        public async Task<Stream> GetObject(string bucketName, string objectKey)
        {
            var request = new GetObjectRequest { BucketName = bucketName, Key = objectKey };
            var s3Client = GetS3Client(AwsRegion, AwsSecretAccessKeyId, AwsSecretAccessKey);
            var response = await s3Client.GetObjectAsync(request).ConfigureAwait(false);
            return response.ResponseStream;
        }

        public async Task<bool> UploadObject(Stream streamFile, string keyName, string bucketName, string contentType, bool autoCloseStream = false, List<Tag> tagList = null)
        {
            try
            {
                CheckUploadObjectParams(streamFile, keyName, bucketName);

                var s3Client = GetS3Client(AwsRegion, AwsSecretAccessKeyId, AwsSecretAccessKey);
                var fileTransferUtility = new TransferUtility(s3Client);

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
                if (!autoCloseStream) streamFile.Position = 0;
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Fatal(ex);
                throw;
            }

            return true;
        }

        public async Task<bool> UploadObjectWithMetadata(Stream streamFile, string keyName, string bucketName, string contentType, IDictionary<string, string> metadata, bool autoCloseStream = false)
        {
            try
            {
                CheckUploadObjectParams(streamFile, keyName, bucketName);
                var s3Client = GetS3Client(AwsRegion, AwsSecretAccessKeyId, AwsSecretAccessKey);

                var request = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName,
                    InputStream = streamFile,
                    ContentType = contentType,
                    CannedACL = S3CannedACL.BucketOwnerFullControl,
                    AutoCloseStream = autoCloseStream
                };

                foreach (var key in metadata)
                {
                    request.Metadata.Add(key.Key, key.Value);
                }

                var upload = await s3Client.PutObjectAsync(request).ConfigureAwait(false);
                if (!autoCloseStream) streamFile.Position = 0;
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Fatal(ex);
                throw;
            }

            return true;
        }

        public async Task<GetObjectMetadataResponse> GetObjectMetadata(string key, string bucketName)
        {
            try
            {
                var s3Client = GetS3Client(AwsRegion, AwsSecretAccessKeyId, AwsSecretAccessKey);
                return await s3Client.GetObjectMetadataAsync(new GetObjectMetadataRequest { Key = key, BucketName = bucketName }).ConfigureAwait(false);
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
                var s3Client = GetS3Client(AwsRegion, AwsSecretAccessKeyId, AwsSecretAccessKey);
                var response = await s3Client.GetObjectMetadataAsync(new GetObjectMetadataRequest { Key = key, BucketName = bucketName }).ConfigureAwait(false);
                if (response.HttpStatusCode == System.Net.HttpStatusCode.NotFound) return false;
                return response.HttpStatusCode == System.Net.HttpStatusCode.OK && response.LastModified != DateTime.MinValue && response.LastModified != DateTime.MaxValue && response.LastModified != default;
            }
            catch (AmazonS3Exception ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound) return false;
                throw;
            }
        }
        public async Task<bool> DeleteFiles(string key, string bucketName)
        {
            var s3Client = GetS3Client(AwsRegion, AwsSecretAccessKeyId, AwsSecretAccessKey);
            try
            {
                await s3Client.DeleteObjectAsync(key, bucketName).ConfigureAwait(false);
                return true;
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error(ex, $"File {key} could not be deleted.");
                return false;
            }
        }

        public async Task<List<S3Object>> GetAllFiles(string bucketName, int maxKeys = 1000, string prefix = null)
        {
            try
            {
                var allObjects = new List<S3Object>();
                var s3Client = GetS3Client(AwsRegion, AwsSecretAccessKeyId, AwsSecretAccessKey);
                var listObjectsrequest = new ListObjectsV2Request()
                {
                    BucketName = bucketName,
                    Prefix = prefix,
                    MaxKeys = maxKeys,

                };
                var listofObjects = await s3Client.ListObjectsV2Async(listObjectsrequest).ConfigureAwait(false);
                allObjects.AddRange(listofObjects.S3Objects);
                while (listofObjects.IsTruncated)
                {
                    listObjectsrequest.ContinuationToken = listofObjects.NextContinuationToken;
                    listofObjects = await s3Client.ListObjectsV2Async(listObjectsrequest).ConfigureAwait(false);
                    allObjects.AddRange(listofObjects.S3Objects);
                }
                return allObjects;
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error(ex, $"Could not get files for {bucketName} bucket");
                throw;
            }
        }

        public async Task CopyObject(string sourceBucket, string sourceKey, string destinationBucket, string destinationKey, Dictionary<string, string> newMetadata)
        {
            try
            {
                var s3Client = GetS3Client(AwsRegion, AwsSecretAccessKeyId, AwsSecretAccessKey);
                var copyObjectRequest = new CopyObjectRequest
                {
                    BucketKeyEnabled = true,
                    SourceBucket = sourceBucket,
                    DestinationBucket = destinationBucket,
                    SourceKey = sourceKey,
                    DestinationKey = destinationKey,
                    CannedACL = S3CannedACL.BucketOwnerFullControl,
                    MetadataDirective = S3MetadataDirective.REPLACE
                };
                foreach (var key in newMetadata)
                {
                    copyObjectRequest.Metadata.Add(key.Key, key.Value);
                }

                await s3Client.CopyObjectAsync(copyObjectRequest).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error(ex, $"Could not copy or update the desired object: {sourceKey}");
                throw;
            }
        }

        public async Task<List<string>> GetDirectoriesNameFromBucket(string bucketName, List<string> foldersInBucket)
        {
            var s3Client = GetS3Client(AwsRegion, AwsSecretAccessKeyId, AwsSecretAccessKey);
            var listObjectsrequest = new ListObjectsV2Request()
            {
                BucketName = bucketName,
                Delimiter = "/" //This is only to get the folder
            };
            foldersInBucket = await CreateListOfDirectories(bucketName, foldersInBucket, s3Client, listObjectsrequest).ConfigureAwait(false);

            return foldersInBucket;
        }

        public async Task<string> GetDirectoryNameFromBucket(string bucketName, string prefix)
        {
            var s3Client = GetS3Client(AwsRegion, AwsSecretAccessKeyId, AwsSecretAccessKey);
            var listObjectsrequest = new ListObjectsV2Request()
            {
                BucketName = bucketName,
                Prefix = prefix,
                Delimiter = "/" //This is only to get the folder
            };

            var listofObjects = await s3Client.ListObjectsV2Async(listObjectsrequest).ConfigureAwait(false);
            return listofObjects.CommonPrefixes.FirstOrDefault();

        }

        #endregion
        #region Sync methods
        // https://docs.microsoft.com/en-us/archive/msdn-magazine/2015/july/async-programming-brownfield-async-development#the-blocking-hack
        public Stream GetObjectSync(string bucketName, string objectKey)
        {
            return GetObject(bucketName, objectKey).GetAwaiter().GetResult();
        }

        public bool UploadObjectSync(Stream streamFile, string keyName, string bucketName, string contentType, bool autoCloseStream = false, List<Tag> tagList = null)
        {
            return UploadObject(streamFile, keyName, bucketName, contentType, autoCloseStream, tagList).GetAwaiter().GetResult();
        }

        public bool UploadObjectWithMetadataSync(Stream streamFile, string keyName, string bucketName, string contentType, IDictionary<string, string> metadata, bool autoCloseStream = false)
        {
            return UploadObjectWithMetadata(streamFile, keyName, bucketName, contentType, metadata, autoCloseStream).GetAwaiter().GetResult();
        }

        public GetObjectMetadataResponse GetObjectMetadataSync(string key, string bucketName)
        {
            return GetObjectMetadata(key, bucketName).GetAwaiter().GetResult();
        }

        public bool CheckObjectExistsSync(string key, string bucketName)
        {
            return CheckObjectExists(key, bucketName).GetAwaiter().GetResult();
        }

        public List<S3Object> GetAllFilesSync(string bucketName, int maxKeys = 1000, string prefix = null)
        {
            return GetAllFiles(bucketName, maxKeys, prefix).GetAwaiter().GetResult();
        }

        public bool DeleteFilesSync(string key, string bucketName)
        {
            return DeleteFiles(key, bucketName).GetAwaiter().GetResult();
        }

        public void CopyObjectSync(string sourceBucket, string sourceKey, string destinationBucket, string destinationKey, Dictionary<string, string> newMetadata)
        {
            CopyObject(sourceBucket, sourceKey, destinationBucket, destinationKey, newMetadata).GetAwaiter().GetResult();
        }

        public List<string> GetDirectoriesNameFromBucketSync(string bucketName, List<string> foldersInBucket)
        {
            return GetDirectoriesNameFromBucket(bucketName, foldersInBucket).GetAwaiter().GetResult();
        }

        public string GetDirectoryNameFromBucketSync(string bucketName, string prefix)
        {
            return GetDirectoryNameFromBucket(bucketName, prefix).GetAwaiter().GetResult();
        }

        #endregion
        #region Private methods
        private void CheckUploadObjectParams(Stream streamFile, string keyName, string bucketName)
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
        private async Task<List<string>> CreateListOfDirectories(string bucketName, List<string> foldersInBucket, IAmazonS3 s3Client, ListObjectsV2Request listObjectsrequest)
        {
            var listofObjects = await s3Client.ListObjectsV2Async(listObjectsrequest).ConfigureAwait(false);
            foldersInBucket.AddRange(listofObjects.CommonPrefixes);
            if (listofObjects.CommonPrefixes.Count != 0)
            {
                foreach (var prefix in listofObjects.CommonPrefixes)
                {
                    listObjectsrequest.Prefix = prefix;
                    await CreateListOfDirectories(bucketName, foldersInBucket, s3Client, listObjectsrequest);
                }
            }

            return foldersInBucket;
        }
        #endregion
    }
}
