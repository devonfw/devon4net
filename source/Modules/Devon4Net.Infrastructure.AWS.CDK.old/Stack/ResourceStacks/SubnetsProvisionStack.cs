using Amazon.CDK.AWS.EC2;

namespace Devon4Net.Infrastructure.AWS.CDK.Stack.ResourceStacks
{
    public partial class ProvisionStack
    {
        private void LocateSubnets()
        {
            if (CdkOptions == null || CdkOptions.Subnets?.Any() != true) return;
            foreach (var subnetItem in from subnetConfiguration in CdkOptions.Subnets
                                       where subnetConfiguration.Subnets != null && subnetConfiguration.Subnets?.Any() == true
                                       from subnetItem in subnetConfiguration.Subnets
                                       select subnetItem)
            {
                StackResources.Subnets.Add(subnetItem.Id, AwsCdkHandler.LocateSubnetById(subnetItem.Id, subnetItem.AwsSubnetId));
            }
        }

        private ISubnet LocateSubnet(string subnetId, string exceptionMessageIfSubnetDoesNotExist, string exceptionMessageIfSubnetIsEmpty = null)
        {
            return StackResources.Locate<ISubnet>(subnetId, exceptionMessageIfSubnetDoesNotExist, exceptionMessageIfSubnetIsEmpty);
        }
    }
}
