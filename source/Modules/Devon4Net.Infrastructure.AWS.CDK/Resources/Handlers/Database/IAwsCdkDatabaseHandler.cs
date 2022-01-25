using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.RDS;
using Amazon.CDK.AWS.SecretsManager;
using Amazon.JSII.Runtime.Deputy;
using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.Database
{
    public interface IAwsCdkDatabaseHandler
    {
        ParameterGroup CreateClusterParameterGroup(string parameterGroupIdentification, IEngine databaseEngineVersion, IRole[] roles);
        IDatabaseInstance CreateDatabase(DeputyBase databaseEngineVersion, string identification, string databaseName, double? port, string userName, ISecret passwordSecret, StorageType storageType, InstanceClass instanceClass, InstanceSize instanceSize, IVpc vpc, ISecurityGroup securityGroup, ISubnetGroup subnetGroup, IParameterGroup parameterGroup = null, double? allocatedStorageGb = 5, RemovalPolicy removalPolicy = RemovalPolicy.DESTROY, bool deleteAutomatedBackups = false, int backupRetentionDays = 1, bool deletionProtection = false, string[] logTypes = null, bool? storageEncrypted = null, bool? enableIamAuthentication = false, Duration enhancedMonitoringInterval = null, bool multiAZEnabled = true, bool autoMinorVersionUpgrade = false);
        IDatabaseInstance CreateDatabase(DeputyBase databaseEngineVersion, string identification, string databaseName, double? port, string userName, string secretName, StorageType storageType, InstanceClass instanceClass, InstanceSize instanceSize, IVpc vpc, string securityId, string securityGroupId, string parameterGroupId = null, IRole[] roles = null, double? allocatedStorageGb = 5, RemovalPolicy removalPolicy = RemovalPolicy.DESTROY, bool deleteAutomatedBackups = false, int backupRetentionDays = 1, bool deletionProtection = false, SubnetType subnetType = default, string defaultSubnetDomainSeparator = ",", string subnets = "", bool multiAZEnabled = true, bool autoMinorVersionUpgrade = false, bool? storageEncrypted = true);
        IDatabaseCluster CreateDatabaseCluster(DeputyBase databaseEngineVersion, string identification, string clusterIdentifier, string instanceIdentifierBase, string databaseName, double? port, double? instances, string userName, string secretName, IVpc vpc, InstanceClass instanceClass, InstanceSize instanceSize, string securityId, string securityGroupId, string parameterGroupId = null, IRole[] roles = null, bool storageEncrypted = true, SubnetType subnetType = default, string defaultSubnetDomainSeparator = ",", string subnets = "", RemovalPolicy removalPolicy = RemovalPolicy.DESTROY, int backupRetentionDays = 1, bool deletionProtection = false);
        IDatabaseInstance CreateDatabaseSqlServer(DeputyBase databaseEngineVersion, string identification, string databaseName, string userName, string password, StorageType storageType, InstanceClass instanceClass, string instanceSize, IVpc vpc, ISecurityGroup security, string securityGroupId, string parameterGroupId = null, IRole[] roles = null, double? allocatedStorageGb = 5, RemovalPolicy removalPolicy = RemovalPolicy.DESTROY, bool deleteAutomatedBackups = false, int backupRetentionDays = 1, bool deletionProtection = false, SubnetType subnetType = default, string defaultSubnetDomainSeparator = ",", string subnets = "", bool multiAZEnabled = true, bool autoMinorVersionUpgrade = false, bool? storageEncrypted = true, string licenseOption = "LICENSE_INCLUDED", string edition = "ex");
        IParameterGroup CreateParameterGroup(DeputyBase databaseEngineVersion, string identification, string parameterGroupName, IDictionary<string, string> parameters = null);
        IParameterGroup LocateParameterGroupByName(string identification, string parameterGroupName);
        ISubnet LocateSubnetById(string identification, string subnetId);
        ISubnetGroup LocateSubnetGroupByName(string identification, string subnetGroupName);
    }
}