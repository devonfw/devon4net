using Amazon.CDK.AWS.EC2;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Management
{
    public interface IVpcHandlerManager
    {
        IVpc AddVpc(string cidr, double? maxAzs, DefaultInstanceTenancy defaultInstanceTenancy, string vpcIdentification = null, bool enableDnsSupport = true, bool enableDnsHostnames = true, List<ISubnetConfiguration> subnetConfigurations = null, Dictionary<string, string> tags = null); //NOSONAR number of params
        IVpc LocateVpc(string identification, string vpcId, bool isDefault = true);
    }
}