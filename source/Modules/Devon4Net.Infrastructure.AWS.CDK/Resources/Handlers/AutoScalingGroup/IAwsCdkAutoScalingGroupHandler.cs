using Amazon.CDK.AWS.AutoScaling;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.IAM;
using Devon4Net.Infrastructure.AWS.CDK.Options.Resources;
using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.AutoScalingGroup
{
    public interface IAwsCdkAutoScalingGroupHandler
    {
        IAutoScalingGroup Create(string id, string instanceName, string instanceType, string machineImageType, string machineAmiImage, IVpc vpc, bool allowAllOutbound, int minCapacity, int maxCapacity, int desiredCapacity, string region, List<ISecurityGroup> securityGroups, string timeOutCreation, IRole role, ISubnet[] subnets, string keyPairName, bool enableProtectionFromScaleIn, List<BlockDevicesOptions> blockDevicesOptions, List<string> userData); //NOSONAR number of parameters 
    }
}