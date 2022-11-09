using Amazon.CDK.AWS.EC2;

namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{
    public class Ec2InstanceOptions
    {
        public string Id { get; set; }
        public string InstanceName { get; set; }
        public string VpcId { get; set; }
        public string InstanceTypeId { get; set; }
        public string MachineImageRegion { get; set; }
        public string AmiId { get; set; }
        public string SecurityGroupId { get; set; }
    }
}
