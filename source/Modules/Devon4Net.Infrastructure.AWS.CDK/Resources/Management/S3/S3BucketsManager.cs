using Amazon.CDK;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.S3;
using Amazon.CDK.AWS.S3.Notifications;
using Devon4Net.Infrastructure.AWS.CDK.Resources.Management.S3;
using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Management
{
    public partial class AwsCdkHandlerManager : IS3BucketsManager
    {
        public IBucket AddS3Bucket(string bucketName, int expirationDays = 0, IList<ILifecycleRule> lifecycleRules = null, RemovalPolicy removalPolicy = RemovalPolicy.DESTROY, BucketEncryption encryption = BucketEncryption.KMS_MANAGED, string webSiteRedirectHost = "", bool versioned = true, bool enforceSSL = false, bool blockPublicAccess = true)
        {
            return HandlerResources.AwsCdkS3Handler.Create(bucketName, expirationDays, lifecycleRules, removalPolicy, encryption, webSiteRedirectHost, versioned, enforceSSL, blockPublicAccess);
        }

        public IBucket LocateBucketByName(string identification, string bucketName)
        {
            return HandlerResources.AwsCdkS3Handler.LocateFromName(identification, bucketName);
        }

        public IBucket LocateBucketByArn(string identification, string arn)
        {
            return HandlerResources.AwsCdkS3Handler.LocateFromArn(identification, arn);
        }

        public ILifecycleRule CreateLifecycleRule(string id, int expirationTime, string expirationTagName, string expirationTagValue, bool isVersionedBucket, int? previousVersionsExpirationDays = null)
        {
            return HandlerResources.AwsCdkS3Handler.CreateLifecycleRule(id, expirationTime, expirationTagName, expirationTagValue, isVersionedBucket, previousVersionsExpirationDays);
        }

        public LambdaDestination CreateLambdaDestination(IFunction lambdaFunction)
        {
            return HandlerResources.AwsCdkS3Handler.CreateLambdaDestination(lambdaFunction);
        }

        public void AddEventNotificationToS3Bucket(Bucket bucket, EventType eventType, IBucketNotificationDestination destination)
        {
            HandlerResources.AwsCdkS3Handler.AddEventNotification(bucket, eventType, destination);
        }
    }
}
