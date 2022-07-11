using Amazon.CDK.AWS.IAM;

namespace Devon4Net.Infrastructure.AWS.CDK.Stack
{
    public partial class ProvisionStack
    {
        private void CreatePolicyDocuments()
        {
            if (CdkOptions == null || CdkOptions.PolicyDocuments?.Any() != true) return;

            foreach (var inlinePolicy in CdkOptions.PolicyDocuments)
            {
                var statements = inlinePolicy.PolicyStatements.Select(x => AwsCdkHandler.CreatePolicyStatement(x.Action.ToArray(), x.Resource.ToArray(), x.Effect)).ToArray();
                StackResources.PolicyDocuments.Add(inlinePolicy.Id, AwsCdkHandler.CreatePolicyDocument(statements));
            }
        }

        private void CreateManagedPolicies()
        {
            if (CdkOptions == null || CdkOptions.ManagedPolicies?.Any() != true) return;

            foreach (var policyOptions in CdkOptions.ManagedPolicies)
            {
                IManagedPolicy managedPolicy;
                if (policyOptions.LocateInsteadOfCreate.HasValue && policyOptions.LocateInsteadOfCreate.Value)
                {
                    managedPolicy = AwsCdkHandler.LocateAwsManagedPolicyByName(policyOptions.Name);
                }
                else
                {
                    var document = LocatePolicyDocument(policyOptions.PolicyDocumentId, "Document could not be found");
                    managedPolicy = AwsCdkHandler.CreateManagedPolicy(policyOptions.Id, policyOptions.Name, document);
                }

                StackResources.ManagedPolicies.Add(policyOptions.Id, managedPolicy);
            }
        }

        private PolicyDocument LocatePolicyDocument(string policyDocumentId, string exceptionMessageIfPolicyDocumentDoesNotExist, string exceptionMessageIfPolicyDocumentIsEmpty = null)
        {
            return StackResources.Locate<PolicyDocument>(policyDocumentId, exceptionMessageIfPolicyDocumentDoesNotExist, exceptionMessageIfPolicyDocumentIsEmpty);
        }

        private IManagedPolicy LocateManagedPolicy(string managedPolicyId, string exceptionMessageIfManagedPolicyDoesNotExist, string exceptionMessageIfManagedPolicyIsEmpty = null)
        {
            return StackResources.Locate<IManagedPolicy>(managedPolicyId, exceptionMessageIfManagedPolicyDoesNotExist, exceptionMessageIfManagedPolicyIsEmpty);
        }
    }
}
