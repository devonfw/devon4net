namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{
    public class AutoScalingGroupOptions
    {
        public string Id { get; set; }
        public string AutoScalingGroupName { get; set; }
        public string InstanceTypeId { get; set; }
        public string MachineImageRegion { get; set; }
        public string MachineImage { get; set; }
        public string VpcId { get; set; }
        public string AmiId { get; set; }
        public bool AllowAllOutbound { get; set; }
        public int MinCapacity { get; set; }
        public int MaxCapacity { get; set; }
        public int DesiredCapacity { get; set; }
        public List<string> SecurityGroupIds { get; set; }
        public string Region { get; set; }
        public string CreationTimeOut { get; set; }
        public string RoleId { get; set; }
        public string KeyPairName { get; set; }
        public List<BlockDevicesOptions> BlockDevices { get; set; }
        public bool EnableProtectionFromScaleIn { get; set; }
        public List<string> SubnetsId { get; set; }
        public List<string> UserData { get; set; }
    }
}
