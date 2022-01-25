using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.Events;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.Logs;
using Amazon.CDK.AWS.S3;
using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Management
{
    public partial class AwsCdkHandlerManager : ILambdaHandlerManager
    {
        public IFunction AddLambda(string id, IFunctionProps lambdaProperties)
        {
            return HandlerResources.AwsCdkLambdaHandler.Create(id, lambdaProperties);
        }

        public IFunctionProps CreateLambdaProperties(IBucket codeBucket, string codeFileName, string lambdaHandler, IRole lambdaRole, Runtime runtime, string lambdaName, IVpc vpc, ISecurityGroup securityGroup, ISubnet[] subnets, double memorySize = 512, int timeOutSeconds = 30, IDictionary<string, string> environmentVariables = null)
        {
            return HandlerResources.AwsCdkLambdaHandler.CreateLambdaProperties(codeBucket, codeFileName, lambdaHandler, lambdaRole, runtime, lambdaName, vpc, securityGroup, subnets, memorySize, timeOutSeconds, environmentVariables);
        }

        public IFunctionProps CreateLambdaProperties(string codeFilePath, string lambdaHandler, IRole lambdaRole, Runtime runtime, string lambdaName, IVpc vpc, ISecurityGroup securityGroup, ISubnet[] subnets, double memorySize = 512, int timeOutSeconds = 30, IDictionary<string, string> environmentVariables = null)
        {
            return HandlerResources.AwsCdkLambdaHandler.CreateLambdaProperties(codeFilePath, lambdaHandler, lambdaRole, runtime, lambdaName, vpc, securityGroup, subnets, memorySize, timeOutSeconds, environmentVariables);
        }

        public IRule CreateRuleForLambda(string id, string ruleName, string hour, string minute, IFunction targetFunction, bool enabled = true)
        {
            return HandlerResources.AwsCdkLambdaHandler.CreateRuleForLambda(id, ruleName, hour, minute, targetFunction, enabled);
        }

        public ILogGroup CreateLogGroup(string id, string lambdaName, RetentionDays retentionTime)
        {
            return HandlerResources.AwsCdkLambdaHandler.CreateLogGroup(id, lambdaName, retentionTime);
        }

        public IFunction AddLogGroupTriggerToLambda(string id, IFunction lambdaFunction, ILogGroup logGroup)
        {
            return HandlerResources.AwsCdkLambdaHandler.AddLogGroupTriggerToLambda(id, lambdaFunction, logGroup);
        }

        public IFunction AddPermissionToLambda(string permissionId, IFunction lambdaFunction, string principal, string action)
        {
            return HandlerResources.AwsCdkLambdaHandler.AddPermissionToLambda(permissionId, lambdaFunction, principal, action);
        }
    }
}
