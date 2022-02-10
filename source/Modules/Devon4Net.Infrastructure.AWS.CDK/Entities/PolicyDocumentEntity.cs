using Amazon.CDK.AWS.IAM;

namespace Devon4Net.Infrastructure.AWS.CDK.Entities
{
    public class PolicyDocumentEntity
    {
        public PolicyStatement[] Statements { get; set; }
    }
}
