using Amazon.CDK.AWS.EC2;
using System;
using System.Linq;

namespace Devon4Net.Infrastructure.AWS.CDK.Stack.ResourceStacks
{
    public partial class ProvisionStack
    {
        private void CreateOrLocateVpcs()
        {
            if (CdkOptions == null || CdkOptions.Vpcs?.Any() != true) return;

            foreach (var vpc in CdkOptions.Vpcs)
            {
                if (vpc.LocateInsteadCreate)
                {
                    StackResources.Vpcs.Add(vpc.Id, AwsCdkHandler.LocateVpc(vpc.Id, vpc.AwsVpcId, vpc.IsDefault));
                }
                else
                {
                    throw new ArgumentException("Creating VPCs is not supported yet in CDK auto Settings");
                }
            }
        }

        private IVpc LocateVpc(string vpcId, string exceptionMessageIfVpcDoesNotExist, string exceptionMessageIfVpcIsEmpty = null)
        {
            return StackResources.Locate<IVpc>(vpcId, exceptionMessageIfVpcDoesNotExist, exceptionMessageIfVpcIsEmpty);
        }
    }
}
