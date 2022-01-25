using Amazon.CDK;
using Devon4Net.Infrastructure.AWS.CDK.Options.Global;
using Devon4Net.Infrastructure.AWS.CDK.Resources.Management;

namespace Devon4Net.Infrastructure.AWS.CDK.Stack
{
    public partial class ProvisionStack
    {
        private CdkOptions CdkOptions { get; }
        private App App { get; }
        private AwsCdkHandlerManager AwsCdkHandler { get; }
        private StackResources StackResources { get; }

        public ProvisionStack(string AwsAccount, string AwsRegion, CdkOptions cdkOptions)
        {
            CdkOptions = cdkOptions;
            App = new App();
            StackResources = new StackResources();
            AwsCdkHandler = new AwsCdkHandlerManager(App, CdkOptions.ProvisionStack.Id, CdkOptions.ProvisionStack.ApplicationName, CdkOptions.ProvisionStack.EnvironmentName, new StackProps { Env = new Amazon.CDK.Environment { Account = AwsAccount, Region = AwsRegion } });
        }

        public void Process()
        {
            CreatePolicyDocuments();
            CreateOrLocateVpcs();
            LocateSubnetGroups();
            LocateSubnets();
            CreateSecurityGroups();
            CreateRoles();
            CreateOrLocateS3Buckets();
            CreateLambdas();
            CreateS3Events();
            CreateLogGroups();
            AddLambdaPolicies();
            CreateEcrRepositories();
            CreateRules();
            CreateApiGateways();
            CreateCodeBuildProjects();
            CreateDatabaseParameterGroups();
            CreateDatabases();
            CreateSecrets();
            CreateSsmParameters();
            CreateNetworkTargetGroup();
            CreateEcsAutoScalingGroups();
            CreateEcsTaskDefinitions();
            CreateEcsClusters();
            CreateAsgCapacityProviders();
            CreateEcsService();
            CreatePipelines();
            CreateNetworkLoadBalancers();
            CreateDms();
            CreateDynamoDB();
            CreateWaf();

            App.Synth();
        }
    }
}
