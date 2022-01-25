using Amazon.CDK;
using Amazon.CDK.AWS.DMS;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.DMS
{
    public interface IAwsCdkDmsHandler
    {
        CfnEndpoint CreateEndpoint(string identification, string endpointType, string engineName, string databaseHost, double databasePort, string databaseName, string databaseUsername, string databasePassword, string endpointName = null);
        CfnReplicationTask CreateMigrationTasks(string identification, string migrationTaskName, string migrationType, string replicationInstanceArn, string sourceEndpointArn, string targetEndpointArn, string tableMappings, string cdcStartPosition = null, double? cdcStartTime = null, string cdcStopPosition = null, string replicationTaskSettings = null, string resourceIdentifier = null, ICfnTag[] tags = null, string taskData = null);
        CfnReplicationInstance CreateReplicationInstance(string identification, string replicationInstanceName, string replicationInstanceClass, string replicationSubnetGroupName, double? allocatedStorage = null, bool? allowMajorVersionUpgrade = null, bool? autoMinorVersionUpgrade = null, string availabilityZone = null, string engineVersion = null, string kmsKeyId = null, bool? multiAz = null, string preferredMaintenanceWindow = null, bool? publiclyAccessible = null, string resourceIdentifier = null, ICfnTag[] tags = null, string[] vpcSecurityGroupIds = null);
        CfnReplicationSubnetGroup CreateReplicationSubnetGroup(string identification, string replicationSubnetGroupName, string replicationSubnetGroupDescription, string[] subnetIds, ICfnTag[] tags = null);
    }
}