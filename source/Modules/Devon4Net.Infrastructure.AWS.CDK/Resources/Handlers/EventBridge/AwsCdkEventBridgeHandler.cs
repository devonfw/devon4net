using Amazon.CDK.AWS.Events;
using Constructs;
using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.EventBridge
{
    public class AwsCdkEventBridgeHandler : AwsCdkBaseHandler, IAwsCdkEventBridgeHandler
    {
        public AwsCdkEventBridgeHandler(Construct scope, string applicationName, string environmentName, string region) : base(scope, applicationName, environmentName, region)
        {
        }

        public IRule CreateRule(string id, IRuleProps props)
        {
            return new Rule(Scope, id, props);
        }

        public IRuleProps CreateRuleProps(string name, IRuleTarget[] targets, IEventPattern eventPattern = null, string triggerHour = null, string triggerMinute = null, string description = null, bool enabled = true, IEventBus eventBus = null)
        {
            Schedule schedule = null;

            if (!string.IsNullOrWhiteSpace(triggerHour) || !string.IsNullOrWhiteSpace(triggerMinute))
            {
                schedule = Schedule.Cron(new CronOptions
                {
                    Hour = triggerHour?.Length == 0 ? null : triggerHour,
                    Minute = triggerMinute?.Length == 0 ? null : triggerMinute
                });
            }

            return new RuleProps
            {
                Description = description,
                Enabled = enabled,
                EventBus = eventBus,
                EventPattern = eventPattern,
                RuleName = name,
                Targets = targets,
                Schedule = schedule
            };
        }

        public IEventPattern CreateS3EventPattern(string[] S3Events, string[] bucketNames, string[] filterIds = null, string[] filterRegions = null, string[] filterResourceArns = null, string[] filterSources = null, string[] filterTimestamps = null, string[] filterVersions = null, string[] detailType = null, string[] account = null)
        {
            detailType ??= new string[] { "AWS API Call via CloudTrail" };

            return new EventPattern
            {
                Account = account,
                Detail = new Dictionary<string, object>
                {
                    { "eventSource", new string[] { "s3.amazonaws.com" } },
                    { "eventName", S3Events },
                    { "requestParameters",
                        new Dictionary<string, object>
                        {
                            { "bucketName", bucketNames }
                        }
                    }
                },
                DetailType = detailType,
                Id = filterIds,
                Region = filterRegions,
                Resources = filterResourceArns,
                Source = filterSources,
                Time = filterTimestamps,
                Version = filterVersions
            };
        }
    }
}
