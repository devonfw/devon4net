using Amazon.CDK;
using Amazon.CDK.AWS.ECR;
using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.ECR
{
    public interface IAwsCdkEcrHandler
    {
        IRepository Create(string repositoryName, RemovalPolicy removalPolicy = RemovalPolicy.DESTROY, TagMutability imageTagMutability = TagMutability.MUTABLE, bool imageScanOnPush = false, IList<ILifecycleRule> lifecycleRule = null, string lifecycleRegistryId = null);
        ILifecycleRule CreateLifecycleRule(string description, int maxImageAgeDays, int maxImageNumber, int priorityOrder, List<string> tagPrefixList, string tagStatus);
        IRepository LocateFromRepositoryArn(string identification, string arn);
        IRepository LocateFromRepositoryName(string identification, string repositoryName);
    }
}