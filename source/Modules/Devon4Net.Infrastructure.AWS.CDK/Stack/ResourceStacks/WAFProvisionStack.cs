using Devon4Net.Infrastructure.AWS.CDK.Options.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace Devon4Net.Infrastructure.AWS.CDK.Stack
{
    public partial class ProvisionStack
    {
        public void CreateWaf()
        {
            if (CdkOptions == null || CdkOptions.Wafs == null) return;

            foreach (var wafOption in CdkOptions.Wafs)
            {
                GetWafResources(wafOption, out var apiGatewayDeploymentStage, out var apiGatewayArn, out var scope, out var cloudWatchMetricsEnabled, out var sampledRequestsEnabled);

                var webAcl = AwsCdkHandler.CreateWebAcl(wafOption.Id, wafOption.Name, wafOption.Description, "", scope, cloudWatchMetricsEnabled, sampledRequestsEnabled);

                var webAclAssociation = AwsCdkHandler.CreateWebAclAssociation($"{wafOption.Id}-association", webAcl, apiGatewayArn);

                // This is necessary because the CDK does not detect the dependency between the deployment stage and the webAcl association, and generally, the creation of the association will begin before the deployment stage is created, causing the latter not being found when the CDK is deployed
                webAclAssociation.AddDependsOn(apiGatewayDeploymentStage);

                StackResources.WebAcls.Add(wafOption.Id, webAcl);
            }
        }

        private void GetWafResources(WafOptions wafOption, out Amazon.CDK.CfnResource apiGatewayDeploymentStage, out string apiGatewayArn, out string scope, out bool cloudWatchMetricsEnabled, out bool sampledRequestsEnabled)
        {
            // Locate Api Gateway, and from it gets its deployment stage and ARN
            var apiGateway = LocateApiGateway(wafOption.AssociatedApiGatewayId, $"The Api Gateway {wafOption.AssociatedApiGatewayId} could not be found for the waf {wafOption.Id}.");
            apiGatewayDeploymentStage = apiGateway.DeploymentStage.Node.FindChild("Resource") as Amazon.CDK.CfnResource;
            apiGatewayArn = $"arn:aws:apigateway:{AwsCdkHandler.Region}::/restapis/{apiGateway.RestApiId}/stages/{apiGateway.DeploymentStage.StageName}";

            // Parse scope
            scope = string.IsNullOrWhiteSpace(wafOption.Scope) ? "REGIONAL" : wafOption.Scope;

            // Parse CloudWatchMetricsEnabled
            cloudWatchMetricsEnabled = wafOption.CloudWatchMetricsEnabled ?? true;

            // Parse SampledRequestsEnabled
            sampledRequestsEnabled = wafOption.SampledRequestsEnabled ?? true;
        }

    }
}
