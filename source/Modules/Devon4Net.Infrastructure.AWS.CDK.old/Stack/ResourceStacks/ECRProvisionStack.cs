using Amazon.CDK.AWS.ECR;
using Amazon.CDK;
using System.Collections.Generic;
using System.Linq;
using Amazon.CDK.AWS.CodePipeline;
using Amazon.CDK.AWS.IAM;
using System;
using Devon4Net.Infrastructure.AWS.CDK.Options.Resources;

namespace Devon4Net.Infrastructure.AWS.CDK.Stack.ResourceStacks
{
    public partial class ProvisionStack
    {
        private void CreateEcrRepositories()
        {
            if (CdkOptions == null || CdkOptions.EcrRepositories?.Any() != true) return;

            StackResources.EcrRepositories = new Dictionary<string, IRepository>();

            foreach (var ecrRepository in CdkOptions.EcrRepositories)
            {
                if (ecrRepository.LocateInsteadOfCreate)
                {
                    StackResources.EcrRepositories.Add(ecrRepository.Id, AwsCdkHandler.LocateEcrRepositoryByName(ecrRepository.Id, ecrRepository.RepositoryName));
                }
                else
                {
                    var expiredImageLifeCycleRules = (ecrRepository.ExpireImageRules?.Any() == null || ecrRepository.ExpireImageRules?.Any() == false) ? null : ecrRepository.ExpireImageRules.ConvertAll(x => AwsCdkHandler.CreateEcrLifecycleRule(x.Description, x.MaxImageAgeDays, x.MaxImageNumber, x.PriorityOrder, x.TagPrefixList, x.TagStatus));
                    var imageTagMutability = ecrRepository.IsMutableImage ? TagMutability.MUTABLE : TagMutability.IMMUTABLE;

                    StackResources.EcrRepositories.Add(ecrRepository.Id, AwsCdkHandler.AddEcrRepository(ecrRepository.RepositoryName, RemovalPolicy.DESTROY, imageTagMutability, ecrRepository.ImageScanOnPush, expiredImageLifeCycleRules));
                }
            }
        }

        private void CreateEcrAction(IStage stage, IDictionary<string, Artifact_> artifacts, PipelineActionEcrOptions actionEcrOptions)
        {
            GetEcrActionResources(actionEcrOptions, artifacts, out var ecrRepository, out var artifact, out var role);

            AwsCdkHandler.CreateEcrActionInStage(stage, actionEcrOptions.Name, actionEcrOptions.ImageTag, artifact, ecrRepository, role);
        }

        private void GetEcrActionResources(PipelineActionEcrOptions actionEcrOptions, IDictionary<string, Artifact_> artifacts, out IRepository ecrRepository, out Artifact_ artifact, out IRole role)
        {
            ecrRepository = LocateEcrRepository(actionEcrOptions.EcrRepositoryName,
                $"The ECR Repository {actionEcrOptions.EcrRepositoryName} of the pipeline action {actionEcrOptions.Name} was not found",
                $"The pipeline action {actionEcrOptions.Name} must have a ECR Repository");

            // Create artifact
            if (string.IsNullOrWhiteSpace(actionEcrOptions.OutputArtifact))
            {
                throw new ArgumentException($"There is no output artifact in the pipeline action {actionEcrOptions.Name}");
            }
            else
            {
                if (artifacts.ContainsKey(actionEcrOptions.OutputArtifact))
                {
                    throw new ArgumentException($"The artifact {actionEcrOptions.OutputArtifact} of the pipeline action {actionEcrOptions.Name} already exists");
                }
                else
                {
                    artifact = new Artifact_(actionEcrOptions.OutputArtifact);
                    artifacts.Add(actionEcrOptions.OutputArtifact, artifact);
                }
            }

            // Locate role
            role = LocateRole(actionEcrOptions.Role, $"The role {actionEcrOptions.Role} of the pipeline action {actionEcrOptions.Name} was not found");
        }
        private IRepository LocateEcrRepository(string repositoryId, string exceptionMessageIfRepositoryDoesNotExist, string exceptionMessageIfRepositoryIsEmpty = null)
        {
            return StackResources.Locate<IRepository>(repositoryId, exceptionMessageIfRepositoryDoesNotExist, exceptionMessageIfRepositoryIsEmpty);
        }
    }
}
