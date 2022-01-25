using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.IAM;
using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AWS.CDK.Entities
{
    public class AutoScalingGroupEntity
    {
        public List<string> UserData { get; set; }
        public string Id { get; set; }
        public string AutoScalingGroupName { get; set; }
        public InstanceType InstanceType { get; set; }
        public IMachineImage MachineImage { get; set; }
        public IVpc Vpc { get; set; }
        public ISubnet[] Subnets { get; set; }
        public bool AllowAllOutbound { get; set; }
        public int MinCapacity { get; set; }
        public int MaxCapacity { get; set; }
        public int DesiredCapacity { get; set; }
        public ISecurityGroup SecurityGroup { get; set; }
        public string TimeOutCreation { get; set; }
        public string KeyPairName { get; set; }
        public IRole Role { get; set; }
        public Amazon.CDK.AWS.AutoScaling.BlockDevice[] BlockDevices { get; set; }
        public bool EnableProtectionFromScaleIn { get; set; }
    }
}
