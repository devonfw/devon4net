namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{
    public class AsgCapacityProviderOptions
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int TargetCapacityPercent { get; set; }
        public string AutoScalingGroupId { get; set; }
        public bool EnableScaleInTerminationProtection { get; set; }
        public string ClusterId { get; set; }
    }
}
