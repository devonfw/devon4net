using Amazon.CDK.AWS.Events.Targets;
using Amazon.CDK.AWS.Events;
using System.Linq;
using Devon4Net.Infrastructure.AWS.CDK.Options.Resources;
using Amazon.CDK.AWS.Lambda;

namespace Devon4Net.Infrastructure.AWS.CDK.Stack.ResourceStacks
{
    public partial class ProvisionStack
    {
        private void CreateRules()
        {
            if (CdkOptions == null || CdkOptions.LambdaRules?.Any() != true) return;

            foreach (var lambdaRule in CdkOptions.LambdaRules)
            {
                GetLambdaRuleResources(lambdaRule, out var lambdaFunction, out var bucketNames);

                var s3Pattern = lambdaRule.S3 == null ? null : AwsCdkHandler.CreateEventBridgeS3Pattern(lambdaRule.S3.Operations, bucketNames);
                var ruleProps = AwsCdkHandler.CreateEventBridgeRuleProps(lambdaRule.Name, new IRuleTarget[] { new LambdaFunction(lambdaFunction) }, eventPattern: s3Pattern, triggerHour: lambdaRule.TriggerHour, triggerMinute: lambdaRule.TriggerMinute, description: lambdaRule.Description, enabled: true, eventBus: null);
                var rule = AwsCdkHandler.CreateEventBridgeRule(lambdaRule.Name, ruleProps);

                StackResources.LambdaRules.Add(lambdaRule.Id, rule);
            }
        }

        private void GetLambdaRuleResources(LambdaRuleOptions lambdaRule, out IFunction lambdaFunction, out string[] bucketNames)
        {
            // Locate lambda function
            lambdaFunction = LocateLambda(lambdaRule.LambdaName,
                $"The lambda name {lambdaRule.LambdaName} of the rule {lambdaRule.Name} was not found",
                $"The rule {lambdaRule.Name} must have a lambda to execute");
            if (lambdaRule?.S3?.BucketIds == null)
            {
                bucketNames = null;
            }
            else
            {
                bucketNames = lambdaRule.S3.BucketIds.Select(x => LocateBucket(x, $"The bucket id {x} inside the lambda rule {lambdaRule.Name} does not exist").BucketName).ToArray();
            }
        }
    }
}
