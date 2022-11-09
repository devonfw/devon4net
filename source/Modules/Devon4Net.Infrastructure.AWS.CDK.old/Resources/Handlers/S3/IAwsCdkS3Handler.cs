using Amazon.CDK;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.S3;
using Amazon.CDK.AWS.S3.Notifications;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.S3
{
    internal interface IAwsCdkS3Handler
    {
        public IBucket Create(string bucketName, int expirationDays, IList<ILifecycleRule> lifecycleRules = null, RemovalPolicy removalPolicy = RemovalPolicy.DESTROY, BucketEncryption encryption = BucketEncryption.KMS_MANAGED, string webSiteRedirectHost = "", bool versioned = true, bool enforceSSL = false, bool blockPublicAccess = true); //NOSONAR number of params
        public IBucket LocateFromName(string identification, string bucketName);
        public IBucket LocateFromArn(string identification, string arn);
        public ILifecycleRule CreateLifecycleRule(string id, int expirationTime, string expirationTagName, string expirationTagValue, bool isVersionedBucket, int? previousVersionsExpirationDays = null);
        public void AddEventNotification(Bucket bucket, EventType eventType, IBucketNotificationDestination destination);
        public LambdaDestination CreateLambdaDestination(IFunction lambdaFunction);
        IBucket AddLifeCycleRuleToExistingBucket(string identification, string bucketName, List<ILifecycleRule> lifecycleRulelist);
        void AddLifeCycleRuleToExistingBucket(ref IBucket bucket, List<ILifecycleRule> lifecycleRulelist);
    }
}