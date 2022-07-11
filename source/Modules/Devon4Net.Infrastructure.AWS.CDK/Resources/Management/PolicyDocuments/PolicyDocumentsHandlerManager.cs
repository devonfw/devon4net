using Amazon.CDK.AWS.IAM;
using Devon4Net.Infrastructure.AWS.CDK.Resources.Management.PolicyDocuments;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Management
{
    public partial class AwsCdkHandlerManager : IPolicyDocumentsHandlerManager
    {
        public PolicyDocument CreatePolicyDocument(PolicyStatement[] policyStatements)
        {
            return HandlerResources.AwsCdkPolicyDocumentHandler.Create(policyStatements);
        }

        public PolicyStatement CreatePolicyStatement(string[] actions, string[] resources, Effect effect)
        {
            return HandlerResources.AwsCdkPolicyDocumentHandler.CreatePolicyStatement(actions, resources, effect);
        }

        public IManagedPolicy CreateManagedPolicy(string id, string name, PolicyDocument document, string description = null, string path = null)
        {
            return HandlerResources.AwsCdkPolicyDocumentHandler.CreateManagedPolicy(id, name, document, description, path);
        }

        public IManagedPolicy LocateAwsManagedPolicyByName(string policyName)
        {
            return HandlerResources.AwsCdkPolicyDocumentHandler.LocateAwsManagedPolicyByName(policyName);
        }

        public IManagedPolicy LocateManagedPolicyByName(string policyName)
        {
            return HandlerResources.AwsCdkPolicyDocumentHandler.LocateManagedPolicyByName(policyName);
        }
    }
}
