using Amazon.CDK.AWS.CodeBuild;
using Amazon.CDK.AWS.IAM;
using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Management
{
    public interface ICodeBuildHandlerManager
    {
        IProject CreateCodeBuildProject(string identification, string projectName, IRole role, Dictionary<string, object> buildSpec, IBuildImage buildImage = null, ComputeType? computeType = null, bool enableIndependentTrigger = false);
    }
}