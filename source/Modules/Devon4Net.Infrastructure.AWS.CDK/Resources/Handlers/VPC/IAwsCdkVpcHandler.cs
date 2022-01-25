using System.Collections.Generic;
using Amazon.CDK.AWS.EC2;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.VPC
{
    public interface IAwsCdkVpcHandler
    {
        IVpc Create(string identification, string cidr, double? maxAzs, DefaultInstanceTenancy defaultInstanceTenancy = DefaultInstanceTenancy.DEFAULT, bool enableDnsSupport = true, bool enableDnsHostnames = true, List<ISubnetConfiguration> subnetConfigurations = null, Dictionary<string, string> tags = null);
        IVpc Locate(string identification, string vpcId, bool isDefault = true);
        ISubnetSelection GetVpcSubnetSelection(IVpc vpc, string subnetDomainToCheck, string defaultSubnetDomainSeparator = ",", SubnetType defaultSubnetType = SubnetType.PRIVATE_ISOLATED);
    }
}