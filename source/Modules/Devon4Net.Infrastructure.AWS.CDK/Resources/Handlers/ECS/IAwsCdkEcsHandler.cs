using Amazon.CDK.AWS.AutoScaling;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.ECS;
using Amazon.CDK.AWS.ElasticLoadBalancingV2;
using Amazon.CDK.AWS.IAM;
using Devon4Net.Infrastructure.AWS.CDK.Options.Resources;
using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.ECS
{
    public interface IAwsCdkEcsHandler
    {
        void AddAsgCapacityProviderToCluster(AsgCapacityProvider asgCapacityProvider, Cluster cluster);
        void AddAutoScalingGroupToCluster(string asgId, Amazon.CDK.AWS.AutoScaling.AutoScalingGroup autoScalingGroup, Cluster cluster);
        ContainerDefinition AddContainerToTaskDefinition(ref ITaskDefinition taskDefinition, string containerId, IContainerDefinitionOptions containerDefinitionOptions);
        void AddEc2ServiceECSDependencies(IService ecsService, List<AsgCapacityProvider> capacityProviders);
        ILoadBalancerTargetProps AddEc2ServiceToNetworkTargetGroup(IService service, INetworkTargetGroup targetGroup, string containerName, double containerPort);
        AsgCapacityProvider CreateAsgCapacityProvider(string id, string name, double targetCapacityPercent, bool enableManagedTerminationProtection, IAutoScalingGroup autoScalingGroup);
        CapacityProviderStrategy CreateCapacityProviderStrategy(AsgCapacityProvider capacityProvider, int capWeigth, int capBase);
        ContainerDefinition CreateContainerDefinition(string containerId, IContainerDefinitionProps containerDefinitionProps);
        ICluster CreateEC2Cluster(string id, string clusterName, IVpc vpc);
        IService CreateEc2Service(string id, string serviceName, ICluster cluster, TaskDefinition taskDefinition, int? healthCheckGracePeriod, List<CapacityProviderStrategy> capacityProviderStrategies, int? desiredCount);
        TaskDefinition CreateEc2TaskDefinition(string taskDefinitionId, string taskDefinitionFamily, List<EcsDockerVolumeOptions> volumesOptions, IRole taskRole = null);
        IEc2TaskDefinition LocateEc2TaskDefinitionByArn(string id, string arn);
        IService LocateEcsServiceByArn(string identification, string arn);
        IBaseService LocateEcsServiceByAttrs(string id, IEc2ServiceAttributes ec2ServiceAttributes);
    }
}