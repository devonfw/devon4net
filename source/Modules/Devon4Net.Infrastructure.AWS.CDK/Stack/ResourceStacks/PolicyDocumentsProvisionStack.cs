using Amazon.CDK.AWS.IAM;
using System.Collections.Generic;
using System.Linq;

namespace Devon4Net.Infrastructure.AWS.CDK.Stack
{
    public partial class ProvisionStack
    {
        private void CreatePolicyDocuments()
        {
            if (CdkOptions == null || CdkOptions.PolicyDocuments?.Any() != true) return;

            StackResources.PolicyDocuments = new Dictionary<string, PolicyDocument>();
            foreach (var inlinePolicy in CdkOptions.PolicyDocuments)
            {
                var statements = inlinePolicy.PolicyStatements.Select(x => AwsCdkHandler.CreatePolicyStatement(x.Action.ToArray(), x.Resource.ToArray(), x.Effect)).ToArray();
                StackResources.PolicyDocuments.Add(inlinePolicy.Id, AwsCdkHandler.CreatePolicyDocument(statements));
            }
        }

        private PolicyDocument LocatePolicyDocument(string policyDocumentId, string exceptionMessageIfPolicyDocumentDoesNotExist, string exceptionMessageIfPolicyDocumentIsEmpty = null)
        {
            return StackResources.Locate<PolicyDocument>(policyDocumentId, exceptionMessageIfPolicyDocumentDoesNotExist, exceptionMessageIfPolicyDocumentIsEmpty);
        }
    }
}
