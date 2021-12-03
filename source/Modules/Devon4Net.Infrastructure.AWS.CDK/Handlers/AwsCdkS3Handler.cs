using Amazon.CDK;
using Amazon.CDK.AWS.S3;
using Devon4Net.Infrastructure.AWS.CDK.CdkEntity;

namespace Devon4Net.Infrastructure.AWS.CDK.Handlers
{
    public class AwsCdkS3Handler : AwsCdkDefaultHandler
    {
        public AwsCdkS3Handler(Construct scope, string applicationName, string environmentName) : base(scope, applicationName, environmentName)
        {
        }

        public IBucket Create(string bucketName, int expirationDays, IList<ILifecycleRule> lifecycleRules = null, RemovalPolicy removalPolicy = RemovalPolicy.DESTROY, BucketEncryption encryption = BucketEncryption.KMS_MANAGED, string webSiteRedirectHost = "", bool versioned = true)
        {
            return CreateBucket(new BucketEntity
            {
                RemovalPolicy = removalPolicy,
                BucketName = bucketName,
                ExpirationDays = expirationDays,
                Versioned = versioned,
                Encryption = encryption,
                WebSiteRedirectHost = webSiteRedirectHost,
                LifecycleRules = lifecycleRules
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

        private IBucket CreateBucket(BucketEntity bucket)
        {
            if (string.IsNullOrEmpty(bucket.WebSiteRedirectHost))
            {
                return new Bucket(Scope, bucket.BucketName, new BucketProps
                {
                    Versioned = bucket.Versioned,
                    RemovalPolicy = bucket.RemovalPolicy,
                    Encryption = bucket.Encryption,
                    LifecycleRules = GetLifeCycleRules(bucket.LifecycleRules, bucket.ExpirationDays)
                });
            }

            return new Bucket(Scope, bucket.BucketName, new BucketProps
            {
                Versioned = bucket.Versioned,
                RemovalPolicy = bucket.RemovalPolicy,
                Encryption = bucket.Encryption,
                LifecycleRules = GetLifeCycleRules(bucket.LifecycleRules, bucket.ExpirationDays),
                WebsiteRedirect = new RedirectTarget
                {
                    HostName = bucket.WebSiteRedirectHost
                }
            });
        }

        private ILifecycleRule[] GetLifeCycleRules(IList<ILifecycleRule> bucketLifecycleRules, int bucketExpirationDays)
        {
            var result = new List<ILifecycleRule>();

            if (bucketExpirationDays > 0)
            {
                result.Add(new LifecycleRule
                {
                    Expiration = Duration.Days(bucketExpirationDays)
                });
            }

            if (bucketLifecycleRules != null && bucketLifecycleRules.Any())
            {
                result.AddRange(bucketLifecycleRules);
            }

            return result.Any() ? result.ToArray() : null;
        }
    }
}
