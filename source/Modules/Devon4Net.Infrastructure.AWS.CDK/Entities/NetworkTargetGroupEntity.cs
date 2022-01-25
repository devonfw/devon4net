using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.ElasticLoadBalancingV2;
using Protocol = Amazon.CDK.AWS.ElasticLoadBalancingV2.Protocol;

namespace Devon4Net.Infrastructure.AWS.CDK.Entities
{
    public class NetworkTargetGroupEntity
    {
        public string Name { get; set; }
        public double Port { get; set; }
        public IVpc Vpc { get; set; }
        public int HealthCheckCount { get; set; }
    }
}
