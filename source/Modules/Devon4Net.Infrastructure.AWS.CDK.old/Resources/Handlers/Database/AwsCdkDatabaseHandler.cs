using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.RDS;
using Amazon.CDK.AWS.SecretsManager;
using Amazon.JSII.Runtime.Deputy;
using Constructs;
using Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.Secrets;
using Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.SecurityGroupHandler;
using Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.VPC;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.Database
{
    public class AwsCdkDatabaseHandler : AwsCdkBaseHandler, IAwsCdkDatabaseHandler
    {
        private IAwsSecurityGroupHandler AwsSecurityGroupHandler { get; }
        private AwsCdkVpcHandler AwsCdkVpcHandler { get; }
        private AwsCdkSecretHandler AwsCdkSecretHandler { get; }

        public AwsCdkDatabaseHandler(Construct scope, string applicationName, string environmentName, IAwsSecurityGroupHandler awsSecurityGroupHandler, AwsCdkVpcHandler awsCdkVpcHandler, AwsCdkSecretHandler awsCdkSecretHandler, string region) : base(scope, applicationName, environmentName, region)
        {
            AwsSecurityGroupHandler = awsSecurityGroupHandler;
            AwsCdkVpcHandler = awsCdkVpcHandler;
            AwsCdkSecretHandler = awsCdkSecretHandler;
        }

        #region AWSExecution

        /// <summary>
        /// AwsCdkDatabaseHandler
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
        /// <param name="parameterGroupId"></param>
        /// <param name="roles"></param>
        /// <param name="storageEncrypted"></param>
        /// <param name="subnetType"></param>
        /// <param name="defaultSubnetDomainSeparator"></param>
        /// <param name="subnets"></param>
        /// <param name="removalPolicy"></param>
        /// <param name="backupRetentionDays"></param>
        /// <param name="deletionProtection"></param>
        public IDatabaseCluster CreateDatabaseCluster(DeputyBase databaseEngineVersion, string identification, string clusterIdentifier, string instanceIdentifierBase, string databaseName, double? port, double? instances, string userName, string secretName, IVpc vpc, InstanceClass instanceClass, InstanceSize instanceSize, string securityId, string securityGroupId, string parameterGroupId = null, IRole[] roles = null, bool storageEncrypted = true, SubnetType subnetType = SubnetType.PRIVATE_ISOLATED, string defaultSubnetDomainSeparator = ",", string subnets = "", RemovalPolicy removalPolicy = RemovalPolicy.DESTROY, int backupRetentionDays = 1, bool deletionProtection = false)
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

        public IDatabaseInstance CreateDatabase(DeputyBase databaseEngineVersion, string identification, string databaseName, double? port, string userName, string secretName, StorageType storageType, InstanceClass instanceClass, InstanceSize instanceSize, IVpc vpc, string securityId, string securityGroupId, string parameterGroupId = null, IRole[] roles = null, double? allocatedStorageGb = 5, RemovalPolicy removalPolicy = RemovalPolicy.DESTROY, bool deleteAutomatedBackups = false, int backupRetentionDays = 1, bool deletionProtection = false, SubnetType subnetType = SubnetType.PRIVATE_ISOLATED, string defaultSubnetDomainSeparator = ",", string subnets = "", bool multiAZEnabled = true, bool autoMinorVersionUpgrade = false, bool? storageEncrypted = true)
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
                Port = port,
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
                ParameterGroup = CreateClusterParameterGroup(parameterGroupId, engine, roles),
                MultiAz = multiAZEnabled,
                AutoMinorVersionUpgrade = autoMinorVersionUpgrade,
                StorageEncrypted = storageEncrypted
            });
        }

        public IDatabaseInstance CreateDatabase(DeputyBase databaseEngineVersion, string identification, string databaseName, double? port, string userName, ISecret passwordSecret, StorageType storageType, InstanceClass instanceClass, InstanceSize instanceSize, IVpc vpc, ISecurityGroup securityGroup, ISubnetGroup subnetGroup, IParameterGroup parameterGroup = null, double? allocatedStorageGb = 5, RemovalPolicy removalPolicy = RemovalPolicy.DESTROY, bool deleteAutomatedBackups = false, int backupRetentionDays = 1, bool deletionProtection = false, string[] logTypes = null, bool? storageEncrypted = null, bool? enableIamAuthentication = false, Duration enhancedMonitoringInterval = null, bool multiAZEnabled = true, bool autoMinorVersionUpgrade = false)
        {
            BasicDatabaseInfra(vpc);
            var engine = GetInstanceEngine(databaseEngineVersion);

            return new DatabaseInstance(Scope, identification, new DatabaseInstanceProps
            {
                Engine = engine,
                RemovalPolicy = removalPolicy,
                DeletionProtection = deletionProtection,
                Credentials = Credentials.FromPassword(userName, passwordSecret.SecretValue),
                StorageType = storageType,
                DatabaseName = databaseName,
                Port = port,
                SubnetGroup = subnetGroup,
                Vpc = vpc,
                SecurityGroups = new[]
                {
                    securityGroup
                },
                DeleteAutomatedBackups = deleteAutomatedBackups,
                BackupRetention = Duration.Days(backupRetentionDays),
                AllocatedStorage = allocatedStorageGb,
                InstanceType = InstanceType.Of(instanceClass, instanceSize),
                ParameterGroup = parameterGroup,
                CloudwatchLogsExports = logTypes,
                StorageEncrypted = storageEncrypted,
                IamAuthentication = enableIamAuthentication,
                MonitoringInterval = enhancedMonitoringInterval,
                MultiAz = multiAZEnabled,
                AutoMinorVersionUpgrade = autoMinorVersionUpgrade
            });
        }

        public IDatabaseInstance CreateDatabaseSqlServer(DeputyBase databaseEngineVersion, string identification, string instanceIdentifier, string userName, string password, StorageType storageType, InstanceClass instanceClass, string instanceSize, IVpc vpc, ISecurityGroup security, string securityGroupId, ISubnetGroup subnetGroup, string parameterGroupId = null, IRole[] parameterGroupRoles = null, double? allocatedStorageGb = 5, RemovalPolicy removalPolicy = RemovalPolicy.DESTROY, bool deleteAutomatedBackups = false, int backupRetentionDays = 1, bool? deletionProtection = false, SubnetType subnetType = SubnetType.PRIVATE_WITH_NAT, string defaultSubnetDomainSeparator = ",", string subnets = "", bool multiAZEnabled = true, bool? autoMinorVersionUpgrade = false, bool? storageEncrypted = true, string licenseOption = "LICENSE_INCLUDED", string edition = "ex", List<ISecurityGroup> securityGroups = null, IRole monitoringRole = null, int monitoringInterval = 0, bool enablePerformanceInsights = false, int? performanceInsightsRetention = 0) //NOSONAR number of params
        {
            BasicDatabaseInfraWithHardcodedPassword(vpc, subnetType, defaultSubnetDomainSeparator, subnets, out var subnetSelection);
            var engine = GetInstanceEngine(databaseEngineVersion, edition);

            if (securityGroups == null) securityGroups = new List<ISecurityGroup>();
            if (security != null) securityGroups.Add(security);

            return new DatabaseInstance(Scope, identification, new DatabaseInstanceProps
            {
                Engine = engine,
                RemovalPolicy = removalPolicy,
                DeletionProtection = deletionProtection,
                Credentials = Credentials.FromPassword(userName, SecretValue.UnsafePlainText(password)),
                StorageType = storageType,
                InstanceIdentifier = instanceIdentifier,
                SubnetGroup = subnetGroup,
                Vpc = vpc,
                SecurityGroups = securityGroups.ToArray(),
                DeleteAutomatedBackups = deleteAutomatedBackups,
                BackupRetention = Duration.Days(backupRetentionDays),
                AllocatedStorage = allocatedStorageGb,
                InstanceType = InstanceType.Of(instanceClass, GetInstanceSize(instanceSize)),
                ParameterGroup = CreateClusterParameterGroup(parameterGroupId, engine, parameterGroupRoles),
                MultiAz = multiAZEnabled,
                AutoMinorVersionUpgrade = autoMinorVersionUpgrade,
                StorageEncrypted = storageEncrypted,
                LicenseModel = GetLicenseModel(licenseOption),
                MonitoringInterval = Duration.Seconds(monitoringInterval),
                MonitoringRole = monitoringRole,
                EnablePerformanceInsights = enablePerformanceInsights,
                PerformanceInsightRetention = GetPerformanceInsightsRetention(enablePerformanceInsights, performanceInsightsRetention)
            });
        }

        private static PerformanceInsightRetention? GetPerformanceInsightsRetention(bool enablePerformanceInsights, int? performanceInsightsRetention)
        {
            if (!enablePerformanceInsights)
            {
                return null;
            }

            if (performanceInsightsRetention < 0 || performanceInsightsRetention > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(performanceInsightsRetention), "Performance Insights Retention Period value must be 0 or 1");
            }

            return performanceInsightsRetention == 1 ? PerformanceInsightRetention.LONG_TERM : PerformanceInsightRetention.DEFAULT;
        }

        public IParameterGroup LocateParameterGroupByName(string identification, string parameterGroupName)
        {
            return ParameterGroup.FromParameterGroupName(Scope, identification, parameterGroupName);
        }

        public IParameterGroup CreateParameterGroup(DeputyBase databaseEngineVersion, string identification, string parameterGroupName, IDictionary<string, string> parameters = null)
        {
            var engine = GetInstanceEngine(databaseEngineVersion);
            return new ParameterGroup(Scope, identification, new ParameterGroupProps
            {
                Engine = engine,
                Description = parameterGroupName,
                Parameters = parameters
            });
        }

        public ParameterGroup CreateClusterParameterGroup(string parameterGroupIdentification, IEngine databaseEngineVersion, IRole[] roles)
        {
            if (string.IsNullOrEmpty(parameterGroupIdentification)) return null;
            if (roles?.Any() != true) return null;

            return new ParameterGroup(Scope, $"{ApplicationName}{EnvironmentName}{parameterGroupIdentification}",
                new ParameterGroupProps
                {
                    Description = $"{ApplicationName}{EnvironmentName}{parameterGroupIdentification}",
                    Engine = databaseEngineVersion,
                    Parameters = roles.Select(t => new { t.RoleName, t.RoleArn }).ToDictionary(t => t.RoleName, t => t.RoleArn)
                });
        }

        public ISubnetGroup LocateSubnetGroupByName(string identification, string subnetGroupName)
        {
            return SubnetGroup.FromSubnetGroupName(Scope, identification, subnetGroupName);
        }

        public ISubnet LocateSubnetById(string identification, string subnetId)
        {
            return Subnet.FromSubnetId(Scope, identification, subnetId);
        }

        private static LicenseModel GetLicenseModel(string getLicenseModel)
        {
            if (string.IsNullOrWhiteSpace(getLicenseModel))
            {
                throw new ArgumentException("The license cannot be null or empty");
            }

            if (Enum.TryParse(getLicenseModel, out LicenseModel model))
            {
                return model;
            }
            throw new ArgumentException($"Could not parse {getLicenseModel} to the desired enum, please check the documentation");
        }

        private static InstanceSize GetInstanceSize(string instanceSize)
        {
            if (string.IsNullOrWhiteSpace(instanceSize))
            {
                throw new ArgumentException("The instance size cannot be null or empty");
            }

            if (Enum.TryParse(instanceSize, out InstanceSize size))
            {
                return size;
            }

            throw new ArgumentException($"Could not parse {instanceSize} to the desired enum, please check the documentation");
        }

        private void BasicDatabaseInfra(IVpc vpc, string secretName, string securityId, string securityGroupId,             SubnetType subnetType, string defaultSubnetDomainSeparator, string subnets, out ISecurityGroup securityGroup,             out ISecret secret, out ISubnetSelection subnetSelection) //NOSONAR number of params
        {
            if (vpc == null)
            {
                throw new ArgumentException("The VPC provided to create the database is not valid");
            }

            securityGroup = AwsSecurityGroupHandler.LocateById(securityId, securityGroupId);

            if (securityGroup == null)
            {
                throw new ArgumentException($"The Security group id {securityGroupId} provided to create the database is not valid");
            }

            secret = AwsCdkSecretHandler.Create(secretName);
            subnetSelection = AwsCdkVpcHandler.GetVpcSubnetSelection(vpc, subnets, defaultSubnetDomainSeparator, subnetType);
        }

        private void BasicDatabaseInfraWithHardcodedPassword(IVpc vpc,
            SubnetType subnetType, string defaultSubnetDomainSeparator, string subnets, out ISubnetSelection subnetSelection)
        {
            if (vpc == null)
            {
                throw new ArgumentException($"The VPC provided to create the database is not valid");
            }

            subnetSelection = AwsCdkVpcHandler.GetVpcSubnetSelection(vpc, subnets, defaultSubnetDomainSeparator, subnetType);
        }

        private static void BasicDatabaseInfra(IVpc vpc)
        {
            if (vpc == null)
            {
                throw new ArgumentException("The VPC provided to create the database is not valid");
            }
        }

        private static IClusterEngine GetClusterEngine(DeputyBase databaseEngineVersion)
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

        private static IInstanceEngine GetInstanceEngine(DeputyBase databaseEngineVersion, string edition = "ex")
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
                return edition switch
                {
                    "ee" => DatabaseInstanceEngine.SqlServerEe(new SqlServerEeInstanceEngineProps()
                    {
                        Version = databaseEngineVersion as SqlServerEngineVersion
                    }),
                    "se" => DatabaseInstanceEngine.SqlServerSe(new SqlServerSeInstanceEngineProps()
                    {
                        Version = databaseEngineVersion as SqlServerEngineVersion
                    }),
                    "ex" => DatabaseInstanceEngine.SqlServerEx(new SqlServerExInstanceEngineProps()
                    {
                        Version = databaseEngineVersion as SqlServerEngineVersion
                    }),
                    "web" => DatabaseInstanceEngine.SqlServerWeb(new SqlServerWebInstanceEngineProps()
                    {
                        Version = databaseEngineVersion as SqlServerEngineVersion
                    }),
                    _ => throw new ArgumentException("The edition of the SQL Server is not recognized."),
                };
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
