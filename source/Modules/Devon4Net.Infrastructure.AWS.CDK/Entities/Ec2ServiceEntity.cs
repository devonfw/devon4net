using Amazon.CDK.AWS.ECS;

namespace Devon4Net.Infrastructure.AWS.CDK.Entities
{
    class Ec2ServiceEntity
    {
        public string Id { get; set; }
        public string ServiceName { get; set; }
        public ICluster Cluster { get; set; }
        public TaskDefinition TaskDefinition { get; set; }
        public List<CapacityProviderStrategy> CapacityProviderStrategies { get; set; }
        public int? HealthCheckGracePeriod { get; set; }
        public int? DesiredCount { get; set; }
        public bool UseDistinctInstances { get; set; }
        public List<string> PlacementStrategies { get; set; }
    }
}
