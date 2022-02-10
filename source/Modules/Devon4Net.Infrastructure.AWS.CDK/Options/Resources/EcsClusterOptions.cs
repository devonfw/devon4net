using Amazon.CDK.AWS.ECS;
using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{
    public class EcsClusterOptions
    {
        public string Id { get; set; }
        public bool LocateInsteadOfCreate { get; set; }
        public string ClusterName { get; set; }
        public string VpcId { get; set; }

        // Deprecated behaviour
        public List<string> AutoScalingGroupIDs { get; set; }
    }
}
