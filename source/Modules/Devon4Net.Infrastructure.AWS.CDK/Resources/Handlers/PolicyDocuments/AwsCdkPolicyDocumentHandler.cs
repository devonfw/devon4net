
using Amazon.CDK.AWS.IAM;
using Constructs;
using Devon4Net.Infrastructure.AWS.CDK.Entities;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.PolicyDocuments
{
    public class AwsCdkPolicyDocumentHandler : AwsCdkBaseHandler, IAwsCdkPolicyDocumentHandler
    {
        public AwsCdkPolicyDocumentHandler(Construct scope, string applicationName, string environmentName, string region) : base(scope, applicationName, environmentName, region)
        {
        }

        public PolicyDocument Create(PolicyStatement[] statements)
        {
            return CreatePolicyDocument(new PolicyDocumentEntity
            {
                Statements = statements
            });
        }

        public PolicyStatement CreatePolicyStatement(string[] actions, string[] resources, Effect effect)
        {
            return CreatePolicyStatement(new PolicyStatementEntity
            {
                Actions = actions,
                Resources = resources,
                Effect = effect
            });
        }

        private static PolicyStatement CreatePolicyStatement(PolicyStatementEntity statement)
        {
            return new PolicyStatement
            (
                new PolicyStatementProps()
                {
                    Effect = statement.Effect,
                    Actions = statement.Actions,
                    Resources = statement.Resources
                }
            );
        }

        private static PolicyDocument CreatePolicyDocument(PolicyDocumentEntity entity)
        {
            var policyDocument = new PolicyDocument();
            policyDocument.AddStatements(entity.Statements);
            return policyDocument;
        }
    }
}
