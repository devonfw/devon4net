using Amazon.CDK.AWS.IAM;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.PolicyDocuments
{
    public interface IAwsCdkPolicyDocumentHandler
    {
        Amazon.CDK.AWS.IAM.PolicyDocument Create(PolicyStatement[] statements);
        PolicyStatement CreatePolicyStatement(string[] actions, string[] resources, Effect effect);
    }
}