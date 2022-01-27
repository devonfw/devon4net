using Amazon.CDK.AWS.WAFv2;
using Constructs;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.WAF
{
    public class AwsCdkWafHandler : AwsCdkBaseHandler, IAwsCdkWafHandler
    {
        public AwsCdkWafHandler(Construct scope, string applicationName, string environmentName, string region) : base(scope, applicationName, environmentName, region)
        {
        }

        public CfnWebACL CreateWebAcl(string id, string name, string description = null, string metricName = null, string scope = "REGIONAL", bool cloudWatchMetricsEnabled = true, bool sampledRequestsEnabled = true)
        {
            if (string.IsNullOrWhiteSpace(metricName))
            {
                metricName = name;
            }

            return new CfnWebACL(Scope, id, new CfnWebACLProps
            {
                Name = name,
                Description = description,
                DefaultAction = new CfnWebACL.DefaultActionProperty
                {
                    Allow = new CfnWebACL.AllowActionProperty
                    {
                        
                    }
                },
                Scope = scope,
                VisibilityConfig = new CfnWebACL.VisibilityConfigProperty
                {
                    CloudWatchMetricsEnabled = cloudWatchMetricsEnabled,
                    SampledRequestsEnabled = sampledRequestsEnabled,
                    MetricName = metricName
                }
            });
        }

        public CfnWebACLAssociation CreateWebAclAssociation(string id, CfnWebACL webAcl, string resourceArn)
        {
            return new CfnWebACLAssociation(Scope, id, new CfnWebACLAssociationProps
            {
                WebAclArn = webAcl.AttrArn,
                ResourceArn = resourceArn
            });
        }
    }
}
