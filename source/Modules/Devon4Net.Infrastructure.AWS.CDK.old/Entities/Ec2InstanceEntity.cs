using Amazon.CDK.AWS.EC2;

namespace Devon4Net.Infrastructure.AWS.CDK.Entities
{
    public class Ec2InstanceEntity
    {
        public string InstanceName { get; set; }
        public InstanceType InstanceType { get; set; }
        public IMachineImage MachineImage { get; set; }
        public IVpc Vpc { get; set; }
        public ISecurityGroup SecurityGroup { get; set; }
    }
}
