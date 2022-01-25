using Amazon.CDK.AWS.ECS;
using System.Collections.Generic;

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
    }
}

