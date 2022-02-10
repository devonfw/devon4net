using Amazon.CDK.AWS.CodeBuild;
using Amazon.CDK.AWS.IAM;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers
{
    public interface IAwsCdkCodeBuildHandler
    {
        PipelineProject CreateCodeBuildPipelineProject(string identification, string projectName, IRole role, BuildSpec buildSpec, IBuildEnvironment buildEnvironment = null);
        Project CreateCodeBuildProject(string identification, string projectName, IRole role, BuildSpec buildSpec, IBuildEnvironment buildEnvironment = null);
    }
}