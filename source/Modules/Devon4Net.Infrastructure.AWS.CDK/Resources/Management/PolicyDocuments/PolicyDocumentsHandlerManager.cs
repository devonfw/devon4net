using Amazon.CDK.AWS.IAM;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Management
{
    public partial class AwsCdkHandlerManager : ILambdaHandlerManager
    {
        public PolicyDocument CreatePolicyDocument(PolicyStatement[] policyStatements)
        {
            return HandlerResources.AwsCdkPolicyDocumentHandler.Create(policyStatements);
        }

        public PolicyStatement CreatePolicyStatement(string[] actions, string[] resources, Effect effect)
        {
            return HandlerResources.AwsCdkPolicyDocumentHandler.CreatePolicyStatement(actions, resources, effect);
        }
    }
}
