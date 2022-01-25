using Amazon.CDK;
using Amazon.CDK.AWS.CodeBuild;
using Amazon.CDK.AWS.CodePipeline;
using Amazon.CDK.AWS.CodePipeline.Actions;
using Amazon.CDK.AWS.ECR;
using Amazon.CDK.AWS.ECS;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.S3;
using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.orig
{
    public interface IAwsCdkPipelineHandler
    {
        CloudFormationCreateUpdateStackAction CreateCloudFormationCreateUpdateStackAction(string actionName, Artifact_ inputArtifact, string templatePath, string stackName, IRole deploymentRole, IRole role, CfnCapabilities[] cfnCapabilities);
        CodeBuildAction CreateCodeBuildAction(string actionName, Artifact_ inputArtifact, Artifact_ outputArtifact, IProject codeBuildProject, Dictionary<string, IBuildEnvironmentVariable> environmentVariables);
        EcrSourceAction CreateEcrAction(string actionName, string imageTag, Artifact_ outputArtifact, IRepository repositoryInstance, IRole role = null, string variableNamespace = null, double? runOrder = null);
        EcsDeployAction CreateEcsDeployAction(string actionName, IBaseService service, uint deploymentTimeout, Artifact_ inputArtifact, IRole role = null, string variableNamespace = null, double? runOrder = null);
        Pipeline CreatePipeline(string identification, string pipelineName, IBucket artifactBucket = null, IRole role = null, bool crossAccountKeys = false);
        S3SourceAction CreateS3Action(string actionName, IBucket sourceCodeBucket, string bucketKey, Artifact_ outputArtifact, IRole role = null, S3Trigger s3Trigger = S3Trigger.POLL, double? runOrder = null);
        IStageOptions CreateStage(string stageName);
    }
}