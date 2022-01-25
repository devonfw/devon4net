using Amazon.CDK.AWS.DynamoDB;
using Amazon.CDK.AWS.EC2;
using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Management
{
    public interface IVpcHandlerManager
    {
        IVpc AddVpc(string cidr, double? maxAzs, DefaultInstanceTenancy defaultInstanceTenancy, string vpcIdentification = null, bool enableDnsSupport = true, bool enableDnsHostnames = true, List<ISubnetConfiguration> subnetConfigurations = null, Dictionary<string, string> tags = null);
        IVpc LocateVpc(string identification, string vpcId, bool isDefault = true);
    }
}