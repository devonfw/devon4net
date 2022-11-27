using Amazon.CDK;
using Amazon.CDK.AWS.CodeBuild;
using Amazon.CDK.AWS.CodePipeline;
using Amazon.CDK.AWS.CodePipeline.Actions;
using Amazon.CDK.AWS.ECR;
using Amazon.CDK.AWS.ECS;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.S3;
using Constructs;
using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.orig
{
    public class AwsCdkPipelineHandler : AwsCdkBaseHandler, IAwsCdkPipelineHandler
    {
        public AwsCdkPipelineHandler(Construct scope, string applicationName, string environmentName, string region) : base(scope, applicationName, environmentName, region)
        {
        }

        public Pipeline CreatePipeline(string identification, string pipelineName, IBucket artifactBucket = null, IRole role = null, bool crossAccountKeys = false)
        {
            return new Pipeline(Scope, identification, new PipelineProps
            {
                ArtifactBucket = artifactBucket,
                CrossAccountKeys = crossAccountKeys,
                PipelineName = pipelineName,
                Role = role
            });
        }

        public IStageOptions CreateStage(string stageName)
        {
            return new StageOptions
            {
                StageName = stageName
            };
        }

        public S3SourceAction CreateS3Action(string actionName, IBucket sourceCodeBucket, string bucketKey, Artifact_ outputArtifact, IRole role = null, S3Trigger s3Trigger = S3Trigger.POLL, double? runOrder = null)
        {
            return new S3SourceAction(new S3SourceActionProps
            {
                ActionName = actionName,
                Bucket = sourceCodeBucket,
                BucketKey = bucketKey,
                Output = outputArtifact,
                Trigger = s3Trigger,
                Role = role,
                RunOrder = runOrder
            });
        }

        public EcrSourceAction CreateEcrAction(string actionName, string imageTag, Artifact_ outputArtifact, IRepository repositoryInstance, IRole role = null, string variableNamespace = null, double? runOrder = null)
        {
            return new EcrSourceAction(new EcrSourceActionProps
            {
                ActionName = actionName,
                ImageTag = imageTag,
                Output = outputArtifact,
                Repository = repositoryInstance,
                Role = role,
                RunOrder = runOrder,
                VariablesNamespace = variableNamespace
            });
        }

        public EcsDeployAction CreateEcsDeployAction(string actionName, IBaseService service, uint deploymentTimeout, Artifact_ inputArtifact, IRole role = null, string variableNamespace = null, double? runOrder = null)
        {
            return new EcsDeployAction(new EcsDeployActionProps
            {
                ActionName = actionName,
                Service = service,
                DeploymentTimeout = Duration.Minutes(deploymentTimeout),
                Input = inputArtifact,
                Role = role,
                RunOrder = runOrder,
                VariablesNamespace = variableNamespace
            });
        }


        public CloudFormationCreateUpdateStackAction CreateCloudFormationCreateUpdateStackAction(string actionName, Artifact_ inputArtifact, string templatePath, string stackName, IRole deploymentRole, IRole role, CfnCapabilities[] cfnCapabilities)
        {
            return new CloudFormationCreateUpdateStackAction(new CloudFormationCreateUpdateStackActionProps
            {
                ActionName = actionName,
                TemplatePath = inputArtifact.AtPath(templatePath),
                StackName = stackName,
                Role = role,
                DeploymentRole = deploymentRole,
                CfnCapabilities = cfnCapabilities
            });
        }

        public CodeBuildAction CreateCodeBuildAction(string actionName, Artifact_ inputArtifact, Artifact_ outputArtifact, IProject codeBuildProject, Dictionary<string, IBuildEnvironmentVariable> environmentVariables)
        {
            return new CodeBuildAction(new CodeBuildActionProps
            {
                ActionName = actionName,
                Input = inputArtifact,
                Outputs = outputArtifact != null ? new[] { outputArtifact } : null,
                Project = codeBuildProject,
                EnvironmentVariables = environmentVariables
            });
        }

        public LambdaInvokeAction CreateLambdaInvokeAction(IFunction lambda, string actionName, IRole role, Artifact_[] inputArtifacts = null, Artifact_[] outputArtifacts = null, IDictionary<string, object> userParameters = null, string variablesNamespace = null)
        {
            return new LambdaInvokeAction(new LambdaInvokeActionProps
            {
                Lambda = lambda,
                ActionName = actionName,
                Inputs = inputArtifacts,
                Outputs = outputArtifacts,
                Role = role,
                UserParameters = userParameters,
                VariablesNamespace = variablesNamespace
            });
        }
    }
}
