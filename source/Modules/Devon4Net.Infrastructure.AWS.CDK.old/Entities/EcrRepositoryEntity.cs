using Amazon.CDK;
using Amazon.CDK.AWS.ECR;
using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AWS.CDK.Entities
{
    public class EcrRepositoryEntity
    {
        public string RepositoryName { get; set; }
        public RemovalPolicy RemovalPolicy { get; set; }
        public TagMutability ImageTagMutability { get; set; }
        public bool ImageScanOnPush { get; set; }
        public IList<ILifecycleRule> LifecycleRules { get; set; }
        public string LifecycleRegistryId { get; set; }
    }
}
