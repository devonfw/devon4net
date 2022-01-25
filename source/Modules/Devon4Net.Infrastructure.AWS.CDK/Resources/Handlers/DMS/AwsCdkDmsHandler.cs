using Amazon.CDK;
using Amazon.CDK.AWS.DMS;
using Constructs;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.DMS
{
    public class AwsCdkDmsHandler : AwsCdkBaseHandler, IAwsCdkDmsHandler
    {
        public AwsCdkDmsHandler(Construct scope, string applicationName, string environmentName) : base(scope, applicationName, environmentName)
        {
        }

        public CfnEndpoint CreateEndpoint(string identification, string endpointType, string engineName, string databaseHost, double databasePort, string databaseName, string databaseUsername, string databasePassword, string endpointName = null)
        {
            return new CfnEndpoint(Scope, identification, new CfnEndpointProps
            {
                DatabaseName = databaseName,
                EndpointType = endpointType,
                EngineName = engineName,
                Password = databasePassword,
                Port = databasePort,
                ServerName = databaseHost,
                Username = databaseUsername,
                EndpointIdentifier = endpointName
            });
        }

        public CfnReplicationSubnetGroup CreateReplicationSubnetGroup(
            string identification,
            string replicationSubnetGroupName,
            string replicationSubnetGroupDescription,
            string[] subnetIds,
            ICfnTag[] tags = null
            )
        {
            return new CfnReplicationSubnetGroup(Scope, identification, new CfnReplicationSubnetGroupProps
            {
                ReplicationSubnetGroupDescription = replicationSubnetGroupDescription,
                ReplicationSubnetGroupIdentifier = replicationSubnetGroupName,
                SubnetIds = subnetIds,
                Tags = tags
            });
        }

        public CfnReplicationInstance CreateReplicationInstance(
            string identification,
            string replicationInstanceName,
            string replicationInstanceClass,
            string replicationSubnetGroupName,
            double? allocatedStorage = null,
            bool? allowMajorVersionUpgrade = null,
            bool? autoMinorVersionUpgrade = null,
            string availabilityZone = null,
            string engineVersion = null,
            string kmsKeyId = null,
            bool? multiAz = null,
            string preferredMaintenanceWindow = null,
            bool? publiclyAccessible = null,
            string resourceIdentifier = null,
            ICfnTag[] tags = null,
            string[] vpcSecurityGroupIds = null)
        {
            return new CfnReplicationInstance(Scope, identification, new CfnReplicationInstanceProps
            {
                ReplicationInstanceClass = replicationInstanceClass,
                AllocatedStorage = allocatedStorage,
                AllowMajorVersionUpgrade = allowMajorVersionUpgrade,
                AutoMinorVersionUpgrade = autoMinorVersionUpgrade,
                AvailabilityZone = availabilityZone,
                EngineVersion = engineVersion,
                KmsKeyId = kmsKeyId,
                MultiAz = multiAz,
                PreferredMaintenanceWindow = preferredMaintenanceWindow,
                PubliclyAccessible = publiclyAccessible,
                ReplicationInstanceIdentifier = replicationInstanceName,
                ReplicationSubnetGroupIdentifier = replicationSubnetGroupName,
                ResourceIdentifier = resourceIdentifier,
                Tags = tags,
                VpcSecurityGroupIds = vpcSecurityGroupIds
            });
        }

        public CfnReplicationTask CreateMigrationTasks(
            string identification,
            string migrationTaskName,
            string migrationType,
            string replicationInstanceArn,
            string sourceEndpointArn,
            string targetEndpointArn,
            string tableMappings,
            string cdcStartPosition = null,
            double? cdcStartTime = null,
            string cdcStopPosition = null,
            string replicationTaskSettings = null,
            string resourceIdentifier = null,
            ICfnTag[] tags = null,
            string taskData = null
            )
        {
            return new CfnReplicationTask(Scope, identification, new CfnReplicationTaskProps
            {
                MigrationType = migrationType,
                ReplicationInstanceArn = replicationInstanceArn,
                SourceEndpointArn = sourceEndpointArn,
                TableMappings = tableMappings,
                TargetEndpointArn = targetEndpointArn,
                CdcStartPosition = cdcStartPosition,
                CdcStartTime = cdcStartTime,
                CdcStopPosition = cdcStopPosition,
                ReplicationTaskIdentifier = migrationTaskName,
                ReplicationTaskSettings = replicationTaskSettings,
                ResourceIdentifier = resourceIdentifier,
                Tags = tags,
                TaskData = taskData
            });
        }
    }
}
