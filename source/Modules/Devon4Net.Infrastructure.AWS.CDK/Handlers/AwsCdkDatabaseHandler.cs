using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.RDS;
using Amazon.CDK.AWS.SecretsManager;
using Amazon.JSII.Runtime.Deputy;
using Devon4Net.Infrastructure.AWS.CDK.Interfaces;

namespace Devon4Net.Infrastructure.AWS.CDK.Handlers
{
    public class AwsCdkDatabaseHandler : AwsCdkDefaultHandler
    {
        private IAwsSecurityGroupHandler AwsSecurityGroupHandler { get; }
        private AwsCdkVpcHandler AwsCdkVpcHandler { get; }
        private AwsCdkSecretHandler AwsCdkSecretHandler { get; }

        public AwsCdkDatabaseHandler(Construct scope, string applicationName, string environmentName, IAwsSecurityGroupHandler awsSecurityGroupHandler, AwsCdkVpcHandler awsCdkVpcHandler, AwsCdkSecretHandler awsCdkSecretHandler) : base(scope, applicationName, environmentName)
        {
            AwsSecurityGroupHandler = awsSecurityGroupHandler;
            AwsCdkVpcHandler = awsCdkVpcHandler;
            AwsCdkSecretHandler = awsCdkSecretHandler;
        }

        #region AWSExecution

        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseEngineVersion"></param>
        /// <param name="identification"></param>
        /// <param name="clusterIdentifier"></param>
        /// <param name="instanceIdentifierBase"></param>
        /// <param name="databaseName"></param>
        /// <param name="port"></param>
        /// <param name="instances"></param>
        /// <param name="userName"></param>
        /// <param name="secretName"></param>
        /// <param name="vpc"></param>
        /// <param name="instanceClass"></param>
        /// <param name="instanceSize"></param>
        /// <param name="securityId"></param>
        /// <param name="securityGroupId"></param>
        /// <param name="roles">Roles to add to the parameterGroupId. Example access to S3 buckets</param>
        /// <param name="storageEncrypted"></param>
        /// <param name="subnetType">Optional. If there are subnets, will be picked instead of this value</param>
        /// <param name="defaultSubnetDomainSeparator"></param>
        /// <param name="subnets">Optional. Comma separated Id</param>
        /// <param name="removalPolicy"></param>
        /// <param name="backupRetentionDays"></param>
        /// <param name="deletionProtection"></param>
        /// <param name="parameterGroupId"></param>
        /// <returns></returns>
        public IDatabaseCluster CreateDatabaseCluster(DeputyBase databaseEngineVersion, string identification, string clusterIdentifier, string instanceIdentifierBase, string databaseName, double? port, double? instances, string userName, string secretName, IVpc vpc, InstanceClass instanceClass, InstanceSize instanceSize, string securityId, string securityGroupId, string parameterGroupId = null, IRole[] roles = null, bool storageEncrypted = true, SubnetType subnetType= SubnetType.PRIVATE, string defaultSubnetDomainSeparator = ",",  string subnets = "",  RemovalPolicy removalPolicy = RemovalPolicy.DESTROY, int backupRetentionDays = 1, bool deletionProtection = false)
        {
            BasicDatabaseInfra(vpc, secretName, securityId, securityGroupId, subnetType, defaultSubnetDomainSeparator, subnets, out var securityGroup, out var secret, out var subnetSelection);
            var engine = GetClusterEngine(databaseEngineVersion);
            return new DatabaseCluster(Scope, identification, new DatabaseClusterProps
            {
                ClusterIdentifier = clusterIdentifier,
                InstanceIdentifierBase = instanceIdentifierBase,
                Engine = engine,
                RemovalPolicy = removalPolicy,
                DeletionProtection = deletionProtection,
                Port = port,
                InstanceProps = new Amazon.CDK.AWS.RDS.InstanceProps
                {
                    InstanceType = InstanceType.Of(instanceClass, instanceSize),
                    VpcSubnets = subnetSelection,
                    Vpc = vpc,
                    SecurityGroups = new[]
                    {
                        securityGroup
                    }
                },
                StorageEncrypted = storageEncrypted,
                Instances = instances,
                Credentials = Credentials.FromPassword(userName, secret.SecretValue),
                DefaultDatabaseName = databaseName,
                ParameterGroup = CreateClusterParameterGroup(parameterGroupId, engine, roles), 
                Backup = new BackupProps
                {
                    Retention = Duration.Days(backupRetentionDays)
                }
            });
        }

        public IDatabaseInstance CreateDatabase(DeputyBase databaseEngineVersion, string identification, string databaseName, string userName, string secretName, StorageType storageType, InstanceClass instanceClass, InstanceSize instanceSize, IVpc vpc, string securityId, string securityGroupId, string parameterGroupId = null, IRole[] roles = null, double? allocatedStorageGb = 5, RemovalPolicy removalPolicy = RemovalPolicy.DESTROY, bool deleteAutomatedBackups = false, int backupRetentionDays = 1, bool deletionProtection = false, SubnetType subnetType = SubnetType.PRIVATE, string defaultSubnetDomainSeparator = ",", string subnets = "")
        {
            BasicDatabaseInfra(vpc, secretName, securityId, securityGroupId, subnetType, defaultSubnetDomainSeparator, subnets, out var securityGroup, out var secret, out var subnetSelection);
            var engine = GetInstanceEngine(databaseEngineVersion);

            return new DatabaseInstance(Scope, identification, new DatabaseInstanceProps
            {
                Engine = engine,
                RemovalPolicy = removalPolicy,
                DeletionProtection = deletionProtection,
                Credentials = Credentials.FromPassword(userName, secret.SecretValue),
                StorageType = storageType,
                DatabaseName = databaseName,
                VpcSubnets = subnetSelection,
                Vpc = vpc,
                SecurityGroups = new[]
                {
                    securityGroup
                },
                DeleteAutomatedBackups = deleteAutomatedBackups,
                BackupRetention = Duration.Days(backupRetentionDays),
                AllocatedStorage = allocatedStorageGb,
                InstanceType = InstanceType.Of(instanceClass, instanceSize),
                ParameterGroup = CreateClusterParameterGroup(parameterGroupId, engine, roles)
            });
        }

        public ParameterGroup CreateClusterParameterGroup(string parameterGroupIdentification, IEngine databaseEngineVersion, IRole[] roles)
        {
            if (string.IsNullOrEmpty(parameterGroupIdentification)) return null;
            if (roles == null || !roles.Any()) return null;

            return new ParameterGroup(Scope, $"{ApplicationName}{EnvironmentName}{parameterGroupIdentification}",
                new ParameterGroupProps
                {
                    Description = $"{ApplicationName}{EnvironmentName}{parameterGroupIdentification}",
                    Engine = databaseEngineVersion,
                    Parameters = roles.Select(t => new { t.RoleName, t.RoleArn }) .ToDictionary(t => t.RoleName, t => t.RoleArn)
                });
        }

        private void BasicDatabaseInfra(IVpc vpc, string secretName, string securityId, string securityGroupId,
            SubnetType subnetType, string defaultSubnetDomainSeparator, string subnets, out ISecurityGroup securityGroup,
            out ISecret secret, out ISubnetSelection subnetSelection)
        {
            if (vpc == null)
            {
                throw new ArgumentException($"The VPC provided to create the database is not valid");
            }

            securityGroup = AwsSecurityGroupHandler.Locate(securityId, securityGroupId);

            if (securityGroup == null)
            {
                throw new ArgumentException($"The Security group id {securityGroupId} provided to create the database is not valid");
            }

            secret = AwsCdkSecretHandler.Create(secretName);
            subnetSelection = AwsCdkVpcHandler.GetVpcSubnetSelection(vpc, subnets, defaultSubnetDomainSeparator, subnetType);
        }

        private IClusterEngine GetClusterEngine(DeputyBase databaseEngineVersion)
        {
            var databaseType = databaseEngineVersion.GetType();
            
            if (databaseType == typeof(AuroraMysqlEngineVersion))
            {
                return DatabaseClusterEngine.AuroraMysql(new AuroraMysqlClusterEngineProps
                {
                    Version = databaseEngineVersion as AuroraMysqlEngineVersion
                });
            }

            if (databaseType == typeof(AuroraPostgresEngineVersion))
            {
                return DatabaseClusterEngine.AuroraPostgres(new AuroraPostgresClusterEngineProps
                {
                    Version = databaseEngineVersion as AuroraPostgresEngineVersion
                });
            }

            throw new ArgumentException("Not supported database cluster option. Try AuroraMysqlEngineVersion or AuroraPostgresEngineVersion");
        }

        private IInstanceEngine GetInstanceEngine(DeputyBase databaseEngineVersion)
        {
            var databaseType = databaseEngineVersion.GetType();

            if (databaseType == typeof(MysqlEngineVersion))
            {
                return DatabaseInstanceEngine.Mysql(new MySqlInstanceEngineProps
                {
                    Version = databaseEngineVersion as MysqlEngineVersion
                });
            }

            if (databaseType == typeof(PostgresEngineVersion))
            {
                return DatabaseInstanceEngine.Postgres(new PostgresInstanceEngineProps()
                {
                    Version = databaseEngineVersion as PostgresEngineVersion
                });
            }

            if (databaseType == typeof(MariaDbEngineVersion))
            {
                return DatabaseInstanceEngine.MariaDb(new MariaDbInstanceEngineProps()
                {
                    Version = databaseEngineVersion as MariaDbEngineVersion
                });
            }

            if (databaseType == typeof(SqlServerEngineVersion))
            {
                return DatabaseInstanceEngine.SqlServerEe(new SqlServerEeInstanceEngineProps()
                {
                    Version = databaseEngineVersion as SqlServerEngineVersion
                });
            }

            if (databaseType == typeof(OracleEngineVersion))
            {
                return DatabaseInstanceEngine.OracleEe(new OracleEeInstanceEngineProps()
                {
                    Version = databaseEngineVersion as OracleEngineVersion
                });
            }

            throw new ArgumentException("Not supported database option. Try: MysqlEngineVersion, PostgresEngineVersion, MariaDbEngineVersion, SqlServerEngineVersion and OracleEngineVersion");
        }

        #endregion
    }
}
