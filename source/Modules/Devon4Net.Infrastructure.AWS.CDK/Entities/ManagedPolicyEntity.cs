using Amazon.CDK.AWS.IAM;

namespace Devon4Net.Infrastructure.AWS.CDK.Entities
{
    class ManagedPolicyEntity
    {
        public string Id { get; set; }
        public string ManagedPolicyName { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public PolicyDocument Document { get; set; }
    }
}
