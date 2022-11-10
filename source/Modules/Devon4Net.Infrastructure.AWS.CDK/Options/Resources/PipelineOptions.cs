using Devon4Net.Infrastructure.AWS.CDK.Enums;
using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{
    public class PipelineOptions
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ArtifactBucket { get; set; }
        public string Role { get; set; }
        public List<PipelineStageOptions> Stages { get; set; }
    }

    public class PipelineStageOptions
    {
        public string Name { get; set; }
        public List<PipelineActionOptions> Actions { get; set; }
    }

    public class PipelineActionOptions
    {
        public PipelineActionType Type { get; set; }
        public PipelineActionS3Options ArgumentsS3 { get; set; }
        public PipelineActionCodeBuildOptions ArgumentsCodeBuild { get; set; }
        public PipelineActionCloudFormationOptions ArgumentsCloudFormation { get; set; }
        public PipelineActionEcrOptions ArgumentsEcrRepository { get; set; }
        public PipelineActionEcsDeployOptions ArgumentsEcsDeploy { get; set; }
        public PipelineActionLambdaInvokeOptions ArgumentsLambdaInvoke { get; set; }
    }

    public class PipelineActionS3Options
    {
        public string Name { get; set; }
        public string BucketName { get; set; }
        public string BucketKey { get; set; }
        public string OutputArtifact { get; set; }
        public string Role { get; set; }
    }

    public class PipelineActionEcrOptions
    {
        public string Name { get; set; }
        public string ImageTag { get; set; }
        public string EcrRepositoryName { get; set; }
        public string OutputArtifact { get; set; }
        public string Role { get; set; }
    }

    public class PipelineActionEcsDeployOptions
    {
        public string Name { get; set; }
        public string ServiceName { get; set; }
        public string ClusterId { get; set; }
        public uint DeploymentTimeoutMinutes { get; set; }
        public string ImageName { get; set; }
        public string InputArtifact { get; set; }
        public string Role { get; set; }
    }

    public class PipelineActionCodeBuildOptions
    {
        public string Name { get; set; }
        public string InputArtifact { get; set; }
        public string OutputArtifact { get; set; }
        public string CodeBuildProject { get; set; }
        public Dictionary<string, string> EnvironmentVariables { get; set; }
    }

    public class PipelineActionCloudFormationOptions
    {
        public string Name { get; set; }
        public string InputArtifact { get; set; }
        public string TemplatePath { get; set; }
        public string StackName { get; set; }
        public string DeploymentRole { get; set; }
        public string Role { get; set; }
        public string[] CfnCapabilities { get; set; }
    }

    public class PipelineActionLambdaInvokeOptions
    {
        public string Name { get; set; }
        public string LambdaId { get; set; }
        public string[] InputArtifacts { get; set; }
        public string[] OutputArtifacts { get; set; }
        public string Role { get; set; }
        public Dictionary<string, object> UserParameters { get; set; }
        public string VariablesNamespace { get; set; }
    }
}
