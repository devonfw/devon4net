using Amazon.CDK.AWS.AutoScaling;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.ECS;
using Amazon.CDK.AWS.ElasticLoadBalancingV2;
using Constructs;
using Devon4Net.Infrastructure.AWS.CDK.Entities;
using Devon4Net.Infrastructure.AWS.CDK.Options.Resources;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.ECS
{
    public class AwsCdkEcsHandler : AwsCdkBaseHandler, IAwsCdkEcsHandler
    {
        private TagHandler TagHandler { get; }

        public AwsCdkEcsHandler(Construct scope, string applicationName, string environmentName, string region) : base(scope, applicationName, environmentName, region)
        {
            TagHandler = new TagHandler();
        }

        public void AddAutoScalingGroupToCluster(string asgId, Amazon.CDK.AWS.AutoScaling.AutoScalingGroup autoScalingGroup, Cluster cluster)
        {
            cluster.AddAsgCapacityProvider(new AsgCapacityProvider(Scope, $"{asgId}-capacityprovider", new AsgCapacityProviderProps
            {
                AutoScalingGroup = autoScalingGroup
            }));
        }

        public IService CreateEc2Service(string id, string serviceName, ICluster cluster, TaskDefinition taskDefinition, int? healthCheckGracePeriod, List<CapacityProviderStrategy> capacityProviderStrategies, int? desiredCount)
        {
            var result = CreateEc2Service(new Ec2ServiceEntity
            {
                Id = id,
                ServiceName = serviceName,
                Cluster = cluster,
                TaskDefinition = taskDefinition,
                HealthCheckGracePeriod = healthCheckGracePeriod,
                CapacityProviderStrategies = capacityProviderStrategies,
                DesiredCount = desiredCount
            });

            TagHandler.AddCustomTag("AmazonECSManaged", "true", result);
            return result;
        }

        public ILoadBalancerTargetProps AddEc2ServiceToNetworkTargetGroup(IService service, INetworkTargetGroup targetGroup, string containerName, double containerPort)
        {
            var castedService = service as Ec2Service;

            var result = castedService.LoadBalancerTarget(new LoadBalancerTargetOptions
            {
                ContainerName = containerName,
                ContainerPort = containerPort
            })
            .AttachToNetworkTargetGroup(targetGroup);

            return result;
        }

        private Ec2Service CreateEc2Service(Ec2ServiceEntity entity)
        {
            var result = new Ec2Service(Scope, /* entity.Id */ entity.ServiceName, new Ec2ServiceProps
            {
                Cluster = entity.Cluster,
                TaskDefinition = entity.TaskDefinition,
                HealthCheckGracePeriod = entity.HealthCheckGracePeriod == null ? null : Amazon.CDK.Duration.Seconds(entity.HealthCheckGracePeriod.Value),
                CapacityProviderStrategies = entity.CapacityProviderStrategies?.Any() == true ? entity.CapacityProviderStrategies.ToArray() : default,
                DesiredCount = entity.DesiredCount
            });

            TagHandler.LogTag($"{ApplicationName}{EnvironmentName}{entity.ServiceName}Ec2Service", result);
            return result;
        }

        public IService LocateEcsServiceByArn(string identification, string arn)
        {
            return Ec2Service.FromEc2ServiceArn(Scope, identification, arn);
        }

        public IBaseService LocateEcsServiceByAttrs(string id, IEc2ServiceAttributes ec2ServiceAttributes)
        {
            return Ec2Service.FromEc2ServiceAttributes(Scope, id, ec2ServiceAttributes);
        }

        public ICluster CreateEC2Cluster(string id, string clusterName, IVpc vpc)
        {
            var cluster = new Cluster(Scope, id, new ClusterProps
            {
                Vpc = vpc,
                ClusterName = clusterName,
            });

            TagHandler.LogTag(ApplicationName + EnvironmentName + clusterName, cluster);
            return cluster;
        }

        public AsgCapacityProvider CreateAsgCapacityProvider(string id, string name, double targetCapacityPercent, bool enableManagedTerminationProtection, IAutoScalingGroup autoScalingGroup)
        {
            var capacityProvider = new AsgCapacityProvider(Scope, id, new AsgCapacityProviderProps
            {
                AutoScalingGroup = autoScalingGroup,
                CapacityProviderName = name,
                TargetCapacityPercent = targetCapacityPercent,
                EnableManagedScaling = true,
                EnableManagedTerminationProtection = enableManagedTerminationProtection
            });

            return capacityProvider;
        }

        public void AddAsgCapacityProviderToCluster(AsgCapacityProvider asgCapacityProvider, Cluster cluster)
        {
            cluster.AddAsgCapacityProvider(asgCapacityProvider);
        }

        public TaskDefinition CreateEc2TaskDefinition(string taskDefinitionId, string taskDefinitionFamily, List<EcsDockerVolumeOptions> volumesOptions)
        {
            List<Amazon.CDK.AWS.ECS.Volume> volumes = new List<Amazon.CDK.AWS.ECS.Volume>();
            if (volumesOptions?.Any() == true)
            {
                foreach (var volOpts in volumesOptions)
                {
                    if (!Enum.TryParse(volOpts.Scope, out Scope scope))
                    {
                        throw new ArgumentException($"The scope {volOpts.Scope} is not a valid Scope");
                    }
                    volumes.Add(new Amazon.CDK.AWS.ECS.Volume
                    {
                        Name = volOpts.Name,
                        DockerVolumeConfiguration = new DockerVolumeConfiguration
                        {
                            Autoprovision = volOpts.AutoProvision,
                            Driver = volOpts.Driver,
                            Scope = scope
                        }
                    });
                }
            }
            var task = new TaskDefinition(Scope, taskDefinitionId, new TaskDefinitionProps
            {
                Family = taskDefinitionFamily,
                Compatibility = Compatibility.EC2,
                NetworkMode = NetworkMode.NAT,
                Volumes = volumes.ToArray()
            });
            TagHandler.LogTag($"{ApplicationName}{EnvironmentName}{taskDefinitionId}Ec2Task", task);
            return task;
        }

        public ContainerDefinition AddContainerToTaskDefinition(ref ITaskDefinition taskDefinition, string containerId, IContainerDefinitionOptions containerDefinitionOptions)
        {
            var taskCasted = taskDefinition as TaskDefinition;
            var result = taskCasted.AddContainer(containerId, containerDefinitionOptions);
            TagHandler.LogTag($"{ApplicationName}{EnvironmentName}{containerId}ContainerDefinition", result);
            return result;
        }

        public IEc2TaskDefinition LocateEc2TaskDefinitionByArn(string id, string arn)
        {
            return (Ec2TaskDefinition)Ec2TaskDefinition.FromEc2TaskDefinitionArn(Scope, id, arn);
        }

        public ContainerDefinition CreateContainerDefinition(string containerId, IContainerDefinitionProps containerDefinitionProps)
        {
            return new ContainerDefinition(Scope, containerId, containerDefinitionProps);
        }

        public CapacityProviderStrategy CreateCapacityProviderStrategy(AsgCapacityProvider capacityProvider, int capWeigth, int capBase)
        {
            return new CapacityProviderStrategy
            {
                CapacityProvider = capacityProvider.CapacityProviderName,
                Base = capWeigth,
                Weight = capBase
            };
        }

        public void AddEc2ServiceECSDependencies(IService ecsService, List<AsgCapacityProvider> capacityProviders)
        {
            if (capacityProviders != null)
            {
                foreach (var provider in capacityProviders)
                {
                    ecsService.Node.AddDependency(provider);
                }
            }
        }
    }
}
