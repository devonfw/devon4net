namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{
    public class VpcOptions
    {
        public string Id { get; set; }
        public string AwsVpcId { get; set; }
        public bool LocateInsteadCreate { get; set; }
        public bool IsDefault { get; set; }
    }

}
