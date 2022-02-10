using Amazon.CDK.AWS.WAFv2;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Management
{
    public partial class AwsCdkHandlerManager
    {
        public CfnWebACL CreateWebAcl(string id, string name, string description = null, string metricName = null, string scope = "REGIONAL", bool cloudWatchMetricsEnabled = true, bool sampledRequestsEnabled = true)
        {
            return HandlerResources.AwsCdkWafHandler.CreateWebAcl(id, name, description, metricName, scope, cloudWatchMetricsEnabled, sampledRequestsEnabled);
        }

        public CfnWebACLAssociation CreateWebAclAssociation(string id, CfnWebACL webAcl, string resourceArn)
        {
            return HandlerResources.AwsCdkWafHandler.CreateWebAclAssociation(id, webAcl, resourceArn);
        }
    }
}
