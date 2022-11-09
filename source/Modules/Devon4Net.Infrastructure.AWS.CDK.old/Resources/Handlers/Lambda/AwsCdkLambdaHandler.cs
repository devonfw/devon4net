using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.Events;
using Amazon.CDK.AWS.Events.Targets;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.Logs;
using Amazon.CDK.AWS.Logs.Destinations;
using Amazon.CDK.AWS.S3;
using Constructs;
using System.Collections.Generic;
using LogGroupProps = Amazon.CDK.AWS.Logs.LogGroupProps;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.Lambda
{
    public class AwsCdkLambdaHandler : AwsCdkBaseHandler, IAwsCdkLambdaHandler
    {
        public AwsCdkLambdaHandler(Construct scope, string applicationName, string environmentName, string region) : base(scope, applicationName, environmentName, region)
        {
        }

        public IFunction Create(string id, IFunctionProps lambdaProperties)
        {
            return new Function(Scope, id, lambdaProperties);
        }

        public IFunctionProps CreateLambdaProperties(IBucket codeBucket, string codeFileName, string lambdaHandler, IRole lambdaRole, Runtime runtime, string lambdaName, IVpc vpc, ISecurityGroup securityGroup, ISubnet[] subnets, double memorySize = 512, int timeOutSeconds = 30, IDictionary<string, string> environmentVariables = null)
        {
            return new FunctionProps
            {
                Code = Code.FromBucket(codeBucket, codeFileName),
                Handler = lambdaHandler,
                Runtime = runtime,
                MemorySize = memorySize,
                Timeout = Duration.Seconds(timeOutSeconds),
                FunctionName = lambdaName,
                Role = lambdaRole,
                Vpc = vpc,
                SecurityGroups = new ISecurityGroup[]
                {
                    securityGroup
                },
                VpcSubnets = new SubnetSelection
                {
                    Subnets = subnets
                },
                Environment = environmentVariables
            };
        }

        public IFunctionProps CreateLambdaProperties(string codeFilePath, string lambdaHandler, IRole lambdaRole, Runtime runtime, string lambdaName, IVpc vpc, ISecurityGroup securityGroup, ISubnet[] subnets, double memorySize = 512, int timeOutSeconds = 30, IDictionary<string, string> environmentVariables = null)
        {
            return new FunctionProps
            {
                Code = Code.FromAsset(codeFilePath),
                Handler = lambdaHandler,
                Runtime = runtime,
                MemorySize = memorySize,
                Timeout = Duration.Seconds(timeOutSeconds),
                FunctionName = lambdaName,
                Role = lambdaRole,
                Vpc = vpc,
                SecurityGroups = new ISecurityGroup[]
                {
                    securityGroup
                },
                VpcSubnets = new SubnetSelection
                {
                    Subnets = subnets
                },
                Environment = environmentVariables
            };
        }

        public IRule CreateRuleForLambda(string id, string ruleName, string hour, string minute, IFunction targetFunction, bool enabled = true)
        {
            return new Rule(Scope, id, new RuleProps
            {
                Enabled = enabled,
                RuleName = ruleName,
                Schedule = Schedule.Cron(new CronOptions
                {
                    Hour = hour,
                    Minute = minute
                }),
                Targets = new IRuleTarget[]
                {
                    new LambdaFunction(targetFunction)
                }
            });
        }

        public ILogGroup CreateLogGroup(string id, string lambdaName, RetentionDays retentionTime)
        {
            return new LogGroup(Scope, id, new LogGroupProps
            {
                LogGroupName = $"/aws/lambda/{lambdaName}",
                RemovalPolicy = RemovalPolicy.DESTROY,
                Retention = retentionTime
            });
        }

        public IFunction AddPermissionToLambda(string permissionId, IFunction lambdaFunction, string principal, string action)
        {
            lambdaFunction.AddPermission(permissionId, new Permission
            {
                Action = action,
                Principal = new ServicePrincipal(principal)
            });

            return lambdaFunction;
        }

        public IFunction AddLogGroupTriggerToLambda(string id, IFunction lambdaFunction, ILogGroup logGroup)
        {
            logGroup.AddSubscriptionFilter(id, new SubscriptionFilterOptions
            {
                Destination = new LambdaDestination(lambdaFunction),
                FilterPattern = FilterPattern.AllEvents()
            });

            return lambdaFunction;
        }
    }
}
