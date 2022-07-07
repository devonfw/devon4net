using Devon4Net.Infrastructure.AWS.CDK.Options.Resources;
using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AWS.CDK.Options.Global
{
    public class CdkOptions
    {
        public ProvisionStackOptions ProvisionStack { get; set; }
        public List<RoleOptions> Roles { get; set; }
        public List<SecretOptions> Secrets { get; set; }
        public List<VpcOptions> Vpcs { get; set; }
        public List<SubnetOptions> Subnets { get; set; }
        public List<S3BucketOptions> S3Buckets { get; set; }
        public List<DatabaseParameterGroupOptions> DatabaseParameterGroups { get; set; }
        public List<DynamoDBOptions> DynamoDB { get; set; }
        public List<DatabaseOptions> Databases { get; set; }
        public DatabaseMiagrationServiceOptions DatabaseMiagrationService { get; set; }
        public List<LambdaOptions> Lambdas { get; set; }
        public List<LambdaPolicyOptions> LambdaPolicies { get; set; }
        public List<LogGroupOptions> LogGroups { get; set; }
        public List<ApiGatewayOptions> ApiGateways { get; set; }
        public List<PipelineOptions> Pipelines { get; set; }
        public List<SecurityGroupOptions> SecurityGroups { get; set; }
        public List<LambdaRuleOptions> LambdaRules { get; set; }
        public List<CodeBuildOptions> CodeBuildProjects { get; set; }
        public List<SsmParameterOptions> SsmParameters { get; set; }
        public List<EcrRepositoryOptions> EcrRepositories { get; set; }
        public List<EcsTaskDefinitionOptions> EcsTaskDefinitions { get; set; }
        public List<EcsClusterOptions> EcsClusters { get; set; }
        public List<EcsServiceOptions> EcsServices { get; set; }
        public List<Ec2InstanceOptions> Ec2Instances { get; set; }
        public List<AutoScalingGroupOptions> AutoScalingGroups { get; set; }
        public List<NetworkTargetGroupOptions> NetworkTargetGroups { get; set; }
        public List<InlinePolicyOptions> PolicyDocuments { get; set; }
        public List<NetworkLoadBalancerOptions> NetworkLoadBalancers { get; set; }
        public List<AsgCapacityProviderOptions> CapacityProviders { get; set; }
        public List<WafOptions> Wafs { get; set; }
        public List<SnsTopicOptions> SnsTopics { get; set; }
        public List<SnsEmailSubscriptionOptions> SnsEmailSubscriptions { get; set; }
        public CognitoOptions Cognito { get; set; }
    }
}
