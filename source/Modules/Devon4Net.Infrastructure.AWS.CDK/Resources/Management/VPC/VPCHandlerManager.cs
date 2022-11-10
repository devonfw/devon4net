using Amazon.CDK.AWS.EC2;
using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Management
{
    public partial class AwsCdkHandlerManager : IVpcHandlerManager
    {
        public IVpc AddVpc(IIpAddresses ipAddresses, double? maxAzs, DefaultInstanceTenancy defaultInstanceTenancy, string vpcIdentification = null, bool enableDnsSupport = true, bool enableDnsHostnames = true, List<ISubnetConfiguration> subnetConfigurations = null, Dictionary<string, string> tags = null)
        {
            return HandlerResources.AwsCdkVpcHandler.Create(string.IsNullOrEmpty(vpcIdentification) ? $"{ApplicationName}{EnvironmentName}vpc" : vpcIdentification, ipAddresses, maxAzs, defaultInstanceTenancy, enableDnsSupport, enableDnsHostnames, subnetConfigurations, tags);
        }

        [Obsolete("2022/11/09 new AWS CDK version. Use ipAddresses instead")]
        public IVpc AddVpc(string cidr, double? maxAzs, DefaultInstanceTenancy defaultInstanceTenancy, string vpcIdentification = null, bool enableDnsSupport = true, bool enableDnsHostnames = true, List<ISubnetConfiguration> subnetConfigurations = null, Dictionary<string, string> tags = null)
        {
            return HandlerResources.AwsCdkVpcHandler.Create(string.IsNullOrEmpty(vpcIdentification) ? $"{ApplicationName}{EnvironmentName}vpc" : vpcIdentification, cidr, maxAzs, defaultInstanceTenancy, enableDnsSupport, enableDnsHostnames, subnetConfigurations, tags);
        }

        public IVpc LocateVpc(string identification, string vpcId, bool isDefault = true)
        {
            return HandlerResources.AwsCdkVpcHandler.Locate(identification, vpcId, isDefault);
        }
    }
}
