namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{
    public class EcsServiceTargetGroupOptions
    {
        public string ContainerName { get; set; }
        public double Port { get; set; }
        public string NetworkTargetGroupId { get; set; }
        public string LoadBalancerType { get; set; }
    }
}
