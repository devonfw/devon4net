using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.Events;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.Logs;
using Amazon.CDK.AWS.S3;
using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.Lambda
{
    public interface IAwsCdkLambdaHandler
    {
        IFunction AddLogGroupTriggerToLambda(string id, IFunction lambdaFunction, ILogGroup logGroup);
        IFunction AddPermissionToLambda(string permissionId, IFunction lambdaFunction, string principal, string action);
        IFunction Create(string id, IFunctionProps lambdaProperties);
        IFunctionProps CreateLambdaProperties(IBucket codeBucket, string codeFileName, string lambdaHandler, IRole lambdaRole, Runtime runtime, string lambdaName, IVpc vpc, ISecurityGroup securityGroup, ISubnet[] subnets, double memorySize = 512, int timeOutSeconds = 30, IDictionary<string, string> environmentVariables = null); //NOSONAR number of params
        IFunctionProps CreateLambdaProperties(string codeFilePath, string lambdaHandler, IRole lambdaRole, Runtime runtime, string lambdaName, IVpc vpc, ISecurityGroup securityGroup, ISubnet[] subnets, double memorySize = 512, int timeOutSeconds = 30, IDictionary<string, string> environmentVariables = null); //NOSONAR number of params
        ILogGroup CreateLogGroup(string id, string lambdaName, RetentionDays retentionTime);
        IRule CreateRuleForLambda(string id, string ruleName, string hour, string minute, IFunction targetFunction, bool enabled = true);
    }
}