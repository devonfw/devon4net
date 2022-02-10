using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.ElasticLoadBalancingV2;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.ELB
{
    public interface IAwsCdkElbHandler
    {
        INetworkTargetGroup CreateNetworkTargetGroup(string id, string name, double port, IVpc vpc, int healthCheckCount);
    }
}