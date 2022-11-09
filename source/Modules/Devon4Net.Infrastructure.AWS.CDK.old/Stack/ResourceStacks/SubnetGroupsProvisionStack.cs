using Amazon.CDK.AWS.RDS;
using System.Linq;

namespace Devon4Net.Infrastructure.AWS.CDK.Stack.ResourceStacks
{
    public partial class ProvisionStack
    {
        private void LocateSubnetGroups()
        {
            if (CdkOptions == null || CdkOptions.Subnets?.Any() != true) return;
            foreach (var subnetGroup in from subnetConfiguration in CdkOptions.Subnets
                                        where subnetConfiguration.SubnetGroups != null && subnetConfiguration.SubnetGroups?.Any() == true
                                        from subnetGroup in subnetConfiguration.SubnetGroups
                                        select subnetGroup)
            {
                StackResources.SubnetGroups.Add(subnetGroup.Id, AwsCdkHandler.LocateSubnetGroupByName(subnetGroup.Id, subnetGroup.SubnetGroupName));
            }
        }

        private ISubnetGroup LocateSubnetGroup(string subnetGroupId, string exceptionMessageIfSubnetGroupDoesNotExist, string exceptionMessageIfSubnetGroupIsEmpty = null)
        {
            return StackResources.Locate<ISubnetGroup>(subnetGroupId, exceptionMessageIfSubnetGroupDoesNotExist, exceptionMessageIfSubnetGroupIsEmpty);
        }
    }
}
