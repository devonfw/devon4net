using System;
using System.Collections.Generic;
using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.KMS;
using Amazon.CDK.AWS.RDS;
using Amazon.CDK.AWS.S3;
using Amazon.CDK.AWS.SecretsManager;
using Devon4Net.Infrastructure.AWS.CDK.Interfaces;

namespace Devon4Net.Infrastructure.AWS.CDK.Handlers
{
    public class AwsCdkHandler : Stack, IAwsCdkHandler
    {
        protected string ApplicationName { get; set; }
        protected string EnvironmentName { get; set; }

        private AwsCdkVpcHandler AwsCdkVpcHandler { get; }
        private AwsCdkS3Handler AwsCdkS3Handler { get; }
        private AwsCdkDatabaseHandler AwsCdkDatabaseHandler { get; }
        private IAwsSecurityGroupHandler AwsSecurityGroupHandler { get; }
        private AwsCdkSecretHandler AwsCdkSecretHandler { get; }
        private AwsCdkKmsHandler AwsCdkKmsHandler { get; }

        private IEnvironment EnvironmentProperties { get; }

        public AwsCdkHandler(Construct scope, string id, string applicationName, string environmentName, IStackProps props = null) : base(scope, id, props)
        {
            if (string.IsNullOrEmpty(applicationName) || string.IsNullOrEmpty(environmentName))
            {
                throw new ArgumentException("The application name or the environment name can not be null");
            }

            ApplicationName = applicationName;
            EnvironmentName = environmentName;

            AwsCdkKmsHandler = new AwsCdkKmsHandler(this, ApplicationName, EnvironmentName);
            EnvironmentProperties = props?.Env;
            AwsCdkVpcHandler = new AwsCdkVpcHandler(this, ApplicationName, EnvironmentName);
            AwsCdkS3Handler = new AwsCdkS3Handler(this, ApplicationName, EnvironmentName);
            AwsSecurityGroupHandler = new AwsSecurityGroupHandler(this, ApplicationName, EnvironmentName, AwsCdkVpcHandler);
            AwsCdkSecretHandler = new AwsCdkSecretHandler(this, ApplicationName, EnvironmentName, AwsCdkKmsHandler, EnvironmentProperties?.Region, EnvironmentProperties?.Account);
            AwsCdkDatabaseHandler = new AwsCdkDatabaseHandler(this, ApplicationName, EnvironmentName, AwsSecurityGroupHandler, AwsCdkVpcHandler, AwsCdkSecretHandler);
        }

        #region Vpc

        public IVpc AddVpc(string cidr, double? maxAzs, DefaultInstanceTenancy defaultInstanceTenancy, string vpcIdentification = null, bool enableDnsSupport = true, bool enableDnsHostnames = true, List<ISubnetConfiguration> subnetConfigurations = null, Dictionary<string, string> tags = null)
        {
            return AwsCdkVpcHandler.Create(string.IsNullOrEmpty(vpcIdentification) ? $"{ApplicationName}{EnvironmentName}vpc" : vpcIdentification, cidr, maxAzs, defaultInstanceTenancy, enableDnsSupport, enableDnsHostnames, subnetConfigurations, tags);
        }

        public IVpc LocateVpc(string identification, string vpcId, bool isDefault = true)
        {
            return AwsCdkVpcHandler.Locate(identification, vpcId, isDefault);
        }

        #endregion

        #region SecurityGroup

        public ISecurityGroup AddSecurityGroup(string identification, string securityGroupName, bool allowGroupAllOutbound, IPeer egressPeer, Port egreessPort, IPeer ingressPeer, Port ingressPort, Vpc vpc)
        {
            return AwsSecurityGroupHandler.Create(identification, securityGroupName, allowGroupAllOutbound, vpc, ingressPeer, ingressPort, egressPeer, egreessPort);
        }

        public ISecurityGroup AddSecurityGroup(string identification, string securityGroupName, bool allowGroupAllOutbound, IPeer egressPeer, Port egreessPort, IPeer ingressPeer, Port ingressPort, IVpc vpcId)
        {
            return AwsSecurityGroupHandler.Create(identification, securityGroupName, allowGroupAllOutbound, vpcId, ingressPeer, ingressPort, egressPeer, egreessPort);
        }

        #endregion

        #region Secret

        /// <summary>
        /// Generates a secret with no KMS key
        /// </summary>
        /// <param name="secretName"></param>
        /// <param name="charsToExclude"></param>
        /// <param name="passwordLength"></param>
        /// <returns></returns>
        public ISecret AddSecret(string secretName, string charsToExclude = "^{}\"@/;-+=&\\/", int passwordLength = 16)
        {
            return AwsCdkSecretHandler.Create(secretName, charsToExclude, passwordLength);
        }

        /// <summary>
        /// Creates a secret using an existing KMS key from the existing KMS id
        /// </summary>
        /// <param name="secretName"></param>
        /// <param name="encryptionKeyId"></param>
        /// <param name="charsToExclude"></param>
        /// <param name="passwordLength"></param>
        /// <returns></returns>
        public ISecret AddSecret(string secretName, string encryptionKeyId, string charsToExclude = "^{}\"@/;-+=&\\/", int passwordLength = 16)
        {
            return AwsCdkSecretHandler.Create(secretName, encryptionKeyId, charsToExclude, passwordLength);
        }

        /// <summary>
        /// Creates a secret using an existing KMS key
        /// </summary>
        /// <param name="secretName"></param>
        /// <param name="key"></param>
        /// <param name="charsToExclude"></param>
        /// <param name="passwordLength"></param>
        /// <returns></returns>
        public ISecret AddSecret(string secretName, IKey key, string charsToExclude = "^{}\"@/;-+=&\\/", int passwordLength = 16)
        {
            return AwsCdkSecretHandler.Create(secretName, key, charsToExclude, passwordLength);
        }

        public string GetSecretValue(string secretId, string secretName, string secretManagerSuffix)
        {
            return AwsCdkSecretHandler.GetSecretValue(secretId, secretName, secretManagerSuffix);
        }

        #endregion

        #region S3

        public IBucket AddS3Bucket(string bucketName, int expirationDays, RemovalPolicy removalPolicy = RemovalPolicy.DESTROY, BucketEncryption encryption = BucketEncryption.KMS_MANAGED, string webSiteRedirectHost = "", bool versioned = true)
        {
            return AwsCdkS3Handler.Create(bucketName, expirationDays, removalPolicy, encryption, webSiteRedirectHost, versioned);
        }

        public IBucket LocateBucketByName(string identification, string bucketName)
        {
            return AwsCdkS3Handler.LocateFromName(identification, bucketName);
        }

        public IBucket LocateBucketByArn(string identification, string arn)
        {
            return AwsCdkS3Handler.LocateFromArn(identification, arn);
        }

        #endregion

        #region RDS

        public IDatabaseCluster AddDatabase(AuroraMysqlEngineVersion databaseEngineVersion, string identification, string clusterIdentifier, string instanceIdentifierBase, string databaseName, double? port, double? instances, string userName, string secretName, IVpc vpc, InstanceClass instanceClass, InstanceSize instanceSize, string securityId, string securityGroupId, string parameterGroupId = null, IRole[] roles = null, bool storageEncrypted = true, SubnetType subnetType = SubnetType.PRIVATE, string subnets = "", RemovalPolicy removalPolicy = RemovalPolicy.DESTROY, int backupRetentionDays = 1, bool deletionProtection = false, string defaultSubnetDomainSeparator = ",")
        {
            return AwsCdkDatabaseHandler.CreateDatabaseCluster(databaseEngineVersion, identification, clusterIdentifier,
                instanceIdentifierBase, databaseName, port, instances, userName, secretName, vpc, instanceClass,
                instanceSize, securityId, securityGroupId, parameterGroupId, roles, storageEncrypted, subnetType, defaultSubnetDomainSeparator, subnets, removalPolicy, backupRetentionDays);
        }

        public IDatabaseCluster AddDatabase(AuroraPostgresEngineVersion databaseEngineVersion, string identification, string clusterIdentifier, string instanceIdentifierBase, string databaseName, double? port, double? instances, string userName, string secretName, IVpc vpc, InstanceClass instanceClass, InstanceSize instanceSize, string securityId, string securityGroupId, string parameterGroupId = null, IRole[] roles = null, bool storageEncrypted = true, SubnetType subnetType = SubnetType.PRIVATE, string subnets = "", RemovalPolicy removalPolicy = RemovalPolicy.DESTROY, int backupRetentionDays = 1, bool deletionProtection = false, string defaultSubnetDomainSeparator = ",")
        {
            return AwsCdkDatabaseHandler.CreateDatabaseCluster(databaseEngineVersion, identification, clusterIdentifier,
                instanceIdentifierBase, databaseName, port, instances, userName, secretName, vpc, instanceClass,
                instanceSize, securityId, securityGroupId, parameterGroupId, roles, storageEncrypted, subnetType, defaultSubnetDomainSeparator, subnets, removalPolicy, backupRetentionDays);
        }

        public IDatabaseInstance AddDatabase(SqlServerEngineVersion databaseEngineVersion, string identification, string databaseName, string userName, string password, StorageType storageType, InstanceClass instanceClass, InstanceSize instanceSize, IVpc vpc, string securityGroupId, string securityGroupName, string parameterGroupId = null, IRole[] roles = null, double? allocatedStorageGb = 5, RemovalPolicy removalPolicy = RemovalPolicy.DESTROY, bool deleteAutomatedBackups = false, int backupRetentionDays = 1, bool deletionProtection = false, SubnetType subnetType = SubnetType.PUBLIC, bool allowGroupAllOutbound = true, string defaultSubnetDomainSeparator = ",")
        {
            return AwsCdkDatabaseHandler.CreateDatabase(databaseEngineVersion, identification, databaseName, userName, password, storageType,
                instanceClass, instanceSize, vpc, securityGroupId, securityGroupName, parameterGroupId, roles, allocatedStorageGb, removalPolicy, deleteAutomatedBackups,
                backupRetentionDays, deletionProtection, subnetType, defaultSubnetDomainSeparator);

        }

        public IDatabaseInstance AddDatabase(MysqlEngineVersion databaseEngineVersion, string identification, string databaseName, string userName, string password, StorageType storageType, InstanceClass instanceClass, InstanceSize instanceSize, IVpc vpc, string securityGroupId, string securityGroupName, string parameterGroupId = null, IRole[] roles = null, double? allocatedStorageGb = 5, RemovalPolicy removalPolicy = RemovalPolicy.DESTROY, bool deleteAutomatedBackups = false, int backupRetentionDays = 1, bool deletionProtection = false, SubnetType subnetType = SubnetType.PUBLIC, bool allowGroupAllOutbound = true, string defaultSubnetDomainSeparator = ",")
        {
            return AwsCdkDatabaseHandler.CreateDatabase(databaseEngineVersion, identification, databaseName, userName, password, storageType,
                instanceClass, instanceSize, vpc, securityGroupId, securityGroupName, parameterGroupId, roles, allocatedStorageGb, removalPolicy, deleteAutomatedBackups,
                backupRetentionDays, deletionProtection, subnetType, defaultSubnetDomainSeparator);
        }

        public IDatabaseInstance AddDatabase(OracleEngineVersion databaseEngineVersion, string identification, string databaseName, string userName, string password, StorageType storageType, InstanceClass instanceClass, InstanceSize instanceSize, IVpc vpc, string securityGroupId, string securityGroupName, string parameterGroupId = null, IRole[] roles = null, double? allocatedStorageGb = 5, RemovalPolicy removalPolicy = RemovalPolicy.DESTROY, bool deleteAutomatedBackups = false, int backupRetentionDays = 1, bool deletionProtection = false, SubnetType subnetType = SubnetType.PUBLIC, bool allowGroupAllOutbound = true, string defaultSubnetDomainSeparator = ",")
        {
            return AwsCdkDatabaseHandler.CreateDatabase(databaseEngineVersion, identification, databaseName, userName, password, storageType,
                instanceClass, instanceSize, vpc, securityGroupId, securityGroupName, parameterGroupId, roles, allocatedStorageGb, removalPolicy, deleteAutomatedBackups,
                backupRetentionDays, deletionProtection, subnetType, defaultSubnetDomainSeparator);
        }

        public IDatabaseInstance AddDatabase(MariaDbEngineVersion databaseEngineVersion, string identification, string databaseName, string userName, string password, StorageType storageType, InstanceClass instanceClass, InstanceSize instanceSize, IVpc vpc, string securityGroupId, string securityGroupName, string parameterGroupId = null, IRole[] roles = null, double? allocatedStorageGb = 5, RemovalPolicy removalPolicy = RemovalPolicy.DESTROY, bool deleteAutomatedBackups = false, int backupRetentionDays = 1, bool deletionProtection = false, SubnetType subnetType = SubnetType.PUBLIC, bool allowGroupAllOutbound = true, string defaultSubnetDomainSeparator = ",")
        {
            return AwsCdkDatabaseHandler.CreateDatabase(databaseEngineVersion, identification, databaseName, userName, password, storageType,
                instanceClass, instanceSize, vpc, securityGroupId, securityGroupName, parameterGroupId, roles, allocatedStorageGb, removalPolicy, deleteAutomatedBackups,
                backupRetentionDays, deletionProtection, subnetType, defaultSubnetDomainSeparator);
        }

        public IDatabaseInstance AddDatabase(PostgresEngineVersion databaseEngineVersion, string identification, string databaseName, string userName, string password, StorageType storageType, InstanceClass instanceClass, InstanceSize instanceSize, IVpc vpc, string securityGroupId, string securityGroupName, string parameterGroupId = null, IRole[] roles = null, double? allocatedStorageGb = 5, RemovalPolicy removalPolicy = RemovalPolicy.DESTROY, bool deleteAutomatedBackups = false, int backupRetentionDays = 1, bool deletionProtection = false, SubnetType subnetType = SubnetType.PUBLIC, bool allowGroupAllOutbound = true, string defaultSubnetDomainSeparator = ",")
        {
            return AwsCdkDatabaseHandler.CreateDatabase(databaseEngineVersion, identification, databaseName, userName, password, storageType,
                instanceClass, instanceSize, vpc, securityGroupId, securityGroupName, parameterGroupId, roles, allocatedStorageGb, removalPolicy, deleteAutomatedBackups,
                backupRetentionDays, deletionProtection, subnetType, defaultSubnetDomainSeparator);
        }

        public IBucket LocateBucketByName(string identification, string bucketName)
        {
            throw new NotImplementedException();
        }

        public IBucket LocateBucketByArn(string identification, string arn)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
