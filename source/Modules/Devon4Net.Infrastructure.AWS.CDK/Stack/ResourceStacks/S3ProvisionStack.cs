using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.S3;
using Devon4Net.Infrastructure.AWS.CDK.Options.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Devon4Net.Infrastructure.AWS.CDK.Stack
{
    public partial class ProvisionStack
    {
        private void CreateOrLocateS3Buckets()
        {
            if (CdkOptions == null || CdkOptions.S3Buckets?.Any() != true) return;

            foreach (var bucket in CdkOptions.S3Buckets)
            {
                if (bucket.LocateInsteadOfCreate)
                {
                    StackResources.Buckets.Add(bucket.Id, AwsCdkHandler.LocateBucketByName(bucket.Id, bucket.BucketName));
                }
                else
                {
                    List<ILifecycleRule> expiredDocumentsLifeCycleRules = null;

                    if (bucket.ExpireDocumentRules?.Any() == true)
                    {
                        expiredDocumentsLifeCycleRules = bucket.ExpireDocumentRules.Select(x => AwsCdkHandler.CreateLifecycleRule(x.RuleName, x.Expiration, x.TagName, x.TagValue, bucket.Versioned, x.PreviousVersionsExpirationDays)).ToList();
                    }

                    if (bucket.ExpirationDays.HasValue)
                    {
                        StackResources.Buckets.Add(bucket.Id, AwsCdkHandler.AddS3Bucket(bucket.BucketName, lifecycleRules: expiredDocumentsLifeCycleRules, versioned: bucket.Versioned, expirationDays: bucket.ExpirationDays.Value, enforceSSL: bucket.EnforceSSL, blockPublicAccess: bucket.BlockPublicAccess ?? true));
                    }
                    else
                    {
                        StackResources.Buckets.Add(bucket.Id, AwsCdkHandler.AddS3Bucket(bucket.BucketName, lifecycleRules: expiredDocumentsLifeCycleRules, versioned: bucket.Versioned, enforceSSL: bucket.EnforceSSL, blockPublicAccess: bucket.BlockPublicAccess ?? true));
                    }
                }
            }
        }

        private void CreateS3Events()
        {
            if (CdkOptions?.Lambdas?.Any() == true && CdkOptions?.S3Buckets?.Any() == true)
            {
                foreach (var bucketOption in CdkOptions.S3Buckets.Where(x => x.Events != null))
                {
                    var bucket = LocateBucket(bucketOption.Id, $"The bucket id {bucketOption.Id} in S3 events does not exist") as Bucket;
                    foreach (var s3Event in bucketOption.Events)
                    {
                        GetS3EventResouces(s3Event, out var eventType, out var lambda);
                        var lambdaDestination = AwsCdkHandler.CreateLambdaDestination(lambda);
                        AwsCdkHandler.AddEventNotificationToS3Bucket(bucket, eventType, lambdaDestination);
                    }
                }
            }
        }

        private void GetS3EventResouces(S3EventOption s3Event, out EventType eventType, out IFunction lambda)
        {
            if (!Enum.TryParse(s3Event.EventType, out eventType))
            {
                throw new ArgumentException($"The event type {s3Event.EventType} could not be parsed");
            }

            lambda = LocateLambda(s3Event.LambdaId, $"The lambda Id {s3Event.LambdaId} in S3 event does not exist");
        }

        private IBucket LocateBucket(string bucketId, string exceptionMessageIfBucketDoesNotExist, string exceptionMessageIfBucketIsEmpty = null)
        {
            return StackResources.Locate<IBucket>(bucketId, exceptionMessageIfBucketDoesNotExist, exceptionMessageIfBucketIsEmpty);
        }
    }
}
