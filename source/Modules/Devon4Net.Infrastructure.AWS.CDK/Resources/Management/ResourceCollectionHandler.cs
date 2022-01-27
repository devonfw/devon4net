using ADC.PostNL.BuildingBlocks.AWSCDK.Handlers;
using Amazon.CDK;
using Constructs;
using Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers;
using Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.AutoScalingGroup;
using Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.Database;
using Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.DMS;
using Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.ECR;
using Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.ECS;
using Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.ELB;
using Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.EventBridge;
using Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.Kms;
using Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.Lambda;
using Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.NetworkLoadBalancer;
using Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.orig;
using Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.ParameterStore;
using Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.PolicyDocuments;
using Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.S3;
using Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.Secrets;
using Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.SecurityGroupHandler;
using Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.VPC;
using Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.WAF;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Management
{
    public class ResourceCollectionHandler
    {
        private Construct Scope { get; }
        private string ApplicationName { get; }
        private string EnvironmentName { get; }
        private IEnvironment EnvironmentProperties { get; }

        #region Handlers
        public AwsCdkVpcHandler AwsCdkVpcHandler { get; set; }
        public AwsCdkDynamoDBHandler AwsCdkDynamoDBHandler { get; set; }
        public AwsCdkRoleHandler AwsCdkRoleHandler { get; set; }
        public AwsCdkPolicyDocumentHandler AwsCdkPolicyDocumentHandler { get; set; }
        public AwsCdkDatabaseHandler AwsCdkDatabaseHandler { get; set; }
        public AwsCdkSecretHandler AwsCdkSecretHandler { get; set; }
        public AwsCdkKmsHandler AwsCdkKmsHandler { get; set; }
        public AwsSecurityGroupHandler AwsSecurityGroupHandler { get; set; }
        public AwsCdkS3Handler AwsCdkS3Handler { get; set; }
        public AwsCdkLambdaHandler AwsCdkLambdaHandler { get; set; }
        public AwsCdkEcrHandler AwsCdkECRHandler { get; set; }
        public AwsCdkEventBridgeHandler AwsCdkEventBridgeHandler { get; set; }
        public AwsCdkApiGatewayHandler AwsCdkApiGatewayHandler { get; set; }
        public AwsCdkCodeBuildHandler AwsCdkCodeBuildHandler { get; set; }
        public AwsCdkSsmParameterStoreHandler AwsCdkSsmParameterStoreHandler { get; set; }
        public AwsCdkElbHandler AwsCdkElbHandler { get; set; }
        public AwsCdkNetworkLoadBalancerHandler AwsCdkNetworkLoadBalancerHandler { get; set; }
        public AwsCdkAutoScalingGroupHandler AwsCdkAutoScalingGroupHandler { get; set;}
        public AwsCdkEcsHandler AwsCdkEcsHandler { get; set; }
        public AwsCdkPipelineHandler AwsCdkPipelineHandler { get; set; }
        public AwsCdkDmsHandler AwsCdkDmsHandler { get; set; }
        public AwsCdkWafHandler AwsCdkWafHandler { get; set; }
        #endregion

        public ResourceCollectionHandler(Construct scope, string applicationName, string environmentName, IStackProps stackProps)
        {
            Scope = scope;
            ApplicationName = applicationName;
            EnvironmentName = environmentName;
            EnvironmentProperties = stackProps?.Env;

            InitializeHandlers();
        }

        private void InitializeHandlers()
        {
            AwsCdkVpcHandler = new AwsCdkVpcHandler(Scope,ApplicationName,EnvironmentName, EnvironmentProperties.Region);
            AwsCdkDynamoDBHandler = new AwsCdkDynamoDBHandler(Scope,ApplicationName,EnvironmentName, EnvironmentProperties.Region);
            AwsCdkRoleHandler = new AwsCdkRoleHandler(Scope,ApplicationName,EnvironmentName, EnvironmentProperties.Region);
            AwsCdkPolicyDocumentHandler = new AwsCdkPolicyDocumentHandler(Scope, ApplicationName, EnvironmentName, EnvironmentProperties.Region);
            AwsCdkKmsHandler = new AwsCdkKmsHandler(Scope, ApplicationName, EnvironmentName, EnvironmentProperties.Region);
            AwsCdkSecretHandler = new AwsCdkSecretHandler(Scope, ApplicationName, EnvironmentName, AwsCdkKmsHandler, EnvironmentProperties.Region, EnvironmentProperties.Account);
            AwsSecurityGroupHandler = new AwsSecurityGroupHandler(Scope, ApplicationName, EnvironmentName, AwsCdkVpcHandler, EnvironmentProperties.Region);
            AwsCdkDatabaseHandler = new AwsCdkDatabaseHandler(Scope, ApplicationName, EnvironmentName, AwsSecurityGroupHandler, AwsCdkVpcHandler, AwsCdkSecretHandler, EnvironmentProperties.Region);
            AwsCdkS3Handler = new AwsCdkS3Handler(Scope, ApplicationName, EnvironmentName, EnvironmentProperties.Region);
            AwsCdkLambdaHandler = new AwsCdkLambdaHandler(Scope, ApplicationName, EnvironmentName, EnvironmentProperties.Region);
            AwsCdkECRHandler = new AwsCdkEcrHandler(Scope, ApplicationName, EnvironmentName, EnvironmentProperties.Region);
            AwsCdkEventBridgeHandler = new AwsCdkEventBridgeHandler(Scope, ApplicationName, EnvironmentName, EnvironmentProperties.Region);
            AwsCdkApiGatewayHandler = new AwsCdkApiGatewayHandler(Scope, ApplicationName, EnvironmentName, EnvironmentProperties.Region);
            AwsCdkCodeBuildHandler = new AwsCdkCodeBuildHandler(Scope, ApplicationName, EnvironmentName, EnvironmentProperties.Region);
            AwsCdkSsmParameterStoreHandler = new AwsCdkSsmParameterStoreHandler(Scope, ApplicationName, EnvironmentName, EnvironmentProperties.Region);
            AwsCdkNetworkLoadBalancerHandler = new AwsCdkNetworkLoadBalancerHandler(Scope, ApplicationName, EnvironmentName, EnvironmentProperties.Region);
            AwsCdkElbHandler = new AwsCdkElbHandler(Scope, ApplicationName, EnvironmentName, EnvironmentProperties.Region);
            AwsCdkAutoScalingGroupHandler = new AwsCdkAutoScalingGroupHandler(Scope, ApplicationName, EnvironmentName, EnvironmentProperties.Region);
            AwsCdkEcsHandler = new AwsCdkEcsHandler(Scope, ApplicationName, EnvironmentName, EnvironmentProperties.Region);
            AwsCdkPipelineHandler = new AwsCdkPipelineHandler(Scope, ApplicationName, EnvironmentName, EnvironmentProperties.Region);
            AwsCdkDmsHandler = new AwsCdkDmsHandler(Scope, ApplicationName, EnvironmentName, EnvironmentProperties.Region);
            AwsCdkWafHandler = new AwsCdkWafHandler(Scope, ApplicationName, EnvironmentName, EnvironmentProperties.Region);
        }
    }
}
