using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{
    public class S3BucketOptions
    {
        public string Id { get; set; }
        public bool LocateInsteadOfCreate { get; set; }
        public string BucketName { get; set; }
        public int? ExpirationDays { get; set; }
        public bool Versioned { get; set; }
        public List<Expiredocumentrule> ExpireDocumentRules { get; set; }
        public bool EnforceSSL { get; set; }
        public bool? BlockPublicAccess { get; set; }
        public S3EventOption[] Events { get; set; }
    }

    public class Expiredocumentrule
    {
        public string RuleName { get; set; }
        public int Expiration { get; set; }
        public int? PreviousVersionsExpirationDays { get; set; }
        public string TagName { get; set; }        
        public string TagValue { get; set; }
    }

    public class S3EventOption
    {
        public string EventType { get; set; }
        public string LambdaId { get; set; }
    }

}
