using Amazon.CDK;
using Amazon.CDK.AWS.AutoScaling;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.IAM;
using Constructs;
using Devon4Net.Infrastructure.AWS.CDK.Entities;
using Devon4Net.Infrastructure.AWS.CDK.Options.Resources;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.AutoScalingGroup
{
    public class AwsCdkAutoScalingGroupHandler : AwsCdkBaseHandler, IAwsCdkAutoScalingGroupHandler
    {
        private TagHandler TagHandler { get; }

        public AwsCdkAutoScalingGroupHandler(Construct scope, string applicationName, string environmentName, string region) : base(scope, applicationName, environmentName, region)
        {
            TagHandler = new TagHandler();
        }

        public IAutoScalingGroup Create(string id, string instanceName, string instanceType, string machineImageType,  string machineAmiImage, IVpc vpc, bool allowAllOutbound, int minCapacity, int maxCapacity, int desiredCapacity, string region, ISecurityGroup securityGroup, string timeOutCreation, IRole role, ISubnet[] subnets, string keyPairName, bool enableProtectionFromScaleIn, List<BlockDevicesOptions> blockDevicesOptions, List<string> userData)
        {
            var result = CreateInstance(new AutoScalingGroupEntity
            {
                Id = id,
                Vpc = vpc,
                Subnets = subnets,
                AutoScalingGroupName = instanceName,
                InstanceType = new InstanceType(instanceType),
                MachineImage = GetMachineImage(machineImageType, new Dictionary<string, string> { { region, machineAmiImage } }),
                AllowAllOutbound = allowAllOutbound,
                MinCapacity = minCapacity,
                MaxCapacity = maxCapacity,
                DesiredCapacity = desiredCapacity,
                SecurityGroup = securityGroup,
                TimeOutCreation = timeOutCreation,
                Role = role,
                KeyPairName = keyPairName,
                BlockDevices = CreateBlockDevices(blockDevicesOptions),
                EnableProtectionFromScaleIn = enableProtectionFromScaleIn,
                UserData = userData
            });

            TagHandler.LogTag(ApplicationName + EnvironmentName + instanceName, result);
            return result;
        }

        private static IMachineImage GetMachineImage(string machineImage, Dictionary<string, string> amiMap)
        {
            return (machineImage.ToLower()) switch
            {
                "genericlinux" => MachineImage.GenericLinux(amiMap),
                "genericwindows" => MachineImage.GenericWindows(amiMap),
                _ => throw new ArgumentException("No MachineImage argument was provided. Please put GenericLinux or GenericWindows"),
            };
        }

        private Amazon.CDK.AWS.AutoScaling.AutoScalingGroup CreateInstance(AutoScalingGroupEntity autoScalingGroupEntity)
        {
            var healthCheck = HealthCheck.Ec2(new Ec2HealthCheckOptions
            {
                Grace = Duration.Minutes(1)
            });

            var userData = UserData.ForOperatingSystem(autoScalingGroupEntity.MachineImage.GetImage(Scope).OsType);
            if (autoScalingGroupEntity.UserData?.Any() == true)
            {
                userData.AddCommands(autoScalingGroupEntity.UserData.ToArray());
            }

            return new Amazon.CDK.AWS.AutoScaling.AutoScalingGroup(Scope, autoScalingGroupEntity.Id, new AutoScalingGroupProps
            {
                InstanceType = autoScalingGroupEntity.InstanceType,
                MachineImage =  autoScalingGroupEntity.MachineImage,
                Vpc = autoScalingGroupEntity.Vpc,
                VpcSubnets = new SubnetSelection
                {
                    Subnets = autoScalingGroupEntity.Subnets
                },
                AllowAllOutbound = autoScalingGroupEntity.AllowAllOutbound,
                MinCapacity = autoScalingGroupEntity.MinCapacity,
                MaxCapacity = autoScalingGroupEntity.MaxCapacity,
                SecurityGroup = autoScalingGroupEntity.SecurityGroup,
                DesiredCapacity = autoScalingGroupEntity.DesiredCapacity,
                HealthCheck = healthCheck,
                Role = autoScalingGroupEntity.Role,
                NewInstancesProtectedFromScaleIn = autoScalingGroupEntity.EnableProtectionFromScaleIn,
                KeyName = autoScalingGroupEntity.KeyPairName,
                BlockDevices = autoScalingGroupEntity.BlockDevices,
                AutoScalingGroupName = autoScalingGroupEntity.AutoScalingGroupName,
                UserData = userData
            });
        }

        private static Amazon.CDK.AWS.AutoScaling.BlockDevice[] CreateBlockDevices(List<BlockDevicesOptions> blockDevicesOptions)
        {
            if (blockDevicesOptions is null)
                return default;

            var blockDevices = new List<Amazon.CDK.AWS.AutoScaling.BlockDevice>();
            foreach (var blockDevice in blockDevicesOptions)
            {
                blockDevices.Add(new Amazon.CDK.AWS.AutoScaling.BlockDevice()
                {
                    DeviceName = blockDevice.BlockDeviceName,
                    Volume = CreateBlockDeviceVolume(blockDevice.BlockDeviceVolume)
                });
            }

            return blockDevices.ToArray();
        }

        private static Amazon.CDK.AWS.AutoScaling.BlockDeviceVolume CreateBlockDeviceVolume(BlockDeviceVolumeOptions blockDevicesOptions)
        {
            if (blockDevicesOptions is null)
                return default;

            return blockDevicesOptions.CreationOption switch
            {
                Enums.StorageCreationOption.EBS => CreateEbsVolume(blockDevicesOptions.EbsDevice),
                Enums.StorageCreationOption.EBS_FROM_SNAPSHOT_ID => CreateEbsVolumeFromSnapshotId(blockDevicesOptions.EbsDevice),
                Enums.StorageCreationOption.EPHEMERAL => CreateEphemeralVolume(blockDevicesOptions.EbsDevice.VolumeIndex),
                Enums.StorageCreationOption.NO_DEVICE => Amazon.CDK.AWS.AutoScaling.BlockDeviceVolume.NoDevice(),
                _ => CreateEbsVolume(blockDevicesOptions.EbsDevice),
            };
        }

        private static Amazon.CDK.AWS.AutoScaling.BlockDeviceVolume CreateEbsVolume(EbsDevicePropsOptions ebsDevice)
        {
            if (ebsDevice.VolumeSize == null)
                throw new ArgumentException("VolumeSize must not be null for the creation of an EbsVolume");

            return Amazon.CDK.AWS.AutoScaling.BlockDeviceVolume.Ebs(ebsDevice.VolumeSize.Value, new Amazon.CDK.AWS.AutoScaling.EbsDeviceOptions()
            {
                DeleteOnTermination = ebsDevice.DeleteOnTermination,
                Encrypted = ebsDevice.Encrypted,
                Iops = ebsDevice.Iops,
                VolumeType = ebsDevice.VolumeType
            });
        }

        private static Amazon.CDK.AWS.AutoScaling.BlockDeviceVolume CreateEbsVolumeFromSnapshotId(EbsDevicePropsOptions ebsDevice)
        {
            if (ebsDevice.SnapshotId == null)
                throw new ArgumentException("SnapshotId must not be null for the creation of an EbsVolumeFromSnapshot");

            return Amazon.CDK.AWS.AutoScaling.BlockDeviceVolume.EbsFromSnapshot(ebsDevice.SnapshotId, new Amazon.CDK.AWS.AutoScaling.EbsDeviceSnapshotOptions()
            {
                DeleteOnTermination = ebsDevice.DeleteOnTermination,
                VolumeSize = ebsDevice.VolumeSize,
                Iops = ebsDevice.Iops,
                VolumeType = ebsDevice.VolumeType
            });
        }

        private static Amazon.CDK.AWS.AutoScaling.BlockDeviceVolume CreateEphemeralVolume(uint? index)
        {
            if (index == null)
                throw new ArgumentException("ValumeIndex must not be null for the creation of an EphemeralVolume");

            return Amazon.CDK.AWS.AutoScaling.BlockDeviceVolume.Ephemeral(index.Value);
        }
    }
}