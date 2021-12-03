using Amazon.CDK;
using Amazon.CDK.AWS.S3;

namespace Devon4Net.Infrastructure.AWS.CDK.CdkEntity
{
    public class BucketEntity
    {
        public string BucketName { get; set; }
        public int ExpirationDays { get; set; }
        public RemovalPolicy RemovalPolicy { get; set; }
        public bool Versioned { get; set; }
        public string WebSiteRedirectHost { get; set; }
        public BucketEncryption Encryption { get; set; }
        public IList<ILifecycleRule> LifecycleRules { get; set; }
    }
}
