using Amazon.CDK.AWS.AutoScaling;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.IAM;
using Devon4Net.Infrastructure.AWS.CDK.Options.Resources;
using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Management
{
    public partial class AwsCdkHandlerManager: IAutoScalingGroupHandlerManager
    {
        public IAutoScalingGroup AddAutoScalingGroup(string id, string autoScalingGroupName, string instanceType, string machineImage, string machineAmiImage, IVpc vpc, bool allowAllOutbound, int minCapacity, int maxCapacity, int desiredCapacity, string region, ISecurityGroup securityGroup, string timeOutCreation, IRole role, ISubnet[] subnets, string keyPairName, bool enableProtectionFromScaleIn, List<BlockDevicesOptions> blockDevices = null, List<string> userData = null)
        {
            return HandlerResources.AwsCdkAutoScalingGroupHandler.Create(id, autoScalingGroupName, instanceType, machineImage, machineAmiImage, vpc, allowAllOutbound, minCapacity, maxCapacity, desiredCapacity, region, securityGroup, timeOutCreation, role, subnets, keyPairName, enableProtectionFromScaleIn, blockDevices, userData);
        }
    }
}
