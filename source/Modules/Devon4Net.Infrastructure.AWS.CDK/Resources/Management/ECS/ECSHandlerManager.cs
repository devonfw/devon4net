using Amazon.CDK.AWS.AutoScaling;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.ECS;
using Amazon.CDK.AWS.ElasticLoadBalancingV2;
using Amazon.CDK.AWS.IAM;
using Devon4Net.Infrastructure.AWS.CDK.Options.Resources;
using System;
using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Management
{
    public partial class AwsCdkHandlerManager
    {
        public void AddAutoScalingGroupToCluster(string asgId, AutoScalingGroup autoScalingGroup, Cluster cluster)
        {
            HandlerResources.AwsCdkEcsHandler.AddAutoScalingGroupToCluster(asgId, autoScalingGroup, cluster);
        }

        internal CapacityProviderStrategy CreateCapacityProviderStrategy(AsgCapacityProvider capacityProvider, int capWeigth, int capBase)
        {
            return HandlerResources.AwsCdkEcsHandler.CreateCapacityProviderStrategy(capacityProvider, capWeigth, capBase);
        }

        public IService AddElasticContainerEc2Service(string id, string serviceName, ICluster cluster, TaskDefinition taskDefinition, int? healthCheckGracePeriod, List<CapacityProviderStrategy> capacityProviderStrategies, int? desiredCount)
        {
            return HandlerResources.AwsCdkEcsHandler.CreateEc2Service(id, serviceName, cluster, taskDefinition, healthCheckGracePeriod, capacityProviderStrategies, desiredCount);
        }

        public ILoadBalancerTargetProps AddEc2ServiceToNetworkTargetGroup(IService service, INetworkTargetGroup targetGroup, string containerName, double containerPort)
        {
            return HandlerResources.AwsCdkEcsHandler.AddEc2ServiceToNetworkTargetGroup(service, targetGroup, containerName, containerPort);
        }

        public ICluster CreateEC2Cluster(string id, string clusterName, IVpc vpc)
        {
            return HandlerResources.AwsCdkEcsHandler.CreateEC2Cluster(id, clusterName, vpc);
        }

        public TaskDefinition CreateEc2TaskDefinition(string taskDefinitionId, string taskDefinitionFamily, List<EcsDockerVolumeOptions> volumesOptions, IRole taskRole = null)
        {
            if (string.IsNullOrEmpty(taskDefinitionId) || string.IsNullOrEmpty(taskDefinitionFamily))
            {
                throw new ArgumentException("Plase provide a valid taskDefinitionId");
            }
            return HandlerResources.AwsCdkEcsHandler.CreateEc2TaskDefinition(taskDefinitionId, taskDefinitionFamily, volumesOptions, taskRole);
        }

        public IService LocateEcsServiceByArn(string id, string arn)
        {
            return HandlerResources.AwsCdkEcsHandler.LocateEcsServiceByArn(id, arn);
        }

        public IBaseService LocateEcsServiceByAttrs(string id, Ec2ServiceAttributes attributes)
        {
            return HandlerResources.AwsCdkEcsHandler.LocateEcsServiceByAttrs(id, attributes);
        }

        public IEc2TaskDefinition LocateEc2TaskDefinitionByArn(string id, string arn)
        {
            return HandlerResources.AwsCdkEcsHandler.LocateEc2TaskDefinitionByArn(id, arn);
        }

        public ContainerDefinition CreateContainerDefinitionByProps(string containerId, IContainerDefinitionProps containerDefinitionProps)
        {
            return HandlerResources.AwsCdkEcsHandler.CreateContainerDefinition(containerId, containerDefinitionProps);
        }

        public AsgCapacityProvider CreateAsgCapacityProvider(string id, string name, double targetCapacityPercent, bool enableManagedTerminationProtection, IAutoScalingGroup autoScalingGroup)
        {
            return HandlerResources.AwsCdkEcsHandler.CreateAsgCapacityProvider(id, name, targetCapacityPercent, enableManagedTerminationProtection, autoScalingGroup);
        }

        public void AddAsgCapacityProviderToCluster(AsgCapacityProvider asgCapacityProvider, Cluster cluster)
        {
            HandlerResources.AwsCdkEcsHandler.AddAsgCapacityProviderToCluster(asgCapacityProvider, cluster);
        }
        public void AddEc2ServiceECSDependencies(IService ecsService, List<AsgCapacityProvider> capacityProviders)
        {
            HandlerResources.AwsCdkEcsHandler.AddEc2ServiceECSDependencies(ecsService, capacityProviders);
        }
    }
}
