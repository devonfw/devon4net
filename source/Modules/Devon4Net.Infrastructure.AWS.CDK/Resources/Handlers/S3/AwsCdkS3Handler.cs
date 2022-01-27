using Amazon.CDK;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.S3;
using Amazon.CDK.AWS.S3.Notifications;
using Constructs;
using Devon4Net.Infrastructure.AWS.CDK.Entities;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.S3
{
    public class AwsCdkS3Handler : AwsCdkBaseHandler, IAwsCdkS3Handler
    {
        public AwsCdkS3Handler(Construct scope, string applicationName, string environmentName, string region) : base(scope, applicationName, environmentName, region)
        {
        }

        public IBucket Create(string bucketName, int expirationDays, IList<ILifecycleRule> lifecycleRules = null, RemovalPolicy removalPolicy = RemovalPolicy.DESTROY, BucketEncryption encryption = BucketEncryption.KMS_MANAGED, string webSiteRedirectHost = "", bool versioned = true, bool enforceSSL = false, bool blockPublicAccess = true)
        {
            return CreateBucket(new BucketEntity
            {
                RemovalPolicy = removalPolicy,
                BucketName = bucketName,
                ExpirationDays = expirationDays,
                Versioned = versioned,
                Encryption = encryption,
                WebSiteRedirectHost = webSiteRedirectHost,
                LifecycleRules = lifecycleRules,
                EnforceSSL = enforceSSL,
                BlockPublicAccess = blockPublicAccess
            });
        }

        public IBucket LocateFromName(string identification, string bucketName)
        {
            return Bucket.FromBucketName(Scope, identification, bucketName);
        }

        public IBucket LocateFromArn(string identification, string arn)
        {
            return Bucket.FromBucketArn(Scope, identification, arn);
        }

        public ILifecycleRule CreateLifecycleRule(string id, int expirationTime, string expirationTagName, string expirationTagValue, bool isVersionedBucket, int? previousVersionsExpirationDays = null)
        {
            Duration nonCurrentVersionExpirationDuration = null;

            if (isVersionedBucket)
            {
                if (previousVersionsExpirationDays.HasValue)
                {
                    nonCurrentVersionExpirationDuration = Duration.Days(previousVersionsExpirationDays.Value);
                } else
                {
                    throw new ArgumentException($"The LifeCycle rule {id} belongs to a versioned bucket and no previousVersionsExpirationDays has been established");
                }
            }

            return new LifecycleRule
            {
                Id = id,
                Expiration = Duration.Days(expirationTime),
                TagFilters = CreateTagFilters(expirationTagName, expirationTagValue),
                NoncurrentVersionExpiration = nonCurrentVersionExpirationDuration
            };
        }

        public void AddEventNotification(Bucket bucket, EventType eventType, IBucketNotificationDestination destination)
        {
            bucket.AddEventNotification(eventType, destination);
        }

        public LambdaDestination CreateLambdaDestination(IFunction lambdaFunction)
        {
            return new LambdaDestination(lambdaFunction);
        }

        private IBucket CreateBucket(BucketEntity bucket)
        {
            if (string.IsNullOrEmpty(bucket.WebSiteRedirectHost))
            {
                return new Bucket(Scope, bucket.BucketName, new BucketProps
                {
                    BucketName = bucket.BucketName,
                    Versioned = bucket.Versioned,
                    RemovalPolicy = bucket.RemovalPolicy,
                    Encryption = bucket.Encryption,
                    LifecycleRules = GetLifeCycleRules(bucket.LifecycleRules, bucket.ExpirationDays),
                    EnforceSSL = bucket.EnforceSSL,
                    BlockPublicAccess = bucket.BlockPublicAccess ? BlockPublicAccess.BLOCK_ALL : null
                });
            }

            return new Bucket(Scope, bucket.BucketName, new BucketProps
            {
                BucketName = bucket.BucketName,
                Versioned = bucket.Versioned,
                RemovalPolicy = bucket.RemovalPolicy,
                Encryption = bucket.Encryption,
                LifecycleRules = GetLifeCycleRules(bucket.LifecycleRules, bucket.ExpirationDays),
                WebsiteRedirect = new RedirectTarget
                {
                    HostName = bucket.WebSiteRedirectHost
                },
                EnforceSSL = bucket.EnforceSSL,
                BlockPublicAccess = bucket.BlockPublicAccess ? BlockPublicAccess.BLOCK_ALL : null
            });
        }

        private static ILifecycleRule[] GetLifeCycleRules(IList<ILifecycleRule> bucketLifecycleRules, int bucketExpirationDays)
        {
            var result = new List<ILifecycleRule>();

            if (bucketExpirationDays > 0)
            {
                result.Add(new LifecycleRule
                {
                    Expiration = Duration.Days(bucketExpirationDays)
                });
            }

            if (bucketLifecycleRules?.Count > 0)
            {
                result.AddRange(bucketLifecycleRules);
            }

            return result.Count > 0 ? result.ToArray() : null;
        }

        private static Dictionary<string, object> CreateTagFilters(string expirationTagName, string expirationTagValue)
        {
            return string.IsNullOrWhiteSpace(expirationTagName) ? null : new Dictionary<string, object>
            {
                { expirationTagName, expirationTagValue }
            };
        }
    }
}
