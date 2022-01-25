using Amazon.CDK.AWS.CodeBuild;
using Amazon.CDK.AWS.IAM;
using Constructs;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers
{
    public class AwsCdkCodeBuildHandler : AwsCdkBaseHandler, IAwsCdkCodeBuildHandler
    {
        public AwsCdkCodeBuildHandler(Construct scope, string applicationName, string environmentName) : base(scope, applicationName, environmentName)
        {
        }

        public PipelineProject CreateCodeBuildPipelineProject(string identification, string projectName, IRole role, BuildSpec buildSpec, IBuildEnvironment buildEnvironment = null)
        {
            return new PipelineProject(Scope, identification, new PipelineProjectProps
            {
                ProjectName = projectName,
                Role = role,
                Environment = buildEnvironment,
                BuildSpec = buildSpec
            });
        }

        public Project CreateCodeBuildProject(string identification, string projectName, IRole role, BuildSpec buildSpec, IBuildEnvironment buildEnvironment = null)
        {
            return new Project(Scope, identification, new ProjectProps
            {
                ProjectName = projectName,
                Role = role,
                Environment = buildEnvironment,
                BuildSpec = buildSpec
            });
        }
    }
}
