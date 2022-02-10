using Amazon.CDK.AWS.IAM;

namespace Devon4Net.Infrastructure.AWS.CDK.Entities
{
    public class PolicyStatementEntity
    {
        public string[] Actions { get; set; }
        public string[] Resources { get; set; }
        public Effect Effect { get; set; }
    }
}
