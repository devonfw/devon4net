using Amazon.CDK.AWS.CodeBuild;
using Amazon.CDK.AWS.IAM;
using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Management.CodeBuild
{
    public partial class AwsCdkHandlerManager : ICodeBuildHandlerManager
    {
        public IProject CreateCodeBuildProject(string identification, string projectName, IRole role, Dictionary<string, object> buildSpec, IBuildImage buildImage = null, ComputeType? computeType = null, bool enableIndependentTrigger = false)
        {
            if (buildImage == null)
            {
                buildImage = LinuxBuildImage.AMAZON_LINUX_2_3;
            }

            if (computeType == null)
            {
                computeType = ComputeType.SMALL;
            }

            var buildEnvironment = new BuildEnvironment
            {
                BuildImage = buildImage,
                ComputeType = computeType
            };

            var buildSpecObject = BuildSpec.FromObject(buildSpec);

            if (enableIndependentTrigger)
            {
                return HandlerResources.AwsCdkCodeBuildHandler.CreateCodeBuildProject(identification, projectName, role, buildSpecObject, buildEnvironment);
            }
            else
            {
                return HandlerResources.AwsCdkCodeBuildHandler.CreateCodeBuildPipelineProject(identification, projectName, role, buildSpecObject, buildEnvironment);
            }
        }
    }
}
