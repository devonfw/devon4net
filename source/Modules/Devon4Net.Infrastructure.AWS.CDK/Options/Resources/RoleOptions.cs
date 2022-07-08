namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{
    public class RoleOptions
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string RoleArn { get; set; }
        public bool LocateInsteadOfCreate { get; set; }
        public List<string> AssumedBy { get; set; }
        public List<string> AwsPolicies { get; set; }
        public List<string> CustomPolicies { get; set; }
        public List<string> InlinePolicies { get; set; }
        public List<string> AwsActions { get; set; }
    }
}