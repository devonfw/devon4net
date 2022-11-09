using Amazon.CDK;
using Amazon.CDK.AWS.ECR;
using Constructs;
using Devon4Net.Infrastructure.AWS.CDK.Entities;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.ECR
{
    public class AwsCdkEcrHandler : AwsCdkBaseHandler
    {
        private TagHandler TagHandler { get; }

        public AwsCdkEcrHandler(Construct scope, string applicationName, string environmentName, string region) : base(scope, applicationName, environmentName, region)
        {
            TagHandler = new TagHandler();
        }

        public IRepository Create(string repositoryName, RemovalPolicy removalPolicy = RemovalPolicy.DESTROY,
            TagMutability imageTagMutability = TagMutability.MUTABLE, bool imageScanOnPush = false, IList<ILifecycleRule> lifecycleRule = null,  string lifecycleRegistryId = null)
        {
            return CreateECRRepository(new EcrRepositoryEntity
            {
                RepositoryName = repositoryName,
                RemovalPolicy = removalPolicy,
                ImageTagMutability = imageTagMutability,
                ImageScanOnPush = imageScanOnPush,
                LifecycleRules = lifecycleRule,
                LifecycleRegistryId = lifecycleRegistryId
            });
        }

        public IRepository LocateFromRepositoryName(string identification, string repositoryName)
        {
            return Repository.FromRepositoryName(Scope, identification, repositoryName);
        }

        public IRepository LocateFromRepositoryArn(string identification, string arn)
        {
            return Repository.FromRepositoryArn(Scope, identification, arn);
        }

        private IRepository CreateECRRepository(EcrRepositoryEntity repo)
        {
            var result = new Repository(Scope, repo.RepositoryName, new RepositoryProps
            {
                RepositoryName = repo.RepositoryName,
                RemovalPolicy = repo.RemovalPolicy,
                ImageTagMutability = repo.ImageTagMutability,
                ImageScanOnPush = repo.ImageScanOnPush
            });

            var cfnOutput = new CfnOutput(Scope, $"{repo.RepositoryName}Url", new CfnOutputProps
            {
                Value = result.RepositoryUri
            });

            TagHandler.LogTag($"{ApplicationName}{EnvironmentName}{repo.RepositoryName}Url", cfnOutput);
            TagHandler.LogTag(ApplicationName + EnvironmentName + repo.RepositoryName, result);
            return result;
        }

        public ILifecycleRule CreateLifecycleRule(string description, int maxImageAgeDays, int maxImageNumber, int priorityOrder, List<string> tagPrefixList, string tagStatus)
        {
            GetLifecyleTagStatus(tagStatus, out var tagStatusEnum);

            return new LifecycleRule
            {
                Description = description,
                MaxImageAge = Duration.Days(maxImageAgeDays),
                MaxImageCount = maxImageNumber,
                RulePriority = priorityOrder,
                TagPrefixList = tagPrefixList.ToArray(),
                TagStatus = tagStatusEnum
            };
        }

        private static void GetLifecyleTagStatus(string tagStatus, out TagStatus tagStatusEnum)
        {
            if (string.IsNullOrWhiteSpace(tagStatus))
            {
                tagStatusEnum = TagStatus.ANY;
                return;
            }

            if (!Enum.TryParse(tagStatus, out tagStatusEnum))
            {
                throw new ArgumentException($"The tag status {tagStatus} of the ECR LifeCycleRule does not have a valid value");
            }
        }
    }
}