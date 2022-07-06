using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.AutoScaling;
using Amazon.CDK.AWS.CodeBuild;
using Amazon.CDK.AWS.CodePipeline;
using Amazon.CDK.AWS.Cognito;
using Amazon.CDK.AWS.DMS;
using Amazon.CDK.AWS.DynamoDB;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.ECR;
using Amazon.CDK.AWS.ECS;
using Amazon.CDK.AWS.ElasticLoadBalancingV2;
using Amazon.CDK.AWS.Events;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.Logs;
using Amazon.CDK.AWS.RDS;
using Amazon.CDK.AWS.S3;
using Amazon.CDK.AWS.SecretsManager;
using Amazon.CDK.AWS.SNS;
using Amazon.CDK.AWS.SSM;
using Amazon.CDK.AWS.WAFv2;
using System;
using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AWS.CDK.Stack
{
    public class StackResources
    {
        public IDictionary<string, IRole> Roles { get; set; }
        public IDictionary<string, PolicyDocument> PolicyDocuments { get; set; }
        public IDictionary<string, IVpc> Vpcs { get; set; }
        public IDictionary<string, ISubnetGroup> SubnetGroups { get; set; }
        public IDictionary<string, ISubnet> Subnets { get; set; }
        public IDictionary<string, ISecurityGroup> SecurityGroups { get; set; }
        public IDictionary<string, IBucket> Buckets { get; set; }
        public IDictionary<string, IFunction> Lambdas { get; set; }
        public IDictionary<string, ILogGroup> LogGroups { get; set; }
        public IDictionary<string, IRule> LambdaRules { get; set; }
        public IDictionary<string, LambdaRestApi> ApiGateways { get; set; }
        public IDictionary<string, IProject> CodeBuildProjects { get; set; }
        public IDictionary<string, IPipeline> Pipelines { get; set; }
        public IDictionary<string, Artifact_> Artifacts { get; set; }
        public IDictionary<string, IParameterGroup> DatabaseParameterGroups { get; set; }
        public IDictionary<string, IDatabaseInstance> Databases { get; set; }
        public IDictionary<string, ITable> DynamoDBs { get; set; }
        public IDictionary<string, CfnSecret> Secrets { get; set; }
        public IDictionary<string, ISecret> DynamicSecrets { get; set; }
        public IDictionary<string, IParameter> SsmParameters { get; set; }
        public IDictionary<string, TaskDefinition> EcsTaskDefinitions { get; set; }
        public IDictionary<string, INetworkTargetGroup> NetworkTargetGroups { get; set; }
        public IDictionary<string, IRepository> EcrRepositories { get; set; }
        public IDictionary<string, IAutoScalingGroup> AutoScalingGroups { get; set; }
        public IDictionary<string, ICluster> EcsClusters { get; set; }
        public IDictionary<string, IService> EcsServices { get; set; }
        public IDictionary<string, IInstance> Ec2Instances { get; set; }
        public IDictionary<string, INetworkLoadBalancer> NetworkLoadBalancers { get; set; }
        public IDictionary<string, Amazon.CDK.AWS.DMS.CfnEndpoint> DmsEndpoints { get; set; }
        public IDictionary<string, CfnReplicationInstance> DmsReplicationInstances { get; set; }
        public IDictionary<string, CfnReplicationSubnetGroup> DmsReplicationSubnetGroups { get; set; }
        public IDictionary<string, CfnReplicationTask> DmsMigrationTasks { get; set; }
        public IDictionary<string, AsgCapacityProvider> AsgCapacityProviders { get; set; }
        public IDictionary<string, CfnWebACL> WebAcls { get; set; }
        public IDictionary<string, IUserPool> CognitoUserPools { get; set; }
        public Dictionary<string, ITopic> SnsTopics { get; set; }

        public StackResources()
        {
            ApiGateways = new Dictionary<string, LambdaRestApi>();
            Artifacts = new Dictionary<string, Artifact_>();
            AsgCapacityProviders = new Dictionary<string, AsgCapacityProvider>();
            AutoScalingGroups = new Dictionary<string, IAutoScalingGroup>();
            Buckets = new Dictionary<string, IBucket>();
            CodeBuildProjects = new Dictionary<string, IProject>();
            DatabaseParameterGroups = new Dictionary<string, IParameterGroup>();
            Databases = new Dictionary<string, IDatabaseInstance>();
            DmsEndpoints = new Dictionary<string, Amazon.CDK.AWS.DMS.CfnEndpoint>();
            DmsMigrationTasks = new Dictionary<string, CfnReplicationTask>();
            DmsReplicationInstances = new Dictionary<string, CfnReplicationInstance>();
            DmsReplicationSubnetGroups = new Dictionary<string, CfnReplicationSubnetGroup>();
            DynamicSecrets = new Dictionary<string, ISecret>();
            DynamoDBs = new Dictionary<string, ITable>();
            Ec2Instances = new Dictionary<string, IInstance>();
            EcsClusters = new Dictionary<string, ICluster>();
            EcsServices = new Dictionary<string, IService>();
            EcsTaskDefinitions = new Dictionary<string, TaskDefinition>();
            EcrRepositories = new Dictionary<string, IRepository>();
            LambdaRules = new Dictionary<string, IRule>();
            Lambdas = new Dictionary<string, IFunction>();
            LogGroups = new Dictionary<string, ILogGroup>();
            NetworkLoadBalancers = new Dictionary<string, INetworkLoadBalancer>();
            NetworkTargetGroups = new Dictionary<string, INetworkTargetGroup>();
            Pipelines = new Dictionary<string, IPipeline>();
            PolicyDocuments = new Dictionary<string, PolicyDocument>();
            Roles = new Dictionary<string, IRole>();
            Secrets = new Dictionary<string, CfnSecret>();
            SecurityGroups = new Dictionary<string, ISecurityGroup>();
            SsmParameters = new Dictionary<string, IParameter>();
            SubnetGroups = new Dictionary<string, ISubnetGroup>();
            Subnets = new Dictionary<string, ISubnet>();
            Vpcs = new Dictionary<string, IVpc>();
            WebAcls = new Dictionary<string, CfnWebACL>();
            CognitoUserPools = new Dictionary<string, IUserPool>();
            SnsTopics = new Dictionary<string, ITopic>();
        }

        public T Locate<T>(string resourceId, string exceptionMessageIfResourceDoesNotExist, string exceptionMessageIfResourceIsEmpty = null)
        {
            if (string.IsNullOrWhiteSpace(resourceId))
            {
                if (string.IsNullOrWhiteSpace(exceptionMessageIfResourceIsEmpty))
                {
                    return default;
                }

                throw new ArgumentException(exceptionMessageIfResourceIsEmpty);
            }
            else
            {
                var resourceDictionary = LocateDictionary<T>();
                if (resourceDictionary.TryGetValue(resourceId, out var resource))
                {
                    return resource;
                }

                throw new ArgumentException(exceptionMessageIfResourceDoesNotExist);
            }
        }

        private Dictionary<string, T> LocateDictionary<T>()
        {
            var l = new object[]
            {
                Roles,
                Vpcs,
                SubnetGroups,
                Subnets,
                SecurityGroups,
                Buckets,
                Lambdas,
                LambdaRules,
                ApiGateways,
                CodeBuildProjects,
                Pipelines,
                Artifacts,
                Databases,
                Secrets,
                EcrRepositories,
                EcsServices,
                EcsClusters,
                Ec2Instances,
                AutoScalingGroups,
                AsgCapacityProviders,
                PolicyDocuments,
                NetworkTargetGroups,
                SnsTopics,
                CognitoUserPools
            };

            return Array.Find(l, x => x is Dictionary<string, T>) as Dictionary<string, T>;
        }
    }
}
