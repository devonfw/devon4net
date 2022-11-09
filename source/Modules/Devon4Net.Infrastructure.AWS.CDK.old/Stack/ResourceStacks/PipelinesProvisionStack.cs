using Amazon.CDK;
using Amazon.CDK.AWS.CodeBuild;
using Amazon.CDK.AWS.CodePipeline;
using Amazon.CDK.AWS.ECS;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.S3;
using Devon4Net.Infrastructure.AWS.CDK.Enums;
using Devon4Net.Infrastructure.AWS.CDK.Options.Global;
using Devon4Net.Infrastructure.AWS.CDK.Options.Resources;
using Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.S3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Devon4Net.Infrastructure.AWS.CDK.Stack.ResourceStacks
{
    public partial class ProvisionStack
    {
        private void CreatePipelines()
        {
            if (CdkOptions == null || CdkOptions.Pipelines?.Any() != true) return;

            foreach (var pipelineOption in CdkOptions.Pipelines)
            {
                GetPipelineResources(pipelineOption, out var bucket, out var role);
                var pipeline = AwsCdkS3Handler.CreatePipeline(pipelineOption.Name, pipelineOption.Name, bucket, role);

                StackResources.Artifacts = new Dictionary<string, Artifact_>();

                foreach (var stageOption in pipelineOption.Stages)
                {
                    var stage = AwsCdkHandler.CreateStageInPipeline(pipeline, stageOption.Name);

                    foreach (var actionOption in stageOption.Actions)
                    {
                        switch (actionOption.Type)
                        {
                            case PipelineActionType.s3Source:
                                var s3Arguments = actionOption.ArgumentsS3;
                                CreateS3Action(stage, StackResources.Artifacts, s3Arguments);
                                break;
                            case PipelineActionType.codebuild:
                                var codeBuildArguments = actionOption.ArgumentsCodeBuild;
                                CreateCodeBuildAction(stage, StackResources.Artifacts, codeBuildArguments);
                                break;
                            case PipelineActionType.cloudformation:
                                var cloudFormationArguments = actionOption.ArgumentsCloudFormation;
                                CreateCloudFormationAction(stage, StackResources.Artifacts, cloudFormationArguments);
                                break;
                            case PipelineActionType.ecrsource:
                                var ecrRepositoryArguments = actionOption.ArgumentsEcrRepository;
                                CreateEcrAction(stage, StackResources.Artifacts, ecrRepositoryArguments);
                                break;
                            case PipelineActionType.ecsdeploy:
                                var ecsDeployArguments = actionOption.ArgumentsEcsDeploy;
                                CreateEcsDeployAction(stage, StackResources.Artifacts, ecsDeployArguments);
                                break;
                        }
                    }
                }

                StackResources.Pipelines.Add(pipelineOption.Id, pipeline);
            }
        }
        private void GetPipelineResources(PipelineOptions pipelineOption, out IBucket bucket, out IRole role)
        {
            // Locate bucket
            bucket = LocateBucket(pipelineOption.ArtifactBucket, $"The bucket {pipelineOption.ArtifactBucket} of the pipeline {pipelineOption.Name} was not found");

            // Locate role
            role = LocateRole(pipelineOption.Role, $"The role {pipelineOption.Role} of the pipeline {pipelineOption.Name} was not found");
        }

        private void CreateS3Action(IStage stage, IDictionary<string, Artifact_> artifacts, PipelineActionS3Options actionS3Options)
        {
            GetS3ActionResources(actionS3Options, artifacts, out var bucket, out var artifact, out var role);

            AwsCdkHandler.CreateS3ActionInStage(stage, actionS3Options.Name, bucket, actionS3Options.BucketKey, artifact, role);
        }

        private void GetS3ActionResources(PipelineActionS3Options actionS3Options, IDictionary<string, Artifact_> artifacts, out IBucket bucket, out Artifact_ artifact, out IRole role)
        {
            // Locate bucket
            bucket = LocateBucket(actionS3Options.BucketName,
                $"The bucket {actionS3Options.BucketName} of the pipeline action {actionS3Options.Name} was not found",
                $"The pipeline action {actionS3Options.Name} must have a bucket");

            // Create artifact
            if (string.IsNullOrWhiteSpace(actionS3Options.OutputArtifact))
            {
                throw new ArgumentException($"There is no output artifact in the pipeline action {actionS3Options.Name}");
            }
            else
            {
                if (artifacts.ContainsKey(actionS3Options.OutputArtifact))
                {
                    throw new ArgumentException($"The artifact {actionS3Options.OutputArtifact} of the pipeline action {actionS3Options.Name} already exists");
                }
                else
                {
                    artifact = new Artifact_();
                    artifacts.Add(actionS3Options.OutputArtifact, artifact);
                }
            }

            // Locate role
            role = LocateRole(actionS3Options.Role, $"The role {actionS3Options.Role} of the pipeline action {actionS3Options.Name} was not found");
        }

        private void CreateCodeBuildAction(IStage stage, IDictionary<string, Artifact_> artifacts, PipelineActionCodeBuildOptions actionCodeBuildOptions)
        {
            GetCodeBuildActionResources(actionCodeBuildOptions, artifacts, out var inputArtifact, out var outputArtifact, out var project);

            AwsCdkHandler.CreateCodeBuildActionInStage(stage, actionCodeBuildOptions.Name, inputArtifact, outputArtifact, project, actionCodeBuildOptions.EnvironmentVariables);
        }
        private void CreateCloudFormationAction(IStage stage, IDictionary<string, Artifact_> artifacts, PipelineActionCloudFormationOptions actionCloudFormationOptions)
        {
            GetCloudFormationActionResources(actionCloudFormationOptions, artifacts, out var artifact, out var deploymentRole, out var role, out var cfnCapabilites);

            AwsCdkHandler.CreateCloudFormationCreateUpdateStackActionInStage(stage, actionCloudFormationOptions.Name, artifact, actionCloudFormationOptions.TemplatePath, actionCloudFormationOptions.StackName, deploymentRole, role, cfnCapabilites);
        }

        private void GetCloudFormationActionResources(PipelineActionCloudFormationOptions actionCloudFormationOptions, IDictionary<string, Artifact_> artifacts, out Artifact_ artifact, out IRole deploymentRole, out IRole role, out CfnCapabilities[] cfnCapabilities)
        {
            // Locate artifact
            if (string.IsNullOrWhiteSpace(actionCloudFormationOptions.InputArtifact))
            {
                throw new ArgumentException($"There is no input artifact in the pipeline action {actionCloudFormationOptions.Name}");
            }
            else
            {
                if (!artifacts.TryGetValue(actionCloudFormationOptions.InputArtifact, out artifact))
                {
                    throw new ArgumentException($"The artifact {actionCloudFormationOptions.InputArtifact} of the pipeline action {actionCloudFormationOptions.Name} was not found");
                }
            }

            // Locate deployment role
            deploymentRole = LocateRole(actionCloudFormationOptions.DeploymentRole,
                $"The role {actionCloudFormationOptions.DeploymentRole} of the pipeline action {actionCloudFormationOptions.Name} was not found",
                $"There is no deployment role in the pipeline action {actionCloudFormationOptions.Name}");

            // Locate role
            role = LocateRole(actionCloudFormationOptions.Role,
                $"There is no role in the pipeline action {actionCloudFormationOptions.Name}",
                $"The role {actionCloudFormationOptions.Role} of the pipeline action {actionCloudFormationOptions.Name} was not found");

            // Parse CfnCapabilites
            cfnCapabilities = actionCloudFormationOptions.CfnCapabilities.Select(x => Enum.Parse<CfnCapabilities>(x)).ToArray();
        }

        private void CreateEcsDeployAction(IStage stage, IDictionary<string, Artifact_> artifacts, PipelineActionEcsDeployOptions actionEcsDeployOptions)
        {
            GetEcsDeployActionResources(actionEcsDeployOptions, artifacts, out var service, out var artifact, out var role);
            AwsCdkHandler.CreateEcsDeployActionInStage(stage, actionEcsDeployOptions.Name, service, actionEcsDeployOptions.DeploymentTimeoutMinutes, artifact, role);
        }

        private void GetEcsDeployActionResources(PipelineActionEcsDeployOptions actionEcsDeployOptions, IDictionary<string, Artifact_> artifacts, out IBaseService service, out Artifact_ artifact, out IRole role)
        {
            service = AwsCdkHandler.LocateEcsServiceByAttrs(actionEcsDeployOptions.Name, new Ec2ServiceAttributes
            {
                Cluster = LocateEcsCluster(actionEcsDeployOptions.ClusterId,
                    $"The cluster with {actionEcsDeployOptions.ClusterId} name does not exists",
                    $"The EcsService with {actionEcsDeployOptions.ClusterId} must hace a cluster"),
                ServiceName = actionEcsDeployOptions.ServiceName
            });

            // Locate artifact
            if (string.IsNullOrWhiteSpace(actionEcsDeployOptions.InputArtifact))
            {
                throw new ArgumentException($"There is no input artifact in the pipeline action {actionEcsDeployOptions.Name}");
            }
            else
            {
                if (!artifacts.TryGetValue(actionEcsDeployOptions.InputArtifact, out artifact))
                {
                    throw new ArgumentException($"The artifact {actionEcsDeployOptions.InputArtifact} of the pipeline action {actionEcsDeployOptions.Name} was not found");
                }
            }

            // Locate role
            role = LocateRole(actionEcsDeployOptions.Role, $"The role {actionEcsDeployOptions.Role} of the pipeline action {actionEcsDeployOptions.Name} was not found");
        }
    }
}
