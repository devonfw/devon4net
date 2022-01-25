using Amazon.CDK.AWS.EC2;
using Amazon.CDK;
using Amazon.CDK.AWS.RDS;
using Amazon.CDK.AWS.SecretsManager;
using System;
using System.Linq;
using Devon4Net.Infrastructure.AWS.CDK.Consts;
using Devon4Net.Infrastructure.AWS.CDK.Options.Resources;

namespace Devon4Net.Infrastructure.AWS.CDK.Stack
{
    public partial class ProvisionStack
    {
        private void CreateDatabases()
        {
            if (CdkOptions == null || CdkOptions.Databases?.Any() != true) return;

            foreach (var databaseOption in CdkOptions.Databases)
            {
                GetDatabaseResources(databaseOption, out var databasePort, out var vpc, out var securityGroup, out var subnetGroup, out var deletionProtection, out var enchancedMonitoringInterval, out var passwordSecret, out var parameterGroup);

                IDatabaseInstance database;
                switch (databaseOption.DatabaseType?.ToLower())
                {
                    case "sqlserver":
                        var secGroup = LocateSecurityGroup(databaseOption.SecurityGroupId, $"SG ith id {databaseOption.SecurityGroupId} not found in the JSON.");
                        database = AwsCdkHandler.AddDatabase(SqlServerEngineVersion.VER_13, databaseOption.DatabaseName, databaseOption.DatabaseName, databaseOption.UserName, databaseOption.Password, StorageType.STANDARD, InstanceClass.BURSTABLE3, databaseOption.InstanceSize, vpc, secGroup, databaseOption.SecurityGroupId, storageEncrypted: true, allocatedStorageGb: databaseOption.AllocatedStorageGb, licenseOption: databaseOption.LicenseOption, edition: databaseOption.Edition);
                        break;
                    default:
                        StackResources.DynamicSecrets.Add(databaseOption.Secrets[DatabaseOptionConsts.PasswordAttributeName], passwordSecret);
                        database = AwsCdkHandler.AddDatabase(MysqlEngineVersion.VER_8_0, databaseOption.DatabaseName, databaseOption.DatabaseName, databasePort, databaseOption.UserName, passwordSecret, StorageType.GP2, InstanceClass.BURSTABLE3, InstanceSize.MEDIUM, vpc, securityGroup, subnetGroup, deletionProtection: deletionProtection, logTypes: databaseOption.LogTypes, storageEncrypted: databaseOption.StorageEncrypted, enableIamAuthentication: databaseOption.EnableIamAuthentication, enhancedMonitoringInterval: enchancedMonitoringInterval, multiAZEnabled: databaseOption.MultiAvailabilityZoneEnabled ?? true, autoMinorVersionUpgrade: databaseOption.AutoMinorVersionUpgrade ?? false, parameterGroup: parameterGroup);
                        break;
                }
                StackResources.Databases.Add(databaseOption.Id, database);
            }
        }

        private void GetDatabaseResources(DatabaseOptions databaseOption, out double? databasePort, out IVpc vpc, out ISecurityGroup securityGroup, out ISubnetGroup subnetGroup, out bool deletionProtection, out Duration enchancedMonitoringInterval, out ISecret passwordSecret, out IParameterGroup parameterGroup)
        {
            // Parse database port
            databasePort = ParseDatabasePort(databaseOption);

            // Locate vpc
            vpc = LocateVPC(databaseOption);

            // Locate security group
            securityGroup = LocateSecurityGroup(databaseOption);

            // Locate subnet group
            subnetGroup = LocateSubnetGroup(databaseOption);

            // Locate parameter group
            parameterGroup = LocateParameterGroup(databaseOption);

            deletionProtection = databaseOption.DeletionProtection ?? false;

            enchancedMonitoringInterval = databaseOption.EnhancedMonitoringIntervalSeconds.HasValue ? Duration.Seconds(databaseOption.EnhancedMonitoringIntervalSeconds.Value) : null;

            passwordSecret = SetDatabasePassword(databaseOption);
        }

        private ISecret SetDatabasePassword(DatabaseOptions databaseOption)
        {
            ISecret passwordSecret;
            if (string.IsNullOrWhiteSpace(databaseOption.Password))
            {
                if (databaseOption.Secrets.ContainsKey(DatabaseOptionConsts.PasswordAttributeName))
                {
                    if (!StackResources.Lambdas.TryGetValue(databaseOption.RotationLambdaId, out var rotationLambda))
                    {
                        throw new ArgumentException($"The database {databaseOption.Id} lambda id {databaseOption.RotationLambdaId} was not found");
                    }

                    var rotationPeriod = databaseOption.PasswordRotationDaysPeriod.HasValue ? Duration.Days(databaseOption.PasswordRotationDaysPeriod.Value) : null;

                    passwordSecret = AwsCdkHandler.AddSecret(databaseOption.Secrets[DatabaseOptionConsts.PasswordAttributeName], rotationPeriod: rotationPeriod, rotationLambda: rotationLambda);
                }
                else
                {
                    throw new ArgumentException($"The database {databaseOption.DatabaseName} has no secret to store the password");
                }
            }
            else
            {
                passwordSecret = null;
            }

            return passwordSecret;
        }

        private IParameterGroup LocateParameterGroup(DatabaseOptions databaseOption)
        {
            IParameterGroup parameterGroup;
            if (!string.IsNullOrWhiteSpace(databaseOption.ParameterGroupId))
            {
                if (!StackResources.DatabaseParameterGroups.TryGetValue(databaseOption.ParameterGroupId, out parameterGroup))
                {
                    throw new ArgumentException($"The parameter group { databaseOption.ParameterGroupId } of the database { databaseOption.DatabaseName} was not found");
                }
            }
            else
            {
                parameterGroup = null;
            }

            return parameterGroup;
        }

        private ISubnetGroup LocateSubnetGroup(DatabaseOptions databaseOption)
        {
            return LocateSubnetGroup(databaseOption.SubnetGroupId,
                $"The subnet group {databaseOption.SubnetGroupId} of the database {databaseOption.DatabaseName} was not found",
                $"The database {databaseOption.DatabaseName} must have a subnet group");
        }

        private ISecurityGroup LocateSecurityGroup(DatabaseOptions databaseOption)
        {
            return LocateSecurityGroup(databaseOption.SecurityGroupId,
                $"The security group {databaseOption.SecurityGroupId} of the database {databaseOption.DatabaseName} was not found",
                $"The database {databaseOption.DatabaseName} must have a security group");
        }

        private IVpc LocateVPC(DatabaseOptions databaseOption)
        {
            return LocateVpc(databaseOption.VpcId,
                $"The vpc {databaseOption.VpcId} of the database {databaseOption.DatabaseName} was not found",
                $"The database {databaseOption.DatabaseName} must have a vpc");
        }

        private static double? ParseDatabasePort(DatabaseOptions databaseOption)
        {
            if (string.IsNullOrWhiteSpace(databaseOption.Port))
            {
                return null;
            }
            else
            {
                if (!double.TryParse(databaseOption.Port, out var port))
                {
                    throw new ArgumentException($"The port {databaseOption.Port} of the database {databaseOption.Id} could not be converted to a number");
                }
                return port;
            }
        }

        private string GetDatabaseProperty(DatabaseOptions databaseOption, IDatabaseInstance database, string propertyName)
        {
            string databaseAttributeValue = null;

            switch (propertyName)
            {
                case DatabaseOptionConsts.HostAttributeName:
                    databaseAttributeValue = database.InstanceEndpoint.Hostname;
                    break;
                case DatabaseOptionConsts.PortAttributeName:
                    databaseAttributeValue = databaseOption.Port;
                    break;
                case DatabaseOptionConsts.UserAttributeName:
                    databaseAttributeValue = databaseOption.UserName;
                    break;
                case DatabaseOptionConsts.PasswordAttributeName:
                    // Ignored, the database password secret is created during the creation of the database itself
                    break;
                case DatabaseOptionConsts.DatabaseNameAttributeName:
                    databaseAttributeValue = databaseOption.DatabaseName;
                    break;
                default:
                    Console.WriteLine($"[WARNING] The database secret property {propertyName} is not recognized and will be skipped");
                    break;
            }

            return databaseAttributeValue;
        }

        private void CreateDatabaseParameterGroups()
        {
            if (CdkOptions == null || CdkOptions.DatabaseParameterGroups?.Any() != true) return;

            foreach (var parameterGroupOption in CdkOptions.DatabaseParameterGroups)
            {
                if (parameterGroupOption.LocateInsteadOfCreate)
                {
                    var parameterGroup = AwsCdkHandler.LocateParameterGroupByName(parameterGroupOption.Id, parameterGroupOption.Name);
                    StackResources.DatabaseParameterGroups.Add(parameterGroupOption.Id, parameterGroup);
                }
                else
                {
                    var parameterGroup = AwsCdkHandler.CreateParameterGroup(MysqlEngineVersion.VER_8_0, parameterGroupOption.Name, parameterGroupOption.Description, parameterGroupOption.Parameters);
                    StackResources.DatabaseParameterGroups.Add(parameterGroupOption.Id, parameterGroup);
                }
            }
        }

        private IDatabaseInstance LocateDatabase(string databaseId, string exceptionMessageIfDatabaseDoesNotExist, string exceptionMessageIfDatabaseIsEmpty = null)
        {
            return StackResources.Locate<IDatabaseInstance>(databaseId, exceptionMessageIfDatabaseDoesNotExist, exceptionMessageIfDatabaseIsEmpty);
        }
    }
}
