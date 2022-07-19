using Amazon.CDK.AWS.IAM;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.PolicyDocuments
{
    public interface IAwsCdkPolicyDocumentHandler
    {
        PolicyDocument Create(PolicyStatement[] statements);
        PolicyStatement CreatePolicyStatement(string[] actions, string[] resources, Effect effect);
        IManagedPolicy CreateManagedPolicy(string id, string name, PolicyDocument document, string description = null, string path = null);
        IManagedPolicy LocateAwsManagedPolicyByName(string policyName);
        IManagedPolicy LocateManagedPolicyByName(string policyName);
    }
}