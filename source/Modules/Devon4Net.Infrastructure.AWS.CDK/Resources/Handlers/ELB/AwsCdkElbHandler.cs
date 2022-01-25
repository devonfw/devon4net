using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.ElasticLoadBalancingV2;
using Constructs;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.ELB
{
    public class AwsCdkElbHandler : AwsCdkBaseHandler, IAwsCdkElbHandler
    {
        public AwsCdkElbHandler(Construct scope, string applicationName, string environmentName) : base(scope, applicationName, environmentName) { }

        public INetworkTargetGroup CreateNetworkTargetGroup(string id, string name, double port, IVpc vpc, int healthCheckCount)
        {
            var targetGroup = new NetworkTargetGroup(Scope, id, new NetworkTargetGroupProps
            {
                Port = port,
                TargetType = TargetType.INSTANCE,
                TargetGroupName = name,
                Vpc = vpc,
                HealthCheck = new HealthCheck
                {
                    HealthyThresholdCount = healthCheckCount,
                    UnhealthyThresholdCount = healthCheckCount,
                }
            });

            return targetGroup;
        }
    }
}
