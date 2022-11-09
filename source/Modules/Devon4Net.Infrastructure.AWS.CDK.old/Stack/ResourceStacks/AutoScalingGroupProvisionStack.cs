using Amazon.CDK.AWS.AutoScaling;
using Amazon.CDK.AWS.ECS;
using System.Linq;

namespace Devon4Net.Infrastructure.AWS.CDK.Stack.ResourceStacks
{
    public partial class ProvisionStack
    {
        private void CreateAsgCapacityProviders()
        {
            if (CdkOptions == null || CdkOptions.CapacityProviders?.Any() != true) return;

            foreach (var capacityProvider in CdkOptions.CapacityProviders)
            {
                ValidateCapacityProvider(capacityProvider);
            }

            Cluster cluster = default;
            foreach (var providerOptions in CdkOptions.CapacityProviders)
            {
                var autoScalingGroup = LocateAutoScalingGroup(providerOptions.AutoScalingGroupId, "ASG assigned to capacity provider does not exist");
                if (cluster == null || cluster.Node.Id != providerOptions.Id)
                {
                    cluster = LocateEcsCluster(providerOptions.ClusterId, "Cluster assigned to capacity provider does not exist") as Cluster;
                }

                var provider = AwsCdkHandler.CreateAsgCapacityProvider(providerOptions.Id, providerOptions.Name, providerOptions.TargetCapacityPercent, providerOptions.EnableScaleInTerminationProtection, autoScalingGroup);
                AwsCdkHandler.AddAsgCapacityProviderToCluster(provider, cluster);

                StackResources.AsgCapacityProviders.Add(providerOptions.Id, provider);
            }
        }

        private IAutoScalingGroup LocateAutoScalingGroup(string autoscalingGroupId, string exceptionMessageIfAsgDoesNotExist, string exceptionMessageIfAsgIsEmpty = null)
        {
            return StackResources.Locate<IAutoScalingGroup>(autoscalingGroupId, exceptionMessageIfAsgDoesNotExist, exceptionMessageIfAsgIsEmpty);
        }

        private AsgCapacityProvider LocateAsgCapacityProvider(string id, string exceptionMessageIfAsgDoesNotExist, string exceptionMessageIfAsgIsEmpty = null)
        {
            return StackResources.Locate<AsgCapacityProvider>(id, exceptionMessageIfAsgDoesNotExist, exceptionMessageIfAsgIsEmpty);
        }
    }
}
