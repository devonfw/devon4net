using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.RDS;
using Amazon.CDK.AWS.SecretsManager;
using Amazon.CDK;
using System;
using System.Collections.Generic;
using System.Text;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Management
{
    public partial class AwsCdkHandlerManager
    {
        #region RDS

        public IDatabaseCluster AddDatabase(AuroraMysqlEngineVersion databaseEngineVersion, string identification, string clusterIdentifier, string instanceIdentifierBase, string databaseName, double? port, double? instances, string userName, string secretName, IVpc vpc, InstanceClass instanceClass, InstanceSize instanceSize, string securityId, string securityGroupId, string parameterGroupId = null, IRole[] roles = null, bool storageEncrypted = true, SubnetType subnetType = SubnetType.PRIVATE_ISOLATED, string subnets = "", RemovalPolicy removalPolicy = RemovalPolicy.DESTROY, int backupRetentionDays = 1, bool deletionProtection = false, string defaultSubnetDomainSeparator = ",")
        {
            return HandlerResources.AwsCdkDatabaseHandler.CreateDatabaseCluster(databaseEngineVersion, identification, clusterIdentifier,
                instanceIdentifierBase, databaseName, port, instances, userName, secretName, vpc, instanceClass,
                instanceSize, securityId, securityGroupId, parameterGroupId, roles, storageEncrypted, subnetType, defaultSubnetDomainSeparator, subnets, removalPolicy, backupRetentionDays);
        }

        public IDatabaseCluster AddDatabase(AuroraPostgresEngineVersion databaseEngineVersion, string identification, string clusterIdentifier, string instanceIdentifierBase, string databaseName, double? port, double? instances, string userName, string secretName, IVpc vpc, InstanceClass instanceClass, InstanceSize instanceSize, string securityId, string securityGroupId, string parameterGroupId = null, IRole[] roles = null, bool storageEncrypted = true, SubnetType subnetType = SubnetType.PRIVATE_ISOLATED, string subnets = "", RemovalPolicy removalPolicy = RemovalPolicy.DESTROY, int backupRetentionDays = 1, bool deletionProtection = false, string defaultSubnetDomainSeparator = ",")
        {
            return HandlerResources.AwsCdkDatabaseHandler.CreateDatabaseCluster(databaseEngineVersion, identification, clusterIdentifier,
                instanceIdentifierBase, databaseName, port, instances, userName, secretName, vpc, instanceClass,
                instanceSize, securityId, securityGroupId, parameterGroupId, roles, storageEncrypted, subnetType, defaultSubnetDomainSeparator, subnets, removalPolicy, backupRetentionDays);
        }

        public IDatabaseInstance AddDatabase(SqlServerEngineVersion databaseEngineVersion, string identification, string databaseName, string userName, string password, StorageType storageType, InstanceClass instanceClass, string instanceSize, IVpc vpc, ISecurityGroup securityGroup, string securityGroupName, string parameterGroupId = null, IRole[] roles = null, double? allocatedStorageGb = 5, RemovalPolicy removalPolicy = RemovalPolicy.DESTROY, bool deleteAutomatedBackups = false, int backupRetentionDays = 1, bool deletionProtection = false, SubnetType subnetType = SubnetType.PUBLIC, bool allowGroupAllOutbound = true, string defaultSubnetDomainSeparator = ",", bool multiAZEnabled = true, bool? storageEncrypted = true, string licenseOption = "LICENSE_INCLUDED", string edition = "ex")
        {
            return HandlerResources.AwsCdkDatabaseHandler.CreateDatabaseSqlServer(databaseEngineVersion, identification, databaseName, userName, password, storageType,
                instanceClass, instanceSize, vpc, securityGroup, identification, parameterGroupId, roles, allocatedStorageGb, removalPolicy, deleteAutomatedBackups,
                backupRetentionDays, deletionProtection, subnetType, defaultSubnetDomainSeparator, storageEncrypted: storageEncrypted, licenseOption: licenseOption, edition: edition);


        }

        public IDatabaseInstance AddDatabase(MysqlEngineVersion databaseEngineVersion, string identification, string databaseName, double? port, string userName, string password, StorageType storageType, InstanceClass instanceClass, InstanceSize instanceSize, IVpc vpc, string securityGroupId, string securityGroupName, string parameterGroupId = null, IRole[] roles = null, double? allocatedStorageGb = 5, RemovalPolicy removalPolicy = RemovalPolicy.DESTROY, bool deleteAutomatedBackups = false, int backupRetentionDays = 1, bool deletionProtection = false, SubnetType subnetType = SubnetType.PUBLIC, bool allowGroupAllOutbound = true, string defaultSubnetDomainSeparator = ",")
        {
            return HandlerResources.AwsCdkDatabaseHandler.CreateDatabase(databaseEngineVersion, identification, databaseName, port, userName, password, storageType,
                instanceClass, instanceSize, vpc, securityGroupId, securityGroupName, parameterGroupId, roles, allocatedStorageGb, removalPolicy, deleteAutomatedBackups,
                backupRetentionDays, deletionProtection, subnetType, defaultSubnetDomainSeparator);
        }

        public IDatabaseInstance AddDatabase(MysqlEngineVersion databaseEngineVersion, string identification, string databaseName, double? port, string userName, ISecret passwordSecret, StorageType storageType, InstanceClass instanceClass, InstanceSize instanceSize, IVpc vpc, ISecurityGroup securityGroup, ISubnetGroup subnetGroup, IParameterGroup parameterGroup = null, double? allocatedStorageGb = 5, RemovalPolicy removalPolicy = RemovalPolicy.DESTROY, bool deleteAutomatedBackups = false, int backupRetentionDays = 1, bool deletionProtection = false, string[] logTypes = null, bool? storageEncrypted = null, bool? enableIamAuthentication = null, Duration enhancedMonitoringInterval = null, bool multiAZEnabled = true, bool autoMinorVersionUpgrade = false)
        {
            return HandlerResources.AwsCdkDatabaseHandler.CreateDatabase(databaseEngineVersion, identification, databaseName, port, userName, passwordSecret, storageType,
                instanceClass, instanceSize, vpc, securityGroup, subnetGroup, parameterGroup, allocatedStorageGb, removalPolicy, deleteAutomatedBackups,
                backupRetentionDays, deletionProtection, logTypes, storageEncrypted, enableIamAuthentication, enhancedMonitoringInterval, multiAZEnabled, autoMinorVersionUpgrade);
        }

        public IDatabaseInstance AddDatabase(OracleEngineVersion databaseEngineVersion, string identification, string databaseName, double? port, string userName, string password, StorageType storageType, InstanceClass instanceClass, InstanceSize instanceSize, IVpc vpc, string securityGroupId, string securityGroupName, string parameterGroupId = null, IRole[] roles = null, double? allocatedStorageGb = 5, RemovalPolicy removalPolicy = RemovalPolicy.DESTROY, bool deleteAutomatedBackups = false, int backupRetentionDays = 1, bool deletionProtection = false, SubnetType subnetType = SubnetType.PUBLIC, bool allowGroupAllOutbound = true, string defaultSubnetDomainSeparator = ",")
        {
            return HandlerResources.AwsCdkDatabaseHandler.CreateDatabase(databaseEngineVersion, identification, databaseName, port, userName, password, storageType,
                instanceClass, instanceSize, vpc, securityGroupId, securityGroupName, parameterGroupId, roles, allocatedStorageGb, removalPolicy, deleteAutomatedBackups,
                backupRetentionDays, deletionProtection, subnetType, defaultSubnetDomainSeparator);
        }

        public IDatabaseInstance AddDatabase(MariaDbEngineVersion databaseEngineVersion, string identification, string databaseName, double? port, string userName, string password, StorageType storageType, InstanceClass instanceClass, InstanceSize instanceSize, IVpc vpc, string securityGroupId, string securityGroupName, string parameterGroupId = null, IRole[] roles = null, double? allocatedStorageGb = 5, RemovalPolicy removalPolicy = RemovalPolicy.DESTROY, bool deleteAutomatedBackups = false, int backupRetentionDays = 1, bool deletionProtection = false, SubnetType subnetType = SubnetType.PUBLIC, bool allowGroupAllOutbound = true, string defaultSubnetDomainSeparator = ",")
        {
            return HandlerResources.AwsCdkDatabaseHandler.CreateDatabase(databaseEngineVersion, identification, databaseName, port, userName, password, storageType,
                instanceClass, instanceSize, vpc, securityGroupId, securityGroupName, parameterGroupId, roles, allocatedStorageGb, removalPolicy, deleteAutomatedBackups,
                backupRetentionDays, deletionProtection, subnetType, defaultSubnetDomainSeparator);
        }

        public IDatabaseInstance AddDatabase(PostgresEngineVersion databaseEngineVersion, string identification, string databaseName, double? port, string userName, string password, StorageType storageType, InstanceClass instanceClass, InstanceSize instanceSize, IVpc vpc, string securityGroupId, string securityGroupName, string parameterGroupId = null, IRole[] roles = null, double? allocatedStorageGb = 5, RemovalPolicy removalPolicy = RemovalPolicy.DESTROY, bool deleteAutomatedBackups = false, int backupRetentionDays = 1, bool deletionProtection = false, SubnetType subnetType = SubnetType.PUBLIC, bool allowGroupAllOutbound = true, string defaultSubnetDomainSeparator = ",")
        {
            return HandlerResources.AwsCdkDatabaseHandler.CreateDatabase(databaseEngineVersion, identification, databaseName, port, userName, password, storageType,
                instanceClass, instanceSize, vpc, securityGroupId, securityGroupName, parameterGroupId, roles, allocatedStorageGb, removalPolicy, deleteAutomatedBackups,
                backupRetentionDays, deletionProtection, subnetType, defaultSubnetDomainSeparator);
        }

        public ISubnetGroup LocateSubnetGroupByName(string identification, string subnetGroupName)
        {
            return HandlerResources.AwsCdkDatabaseHandler.LocateSubnetGroupByName(identification, subnetGroupName);
        }

        public ISubnet LocateSubnetById(string identification, string subnetId)
        {
            return HandlerResources.AwsCdkDatabaseHandler.LocateSubnetById(identification, subnetId);
        }

        public IParameterGroup LocateParameterGroupByName(string identification, string parameterGroupName)
        {
            return HandlerResources.AwsCdkDatabaseHandler.LocateParameterGroupByName(identification, parameterGroupName);
        }

        public IParameterGroup CreateParameterGroup(MysqlEngineVersion databaseEngineVersion, string identification, string parameterGroupName, IDictionary<string, string> parameters = null)
        {
            return HandlerResources.AwsCdkDatabaseHandler.CreateParameterGroup(databaseEngineVersion, identification, parameterGroupName, parameters);
        }

        #endregion
    }
}
