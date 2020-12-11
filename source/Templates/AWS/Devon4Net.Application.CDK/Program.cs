using System.Linq;
using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.RDS;
using Devon4Net.Infrastructure.AWS.CDK.Handlers;
using Environment = Amazon.CDK.Environment;

namespace Devon4Net.Application.CDK
{
    public class Program
    {
        private static string AwsAccount { get; set; }
        private static string AwsRegion { get; set; }

        static void Main(string[] args)
        {
            if (args != null && args.Any())
            {   
                AwsAccount = args.Length >= 1 ? args[0] : System.Environment.GetEnvironmentVariable("CDK_DEFAULT_ACCOUNT");
                AwsRegion = args.Length == 2 ? args[1] : System.Environment.GetEnvironmentVariable("CDK_DEFAULT_REGION");
            }

            //Create infra as code application (AWS framework)
            var app = new App();
            
            //Create the infrastructure provision handler (devon4Net Framework)
            var provisionStack = new AwsCdkHandler(app, "Devon4NetCloudStackDemo", "Devon4NetCdkDemo20201201", "development", new StackProps{Env = CreateEnv(AwsAccount, AwsRegion)});
            
            //Provision the infrastructure
            //Create S3 bucket
            provisionStack.AddS3Bucket("Devon4NetBucket", 1);

            //Locate existing VPC
            var vpc = provisionStack.LocateVpc("vpc-12345", "vpc-c274babb");

            //Create single database instance sample
            //provisionStack.AddDatabase(MysqlEngineVersion.VER_8_0_21, "MyDbInstance", "MyDbInstanceName", "defaultUserName", "aws_database_secret", StorageType.GP2, InstanceClass.BURSTABLE2, InstanceSize.MICRO, vpc, "sg-0042c46d08771873e", "sg-0042c46d08771873e");

            //Create cluster database instance sample
            provisionStack.AddDatabase(AuroraMysqlEngineVersion.VER_5_7_12 , "MyDbInstance", "MyClusterId",
                "MyClusterInstanceId",
                "Devon4NetClusterDemo20201201", 3306, 1, "defaultUserName", "aws_database_secret", vpc,
                InstanceClass.BURSTABLE2, InstanceSize.SMALL, "sg-0042c46d08771873e", "sg-0042c46d08771873e",string.Empty, null, true, SubnetType.PUBLIC);

            //Execute provisioning (AWS)
            app.Synth();
        }
        private static  Environment CreateEnv(string account = null, string region = null)
        {
            return new Environment
            {
                Account = string.IsNullOrEmpty(account) ? "YOUR_ACCOUNT_ID" : account,
                Region = string.IsNullOrEmpty(region) ? "eu-west-1" : region
            };
        }
    }
}

