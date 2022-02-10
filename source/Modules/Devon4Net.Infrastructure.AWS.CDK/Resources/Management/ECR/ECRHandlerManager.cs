using Amazon.CDK.AWS.ECR;
using Amazon.CDK;
using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Management
{
    public partial class AwsCdkHandlerManager: IEcrHandlerManager
    {
        public IRepository AddEcrRepository(string repositoryName, RemovalPolicy removalPolicy = RemovalPolicy.DESTROY, TagMutability imageTagMutability = TagMutability.MUTABLE, bool imageScanOnPush = false, IList<Amazon.CDK.AWS.ECR.ILifecycleRule> lifecycleRule = null, string lifecycleRegistryId = null)
        {
            return HandlerResources.AwsCdkECRHandler.Create(repositoryName, removalPolicy, imageTagMutability, imageScanOnPush, lifecycleRule, lifecycleRegistryId);
        }

        public IRepository LocateEcrRepositoryByName(string identification, string repositoryName)
        {
            return HandlerResources.AwsCdkECRHandler.LocateFromRepositoryName(identification, repositoryName);
        }

        public IRepository LocateEcrRepositoryByArn(string identification, string arn)
        {
            return HandlerResources.AwsCdkECRHandler.LocateFromRepositoryArn(identification, arn);
        }
        public ILifecycleRule CreateEcrLifecycleRule(string description, int maxImageAgeDays, int maxImageNumber, int priorityOrder, List<string> tagPrefixList, string tagStatus)
        {
            return HandlerResources.AwsCdkECRHandler.CreateLifecycleRule(description, maxImageAgeDays, maxImageNumber, priorityOrder, tagPrefixList, tagStatus);
        }
    }
}
