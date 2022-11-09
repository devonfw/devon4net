using Amazon.CDK.AWS.WAFv2;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.WAF
{
    public  interface IAwsCdkWafHandler
    {
        CfnWebACL CreateWebAcl(string id, string name, string description = null, string metricName = null, string scope = "REGIONAL", bool cloudWatchMetricsEnabled = true, bool sampledRequestsEnabled = true);
        CfnWebACLAssociation CreateWebAclAssociation(string id, CfnWebACL webAcl, string resourceArn);
    }
}