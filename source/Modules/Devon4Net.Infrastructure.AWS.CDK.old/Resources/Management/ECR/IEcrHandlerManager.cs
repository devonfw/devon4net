using Amazon.CDK.AWS.ECR;
using Amazon.CDK;
using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Management.ECR
{
    public interface IEcrHandlerManager
    {
        IRepository AddEcrRepository(string repositoryName, RemovalPolicy removalPolicy = RemovalPolicy.DESTROY, TagMutability imageTagMutability = TagMutability.MUTABLE, bool imageScanOnPush = false, IList<Amazon.CDK.AWS.ECR.ILifecycleRule> lifecycleRule = null, string lifecycleRegistryId = null);
        IRepository LocateEcrRepositoryByName(string identification, string repositoryName);
        IRepository LocateEcrRepositoryByArn(string identification, string arn);
        ILifecycleRule CreateEcrLifecycleRule(string description, int maxImageAgeDays, int maxImageNumber, int priorityOrder, List<string> tagPrefixList, string tagStatus);
    }
}