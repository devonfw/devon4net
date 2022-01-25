using Amazon.CDK.AWS.CodeBuild;
using Amazon.CDK.AWS.CodePipeline.Actions;
using Amazon.CDK.AWS.CodePipeline;
using Amazon.CDK.AWS.ECS;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.S3;
using Amazon.CDK;
using System.Collections.Generic;
using Amazon.CDK.AWS.ECR;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Management
{
    public partial class AwsCdkHandlerManager
    {

        public Pipeline CreatePipeline(string identification, string pipelineName, IBucket artifactBucket = null, IRole role = null, bool crossAccountKeys = false)
        {
            return HandlerResources.AwsCdkPipelineHandler.CreatePipeline(identification, pipelineName, artifactBucket, role, crossAccountKeys);
        }

        public IStage CreateStageInPipeline(Pipeline pipeline, string stageName)
        {
            var stage = HandlerResources.AwsCdkPipelineHandler.CreateStage(stageName);
            return pipeline.AddStage(stage);
        }

        public IStage CreateS3ActionInStage(IStage stage, string actionName, IBucket sourceCodeBucket, string bucketKey, Artifact_ outputArtifact, IRole role = null, S3Trigger s3Trigger = S3Trigger.POLL, double? runOrder = null)
        {
            var action = HandlerResources.AwsCdkPipelineHandler.CreateS3Action(actionName, sourceCodeBucket, bucketKey, outputArtifact, role, s3Trigger, runOrder);
            stage.AddAction(action);
            return stage;
        }

        public IStage CreateCloudFormationCreateUpdateStackActionInStage(IStage stage, string actionName, Artifact_ inputArtifact, string templatePath, string stackName, IRole deploymentRole, IRole role, CfnCapabilities[] cfnCapabilities)
        {
            var action = HandlerResources.AwsCdkPipelineHandler.CreateCloudFormationCreateUpdateStackAction(actionName, inputArtifact, templatePath, stackName, deploymentRole, role, cfnCapabilities);
            stage.AddAction(action);
            return stage;
        }

        public IStage CreateCodeBuildActionInStage(IStage stage, string actionName, Artifact_ inputArtifact, Artifact_ outputArtifact, IProject codeBuildProject, Dictionary<string, string> environmentVariables)
        {

            var environmentVariablesPlain = new Dictionary<string, IBuildEnvironmentVariable>();

            if (environmentVariables != null)
            {
                foreach (var keyValue in environmentVariables)
                {
                    environmentVariablesPlain.Add(keyValue.Key, new BuildEnvironmentVariable
                    {
                        Type = BuildEnvironmentVariableType.PLAINTEXT,
                        Value = keyValue.Value
                    });
                }

            }
            var action = HandlerResources.AwsCdkPipelineHandler.CreateCodeBuildAction(actionName, inputArtifact, outputArtifact, codeBuildProject, environmentVariablesPlain);
            stage.AddAction(action);
            return stage;
        }
        public IStage CreateEcsDeployActionInStage(IStage stage, string actionName, IBaseService service, uint deploymentTimeout, Artifact_ inputArtifact, IRole role = null, string variableNamespace = null, double? runOrder = null)
        {
            var action = HandlerResources.AwsCdkPipelineHandler.CreateEcsDeployAction(actionName, service, deploymentTimeout, inputArtifact, role, variableNamespace, runOrder);
            stage.AddAction(action);
            return stage;
        }

        public IStage CreateEcrActionInStage(IStage stage, string actionName, string imageTag, Artifact_ outputArtifact, IRepository repositoryInstance, IRole role = null, string variableNamespace = null, double? runOrder = null)
        {
            var action = HandlerResources.AwsCdkPipelineHandler.CreateEcrAction(actionName, imageTag, outputArtifact, repositoryInstance, role, variableNamespace, runOrder);
            stage.AddAction(action);
            return stage;
        }

    }
}
