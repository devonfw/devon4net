using Amazon.CDK.AWS.Events;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Management
{
    public partial class AwsCdkHandlerManager : IEventBridgeManager
    {
        public IRule CreateEventBridgeRule(string id, IRuleProps props)
        {
            return HandlerResources.AwsCdkEventBridgeHandler.CreateRule(id, props);
        }

        public IRuleProps CreateEventBridgeRuleProps(string name, IRuleTarget[] targets, IEventPattern eventPattern = null, string triggerHour = null, string triggerMinute = null, string description = null, bool enabled = true, IEventBus eventBus = null)
        {
            return HandlerResources.AwsCdkEventBridgeHandler.CreateRuleProps(name, targets, eventPattern, triggerHour, triggerMinute, description, enabled, eventBus);
        }

        public IEventPattern CreateEventBridgeS3Pattern(string[] S3Events, string[] bucketNames, string[] filterIds = null, string[] filterRegions = null, string[] filterResourceArns = null, string[] filterSources = null, string[] filterTimestamps = null, string[] filterVersions = null, string[] detailType = null, string[] account = null)
        {
            return HandlerResources.AwsCdkEventBridgeHandler.CreateS3EventPattern(S3Events, bucketNames, filterIds, filterRegions, filterResourceArns, filterSources, filterTimestamps, filterVersions, detailType, account);
        }
    }
}
