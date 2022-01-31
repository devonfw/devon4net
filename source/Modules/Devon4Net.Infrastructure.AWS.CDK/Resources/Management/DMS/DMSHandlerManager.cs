using Amazon.CDK.AWS.DMS;
using Amazon.CDK;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Management
{
    public partial class AwsCdkHandlerManager
    {
        public CfnEndpoint CreateDmsEndpoint(string identification, string endpointType, string engineName, string databaseHost, double databasePort, string databaseName, string databaseUsername, string databasePassword, string endpointName = null) //NOSONAR number of params
        {
            return HandlerResources.AwsCdkDmsHandler.CreateEndpoint(identification, endpointType, engineName, databaseHost, databasePort, databaseName, databaseUsername, databasePassword, endpointName);
        }

        public CfnReplicationSubnetGroup CreateDmsReplicationSubnetGroup(string identification, string replicationSubnetGroupName, string replicationSubnetGroupDescription, string[] subnetIds, ICfnTag[] tags = null)
        {
            return HandlerResources.AwsCdkDmsHandler.CreateReplicationSubnetGroup(identification, replicationSubnetGroupName, replicationSubnetGroupDescription, subnetIds, tags);
        }

        public CfnReplicationInstance CreateDmsReplicationInstance(string identification, string replicationInstanceName,string replicationInstanceClass, string replicationSubnetGroupName, double? allocatedStorage = null, bool? allowMajorVersionUpgrade = null,bool? autoMinorVersionUpgrade = null, string availabilityZone = null, string engineVersion = null, string kmsKeyId = null, bool? multiAz = null, string preferredMaintenanceWindow = null, bool? publiclyAccessible = null, string resourceIdentifier = null, ICfnTag[] tags = null, string[] vpcSecurityGroupIds = null) //NOSONAR number of params
        {
            return HandlerResources.AwsCdkDmsHandler.CreateReplicationInstance(identification, replicationInstanceName, replicationInstanceClass, replicationSubnetGroupName, allocatedStorage, allowMajorVersionUpgrade, autoMinorVersionUpgrade, availabilityZone, engineVersion, kmsKeyId, multiAz, preferredMaintenanceWindow, publiclyAccessible, resourceIdentifier, tags, vpcSecurityGroupIds);
        }

        public CfnReplicationTask CreateDmsMigrationTasks(string identification, string migrationTaskName, string migrationType, string replicationInstanceArn, string sourceEndpointArn, string targetEndpointArn, string tableMappings, string cdcStartPosition = null, double? cdcStartTime = null, string cdcStopPosition = null, string replicationTaskSettings = null, string resourceIdentifier = null, ICfnTag[] tags = null, string taskData = null ) //NOSONAR number of params
        {
            return HandlerResources.AwsCdkDmsHandler.CreateMigrationTasks(identification, migrationTaskName, migrationType, replicationInstanceArn, sourceEndpointArn, targetEndpointArn, tableMappings, cdcStartPosition, cdcStartTime, cdcStopPosition, replicationTaskSettings, resourceIdentifier, tags, taskData);
        }
    }
}
