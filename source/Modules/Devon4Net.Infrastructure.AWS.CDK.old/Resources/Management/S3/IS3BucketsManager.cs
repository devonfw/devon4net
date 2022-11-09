using Amazon.CDK.AWS.S3;
using Amazon.CDK;
using System.Collections.Generic;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.S3.Notifications;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Management.S3
{
    public interface IS3BucketsManager
    {
        IBucket AddS3Bucket(string bucketName, int expirationDays = 0, IList<ILifecycleRule> lifecycleRules = null, RemovalPolicy removalPolicy = RemovalPolicy.DESTROY, BucketEncryption encryption = BucketEncryption.KMS_MANAGED, string webSiteRedirectHost = "", bool versioned = true, bool enforceSSL = false, bool blockPublicAccess = true); //NOSONAR number of params
        IBucket LocateBucketByName(string identification, string bucketName);
        IBucket LocateBucketByArn(string identification, string arn);
        ILifecycleRule CreateLifecycleRule(string id, int expirationTime, string expirationTagName, string expirationTagValue, bool isVersionedBucket, int? previousVersionsExpirationDays = null);
        LambdaDestination CreateLambdaDestination(IFunction lambdaFunction);
        void AddEventNotificationToS3Bucket(Bucket bucket, EventType eventType, IBucketNotificationDestination destination);
        IBucket AddLifeCycleRuleToExistingBucket(string identification, string bucketName, List<ILifecycleRule> lifecycleRuleList);
        void AddLifeCycleRuleToExistingBucket(ref IBucket bucket, List<ILifecycleRule> lifecycleRuleList);
    }
}