using Amazon.CDK.AWS.ECS;
using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{

    public class EcsServiceOptions
    {
        public string Id { get; set; }
        public bool LocateInsteadOfCreate { get; set; }
        public string ServiceName { get; set; }
        public string EcsClusterId { get; set; }
        public string EcsTaskDefinitionId { get; set; }
        public List<EcsServiceTargetGroupOptions> TargetGroups { get; set; }
        public int? HealthCheckGracePeriod { get; set; }
        public List<CapacityProviderStrategyItemOptions> CapacityProviderStrategy { get; set; }
        public int? DesiredCount { get; set; }
    }
}
