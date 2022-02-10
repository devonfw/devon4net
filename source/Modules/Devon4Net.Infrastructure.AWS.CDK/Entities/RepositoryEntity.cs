using Amazon.CDK;
using Amazon.CDK.AWS.ECR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Devon4Net.Infrastructure.AWS.CDK.Entities
{
    class RepositoryEntity
    {
        public string RepositoryName { get; set; }
        public RemovalPolicy RemovalPolicy { get; set; }
        public TagMutability ImageTagMutability { get; set; }
        public bool ImageScanOnPush { get; set; }
        public IList<ILifecycleRule> LifecycleRules { get; set; }
        public string LifecycleRegistryId { get; set; }
    }
}
