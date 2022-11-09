using Amazon.CDK.AWS.Events;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Management.EventBridge
{
    public interface IEventBridgeManager
    {
        IRule CreateEventBridgeRule(string id, IRuleProps props);
        IRuleProps CreateEventBridgeRuleProps(string name, IRuleTarget[] targets, IEventPattern eventPattern = null, string triggerHour = null, string triggerMinute = null, string description = null, bool enabled = true, IEventBus eventBus = null); //NOSONAR number of params
        IEventPattern CreateEventBridgeS3Pattern(string[] S3Events, string[] bucketNames, string[] filterIds = null, string[] filterRegions = null, string[] filterResourceArns = null, string[] filterSources = null, string[] filterTimestamps = null, string[] filterVersions = null, string[] detailType = null, string[] account = null); //NOSONAR number of params
    }
}