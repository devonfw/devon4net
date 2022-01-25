using Amazon.CDK.AWS.EC2;

namespace Devon4Net.Infrastructure.AWS.CDK.Entities
{
    class NetworkLoadBalancerEntity
    {
        public bool CrossZoneEnabled { get; set; }
        public bool DeletionProtection { get; set; }
        public bool InternetFacing { get; set; }
        public string LoadBalancerName { get; set; }
        public IVpc Vpc { get; set; }
        public ISubnetSelection Subnets { get; set; }
    }
}
