using Amazon.CDK.AWS.Events;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.EventBridge
{
    public interface IAwsCdkEventBridgeHandler
    {
        IRule CreateRule(string id, IRuleProps props);
        IRuleProps CreateRuleProps(string name, IRuleTarget[] targets, IEventPattern eventPattern = null, string triggerHour = null, string triggerMinute = null, string description = null, bool enabled = true, IEventBus eventBus = null);//NOSONAR number of params
        IEventPattern CreateS3EventPattern(string[] S3Events, string[] bucketNames, string[] filterIds = null, string[] filterRegions = null, string[] filterResourceArns = null, string[] filterSources = null, string[] filterTimestamps = null, string[] filterVersions = null, string[] detailType = null, string[] account = null);//NOSONAR number of params
    }
}