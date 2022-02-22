using Amazon.CDK;
using Amazon.CDK.AWS.AutoScaling;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.ECS;
using Devon4Net.Infrastructure.AWS.CDK.Options.Resources;

namespace Devon4Net.Infrastructure.AWS.CDK.Stack
{
    public partial class ProvisionStack
    {
        private void CreateEcsAutoScalingGroups()
        {
            if (CdkOptions == null || CdkOptions.AutoScalingGroups?.Any() != true) return;

            foreach (var autoScalingGroup in CdkOptions.AutoScalingGroups)
            {
                var vpc = LocateVpc(autoScalingGroup.VpcId, $"The VPC name {autoScalingGroup.VpcId} in Auto Scaling Group {autoScalingGroup.AutoScalingGroupName} does not exist");
                var securityGroup = StackResources.SecurityGroups?.FirstOrDefault(s => s.Key == autoScalingGroup.SecurityGroupId).Value;
                var role = LocateRole(autoScalingGroup.RoleId, $"The role {autoScalingGroup.RoleId} does not exists");

                var subnets = new List<ISubnet>();
                foreach (var subnetId in autoScalingGroup.SubnetsId)
                {
                    subnets.Add(LocateSubnet(subnetId, $"The subnet {subnetId} does not exists."));
                }

                var asgGroup = AwsCdkHandler.AddAutoScalingGroup(autoScalingGroup.Id, autoScalingGroup.AutoScalingGroupName, autoScalingGroup.InstanceTypeId, autoScalingGroup.MachineImage, autoScalingGroup.AmiId, vpc, autoScalingGroup.AllowAllOutbound, autoScalingGroup.MinCapacity, autoScalingGroup.MaxCapacity, autoScalingGroup.DesiredCapacity, autoScalingGroup.MachineImageRegion, securityGroup, autoScalingGroup.CreationTimeOut, role, subnets.ToArray(), autoScalingGroup.KeyPairName, autoScalingGroup.EnableProtectionFromScaleIn, autoScalingGroup.BlockDevices, autoScalingGroup.UserData);
                StackResources.AutoScalingGroups.Add(autoScalingGroup.Id, asgGroup);
            }
        }

        private void CreateEcsTaskDefinitions()
        {
            if (CdkOptions == null || CdkOptions.EcsTaskDefinitions?.Any() != true) return;

            StackResources.EcsTaskDefinitions = new Dictionary<string, TaskDefinition>();

            foreach (var taskDefinitionOpt in CdkOptions.EcsTaskDefinitions)
            {
                TaskDefinition taskDefinition = null;
                if (!string.IsNullOrWhiteSpace(taskDefinitionOpt.RoleId))
                {
                    var locatedRole = LocateRole(taskDefinitionOpt.RoleId, $"Could not find role with ID {taskDefinitionOpt.RoleId}");
                    taskDefinition = AwsCdkHandler.CreateEc2TaskDefinition(taskDefinitionOpt.Id, taskDefinitionOpt.Family, taskDefinitionOpt.Volumes, locatedRole);
                }
                else
                {
                    AwsCdkHandler.CreateEc2TaskDefinition(taskDefinitionOpt.Id, taskDefinitionOpt.Family, taskDefinitionOpt.Volumes);
                }

                StackResources.EcsTaskDefinitions.Add(taskDefinitionOpt.Id, taskDefinition);
            }
        }

        private void ValidateCapacityProvider(AsgCapacityProviderOptions asgCapacityProviderOptions)
        {
            if (string.IsNullOrWhiteSpace(asgCapacityProviderOptions.AutoScalingGroupId))
            {
                throw new ArgumentException("The capacity provider should have an autoscaling group Id");
            }
            if (string.IsNullOrWhiteSpace(asgCapacityProviderOptions.ClusterId))
            {
                throw new ArgumentException("The capacity provider should have a cluster id");
            }
            if (string.IsNullOrWhiteSpace(asgCapacityProviderOptions.Id))
            {
                throw new ArgumentException("The capacity provider should have an Id");
            }
            if (string.IsNullOrWhiteSpace(asgCapacityProviderOptions.Name))
            {
                throw new ArgumentException("The capacity provider should have a name");
            }
            if (asgCapacityProviderOptions.TargetCapacityPercent == default)
            {
                throw new ArgumentException("The capacity provider should have a TargetCapacityPercent greater than 0");
            }
        }

        private void ValidateClusterOptions(EcsClusterOptions cluster)
        {
            if (string.IsNullOrWhiteSpace(cluster.ClusterName))
            {
                throw new ArgumentException("The cluster needs a name");
            }
            if (string.IsNullOrWhiteSpace(cluster.Id))
            {
                throw new ArgumentException("The cluster needs an Id");
            }
            if (string.IsNullOrWhiteSpace(cluster.VpcId))
            {
                throw new ArgumentException("The cluster need a Vpc Id");
            }
        }

        private void CreateEcsClusters()
        {
            if (CdkOptions == null || CdkOptions.EcsClusters?.Any() != true) return;

            foreach (var cluster in CdkOptions.EcsClusters)
            {
                ValidateClusterOptions(cluster);
            }

            foreach (var cluster in CdkOptions.EcsClusters)
            {
                var vpc = StackResources.Vpcs.FirstOrDefault(v => v.Key == cluster.VpcId).Value;
                var ec2Cluster = AwsCdkHandler.CreateEC2Cluster(cluster.Id, cluster.ClusterName, vpc);
                StackResources.EcsClusters.Add(cluster.Id, ec2Cluster);

                if (cluster.AutoScalingGroupIDs != null)
                {
                    foreach (var asgId in cluster.AutoScalingGroupIDs)
                    {
                        var asg = LocateAutoScalingGroup(asgId, "Could not found the autoScalingGroupneeded for the cluster");
                        AwsCdkHandler.AddAutoScalingGroupToCluster(asgId, asg as AutoScalingGroup, ec2Cluster as Cluster);
                    }
                }
            }
        }

        private void CreateEcsService()
        {
            if (CdkOptions == null || CdkOptions.EcsServices?.Any() != true) return;

            foreach (var service in CdkOptions.EcsServices)
            {
                if (service.LocateInsteadOfCreate)
{
                    StackResources.EcsServices.Add(service.Id, AwsCdkHandler.LocateEcsServiceByArn(service.Id, service.ServiceName));
                }
                else
                {
                    var definitionOptions = CdkOptions.EcsTaskDefinitions?.FirstOrDefault(t => t.Id == service.EcsTaskDefinitionId);

                    if (definitionOptions == null)
                    {
                        throw new ArgumentException("Please add a task definition option properly set up on your json configuration");
                    }

                    switch (definitionOptions.Compatibility)
                    {
                        case Compatibility.EC2:
                            CreateEc2Service(service, definitionOptions);
                            break;
                        case Compatibility.EC2_AND_FARGATE:
                        case Compatibility.FARGATE:
                            throw new NotImplementedException($"The module only supports EC2 Compatibility. {definitionOptions.Compatibility} can be implemented.");
                        default:
                            throw new ArgumentException($"An invalid Compatibility {definitionOptions.Compatibility} was defined for the TaskDefinition {definitionOptions.Id}");
                    }
                }
            }
        }

        private void CreateEc2Service(EcsServiceOptions service, EcsTaskDefinitionOptions definitionOptions)
{
            var taskDefinition = StackResources.EcsTaskDefinitions?.FirstOrDefault(t => t.Key == service.EcsTaskDefinitionId).Value;

            if (taskDefinition == null)
            {
                throw new ArgumentException("Please add a task definition option properly set up on your json configuration. No task definition could be added.");
            }

            var cluster = StackResources.EcsClusters.FirstOrDefault(c => c.Key == service.EcsClusterId).Value;

            if (cluster == null)
            {
                throw new ArgumentException("Please add a cluster definition option properly set up on your json configuration. No cluster could be added.");
            }

            List<CapacityProviderStrategy> strategyItems = null;
            List<AsgCapacityProvider> capacityProviders = null;

            if (service.CapacityProviderStrategy?.Any() == true)
            {
                strategyItems = new List<CapacityProviderStrategy>();
                capacityProviders = new List<AsgCapacityProvider>();

                foreach (var strategy in service.CapacityProviderStrategy)
                {
                    var capacityProvider = LocateAsgCapacityProvider(strategy.ProviderId, "Could not find capacity provider for ecs service");
                    var strategyItem = AwsCdkHandler.CreateCapacityProviderStrategy(capacityProvider, strategy.Weigth, strategy.Base);
                    strategyItems.Add(strategyItem);
                    capacityProviders.Add(capacityProvider);
                }
            }

            var ecsService = AwsCdkHandler.AddElasticContainerEc2Service(service.Id, service.ServiceName, cluster, taskDefinition, service.HealthCheckGracePeriod, strategyItems, service.DesiredCount);
            AwsCdkHandler.AddEc2ServiceECSDependencies(ecsService, capacityProviders);

            CreateContainerDefinition(definitionOptions, taskDefinition);

            if (service.TargetGroups?.Any() == true)
            {
                foreach (var targetGroupConfig in service.TargetGroups)
                {
                    switch (targetGroupConfig.LoadBalancerType)
                    {
                        case "Network":
                            var targetGroup = LocateNetworkTargetGroup(targetGroupConfig.NetworkTargetGroupId, $"The NetworkTargetGroup {targetGroupConfig.NetworkTargetGroupId} could not be found for the service {service.Id}.");
                            AwsCdkHandler.AddEc2ServiceToNetworkTargetGroup(ecsService, targetGroup, targetGroupConfig.ContainerName, targetGroupConfig.Port);
                            break;
                        case "Application":
                            throw new NotImplementedException();
                        case "Gateway":
                            throw new NotImplementedException();
                        default:
                            throw new ArgumentException("Load balancer type in service definition not recognized");
                    }
                }
}

            StackResources.EcsServices.Add(service.Id, ecsService);
        }

        private void CreateContainerDefinition(EcsTaskDefinitionOptions definitionOptions, TaskDefinition taskDefinition)
        {
            foreach (var containerDef in definitionOptions.Containers)
            {
                var ecr = StackResources.EcrRepositories.FirstOrDefault(ecr => ecr.Key == containerDef.RepositoryId);

                if (ecr.Key == null || ecr.Value == null)
                {
                    throw new ArgumentException("Please add a ECR definition option properly set up on your json configuration. No task definition could not be added.");
                }

                var portMapping = new List<PortMapping>();
                if (containerDef.TCPPortMapping?.Any() == true)
                {
                    foreach (var ports in containerDef.TCPPortMapping)
                    {
                        portMapping.Add(new PortMapping
                        {
                            ContainerPort = ports.ContainerPort,
                            HostPort = ports.HostPort,
                            Protocol = Amazon.CDK.AWS.ECS.Protocol.TCP
                        });
                    }
                }

                var containerDefinitionProps = new ContainerDefinitionProps
                {
                    TaskDefinition = taskDefinition,
                    Image = ContainerImage.FromEcrRepository(ecr.Value, containerDef.ImageTag),
                    MemoryLimitMiB = containerDef.MemoryLimitMiB,
                    Cpu = containerDef.CpuUnits,
                    StartTimeout = Duration.Minutes(containerDef.StartTimeOutMinutes),
                    PortMappings = portMapping.ToArray(),
                    Environment = containerDef.EnvironmentVariables,
                    DnsServers = containerDef.DnsServers?.ToArray()
                };

                var container = AwsCdkHandler.CreateContainerDefinitionByProps(containerDef.Id, containerDefinitionProps);

                if (definitionOptions.MountPoints?.Any() == true)
                {
                    var mountPoints = new List<MountPoint>();
                    foreach (var mountPointOption in definitionOptions.MountPoints)
                    {
                        mountPoints.Add(new MountPoint
                        {
                            SourceVolume = mountPointOption.SourceVolume,
                            ContainerPath = mountPointOption.ContainerPath
                        });
                    }
                    container.AddMountPoints(mountPoints.ToArray());
                }
            }
        }

        private ICluster LocateEcsCluster(string clusterId, string exceptionMessageIfClusterDoesNotExist, string exceptionMessageIfClusterIsEmpty = null)
        {
            return StackResources.Locate<ICluster>(clusterId, exceptionMessageIfClusterDoesNotExist, exceptionMessageIfClusterIsEmpty);
        }
    }
}
