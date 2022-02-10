using Amazon.CDK.AWS.DMS;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.RDS;
using Amazon.CDK.AWS.SecretsManager;
using Devon4Net.Infrastructure.AWS.CDK.Consts;
using Devon4Net.Infrastructure.AWS.CDK.Options.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Devon4Net.Infrastructure.AWS.CDK.Stack
{
    public partial class ProvisionStack
    {
        public void CreateDms()
        {
            if (CdkOptions == null || CdkOptions.DatabaseMiagrationService == null) return;

            CreateDmsEndpoints();
            CreateDmsReplicationSubnetGroups();
            CreateDmsReplicationInstances();
            CreateDmsMigrationTasks();
        }

        public void CreateDmsEndpoints()
        {
            if (CdkOptions == null || CdkOptions.Databases?.Any() != true) return;

            StackResources.DmsEndpoints = new Dictionary<string, CfnEndpoint>();

            foreach (var databaseOption in CdkOptions.Databases)
            {
                GetDmsEndpointsResources(databaseOption, out var databasePort, out var databaseInstance, out var databasePasswordSecret);

                if (databaseOption.DmsEndpoints?.Any() == true)
                {
                    foreach (var dmsEnpointOption in databaseOption.DmsEndpoints)
                    {
                        var dmsEndpoint = AwsCdkHandler.CreateDmsEndpoint(dmsEnpointOption.Id, dmsEnpointOption.Type, RdsEngineNameConsts.MySql, databaseInstance.InstanceEndpoint.Hostname, databasePort, databaseOption.DatabaseName, databaseOption.UserName, databasePasswordSecret.SecretValue.ToString(), dmsEnpointOption.Name);

                        StackResources.DmsEndpoints.Add(dmsEnpointOption.Id, dmsEndpoint);
                    }
                }
            }
        }

        private void GetDmsEndpointsResources(DatabaseOptions databaseOption, out double databasePort, out IDatabaseInstance databaseInstance, out ISecret databasePasswordSecret)
        {
            // Parse database port
            if (!double.TryParse(databaseOption.Port, out databasePort))
            {
                throw new ArgumentException($"The port {databaseOption.Port} of the database {databaseOption.Id} could not be converted to a number");
            }

            // Find database instance
            databaseInstance = LocateDatabase(databaseOption.Id,
                $"The database {databaseOption.Id} could not be found",
                "The database field \"Id\" cannot be empty");

            // Find database password secret
            if (StackResources.DynamicSecrets?.Any() != true)
            {
                throw new ArgumentException("No dynamic secrets have been created, so the Dms Endpoint cannot be created");
            }

            if (!databaseOption.Secrets.TryGetValue(DatabaseOptionConsts.PasswordAttributeName, out var databasePasswordSecretKey))
            {
                throw new ArgumentException("No secret for the database password has been created, so the Dms Endpoint cannot be created");
            }

            if (!StackResources.DynamicSecrets.TryGetValue(databasePasswordSecretKey, out databasePasswordSecret))
            {
                throw new ArgumentException($"The secret password {databasePasswordSecretKey} for the database {databaseOption.Id} was not found");
            }
        }

        public void CreateDmsReplicationSubnetGroups()
        {
            if (CdkOptions == null || CdkOptions.DatabaseMiagrationService.DmsReplicationSubnetGroups?.Any() != true) return;

            StackResources.DmsReplicationSubnetGroups = new Dictionary<string, CfnReplicationSubnetGroup>();

            foreach (var replicationSubnetGroupOption in CdkOptions.DatabaseMiagrationService.DmsReplicationSubnetGroups)
            {
                GetDmsReplicationSubnetGroupsResources(replicationSubnetGroupOption, out var vpcSubnetIds);

                var dmsReplicationSubnetGroup = AwsCdkHandler.CreateDmsReplicationSubnetGroup(replicationSubnetGroupOption.Id, replicationSubnetGroupOption.Name, replicationSubnetGroupOption.Description, vpcSubnetIds);

                StackResources.DmsReplicationSubnetGroups.Add(replicationSubnetGroupOption.Id, dmsReplicationSubnetGroup);
            }
        }

        public void GetDmsReplicationSubnetGroupsResources(DmsReplicationSubnetGroupOptions replicationSubnetGroupOption, out string[] vpcSubnetIds)
        {
            // Locate Subnets
            vpcSubnetIds = null;
            if (replicationSubnetGroupOption.ReplicationSubnetIds?.Any() != true) throw new ArgumentException($"The replication subnet group {replicationSubnetGroupOption.Name} must have a replication subnet ids");

            List<string> vpcSubnetIdsList = new List<string>();
            foreach (var subnetIdOption in replicationSubnetGroupOption.ReplicationSubnetIds)
            {
                if (!string.IsNullOrWhiteSpace(subnetIdOption))
                {
                    if (!StackResources.Subnets.TryGetValue(subnetIdOption, out var subnet))
                    {
                        throw new ArgumentException($"The replication subnet id { subnetIdOption } of the replication subnet group {replicationSubnetGroupOption.Name} was not found");
                    }
                    vpcSubnetIdsList.Add(subnet.SubnetId);
                }
            }
            vpcSubnetIds = vpcSubnetIdsList.ToArray();
        }

        public void CreateDmsReplicationInstances()
        {
            if (CdkOptions == null || CdkOptions.DatabaseMiagrationService.DmsReplicationInstances?.Any() != true) return;

            StackResources.DmsReplicationInstances = new Dictionary<string, CfnReplicationInstance>();

            foreach (var replicationInstanceOption in CdkOptions.DatabaseMiagrationService.DmsReplicationInstances)
            {
                GetDmsReplicationInstancesResources(replicationInstanceOption, out var vpcSecurityGroupsIds, out var subnetGroupName, out var publiclyAccessible);
                var dmsReplicationInstance = AwsCdkHandler.CreateDmsReplicationInstance(replicationInstanceOption.Id, replicationInstanceOption.Name, replicationInstanceOption.ReplicationInstanceClass, subnetGroupName, vpcSecurityGroupIds: vpcSecurityGroupsIds, publiclyAccessible: publiclyAccessible);

                StackResources.DmsReplicationInstances.Add(replicationInstanceOption.Id, dmsReplicationInstance);
            }
        }

        public void GetDmsReplicationInstancesResources(DmsReplicationInstanceOptions replicationInstanceOption, out string[] vpcSecurityGroupsIds, out string subnetGroupName, out bool publiclyAccessible)
        {
            // Locate subnet group
            if (!string.IsNullOrWhiteSpace(replicationInstanceOption.SubnetGroupId))
            {
                if (!StackResources.DmsReplicationSubnetGroups.TryGetValue(replicationInstanceOption.SubnetGroupId, out var subnetGroup))
                {
                    throw new ArgumentException($"The subnet group id { replicationInstanceOption.SubnetGroupId } of the replication instance { replicationInstanceOption.Name } was not found");
                }
                else
                {
                    subnetGroupName = subnetGroup.ReplicationSubnetGroupIdentifier;
                }
            }
            else
            {
                throw new ArgumentException($"The replication instance { replicationInstanceOption.Name } must have a subnet group id");
            }

            // Locate security group Ids
            vpcSecurityGroupsIds = null;
            if (replicationInstanceOption.SecurityGroupIds?.Any() == true)
            {
                List<string> vpcSecurityGroupsIdsList = new List<string>();
                foreach (var securityGroupOption in replicationInstanceOption.SecurityGroupIds)
                {
                    ISecurityGroup securityGroup = LocateSecurityGroup(securityGroupOption,
                        $"The security group {securityGroupOption} of the replicationInstance {replicationInstanceOption.Name} was not found");
                    vpcSecurityGroupsIdsList.Add(securityGroup.SecurityGroupId);
                }
                vpcSecurityGroupsIds = vpcSecurityGroupsIdsList.ToArray();
            }

            // Is public accessible
            publiclyAccessible = replicationInstanceOption.PubliclyAccessible ?? false;
        }

        public void CreateDmsMigrationTasks()
        {
            if (CdkOptions == null || CdkOptions.DatabaseMiagrationService.DmsMigrationTasks?.Any() != true) return;

            StackResources.DmsMigrationTasks = new Dictionary<string, CfnReplicationTask>();

            foreach (var dmsMigrationTaskOption in CdkOptions.DatabaseMiagrationService.DmsMigrationTasks)
            {
                GetDmsMigrationTaskResources(dmsMigrationTaskOption, out var sourceEndpointArn, out var targetEndpointArn, out var replicationInstanceARN);

                var dmsMigrationTasks = AwsCdkHandler.CreateDmsMigrationTasks(dmsMigrationTaskOption.Id, dmsMigrationTaskOption.Name, dmsMigrationTaskOption.MigrationType, replicationInstanceARN, sourceEndpointArn, targetEndpointArn, dmsMigrationTaskOption.TableMappings);

                StackResources.DmsMigrationTasks.Add(dmsMigrationTaskOption.Id, dmsMigrationTasks);
            }
        }

        public void GetDmsMigrationTaskResources(DmsMigrationTaskOptions dmsMigrationTaskOption, out string sourceEndpointArn, out string targetEndpointArn, out string replicationInstanceARN)
        {
            // Check Migration Type
            CheckMigrationType(dmsMigrationTaskOption);

            // Locate source endpoint id
            sourceEndpointArn = LocateSourceEndpoint(dmsMigrationTaskOption);

            // Locate target endpoint id
            targetEndpointArn = LocateTargetEndpoint(dmsMigrationTaskOption);

            // Locate replication instance id
            replicationInstanceARN = LocateReplicationInstance(dmsMigrationTaskOption);
        }

        private static void CheckMigrationType(DmsMigrationTaskOptions dmsMigrationTaskOption)
        {
            if (!string.IsNullOrWhiteSpace(dmsMigrationTaskOption.MigrationType))
            {
                if (!DmsMigrationTypeConsts.MigrationTypes.Contains(dmsMigrationTaskOption.MigrationType))
                {
                    throw new ArgumentException($"The migration type { dmsMigrationTaskOption.MigrationType } of the dms migration task { dmsMigrationTaskOption.Name } is wrong. It must have one of the next values: { string.Join(", ", DmsMigrationTypeConsts.MigrationTypes) }");
                }
            }
            else
            {
                throw new ArgumentException($"The dms migration task { dmsMigrationTaskOption.Name } must have a migration type");
            }

            // Check Table Mappings
            if (string.IsNullOrWhiteSpace(dmsMigrationTaskOption.TableMappings))
            {
                throw new ArgumentException($"The dms migration task { dmsMigrationTaskOption.Name } must have a table mappings");
            }
        }

        private string LocateSourceEndpoint(DmsMigrationTaskOptions dmsMigrationTaskOption)
        {
            string sourceEndpointArn;
            if (!string.IsNullOrWhiteSpace(dmsMigrationTaskOption.SourceEndpointId))
            {
                if (!StackResources.DmsEndpoints.TryGetValue(dmsMigrationTaskOption.SourceEndpointId, out var sourceEndpoint))
                {
                    throw new ArgumentException($"The source endpoint id { dmsMigrationTaskOption.SourceEndpointId } of the dms migration task { dmsMigrationTaskOption.Name } was not found");
                }
                else
                {
                    sourceEndpointArn = sourceEndpoint.Ref;
                }
            }
            else
            {
                throw new ArgumentException($"The dms migration task { dmsMigrationTaskOption.Name } must have a source endpoint id");
            }

            return sourceEndpointArn;
        }

        private string LocateTargetEndpoint(DmsMigrationTaskOptions dmsMigrationTaskOption)
        {
            string targetEndpointArn;
            if (!string.IsNullOrWhiteSpace(dmsMigrationTaskOption.TargetEndpointId))
            {
                if (!StackResources.DmsEndpoints.TryGetValue(dmsMigrationTaskOption.TargetEndpointId, out var targetEndpoint))
                {
                    throw new ArgumentException($"The target endpoint id { dmsMigrationTaskOption.TargetEndpointId } of the dms migration task { dmsMigrationTaskOption.Name } was not found");
                }
                else
                {
                    targetEndpointArn = targetEndpoint.Ref;
                }
            }
            else
            {
                throw new ArgumentException($"The dms migration task { dmsMigrationTaskOption.Name } must have a target endpoint id");
            }

            return targetEndpointArn;
        }

        private string LocateReplicationInstance(DmsMigrationTaskOptions dmsMigrationTaskOption)
        {
            string replicationInstanceARN;
            if (!string.IsNullOrWhiteSpace(dmsMigrationTaskOption.ReplicationInstanceId))
            {
                if (!StackResources.DmsReplicationInstances.TryGetValue(dmsMigrationTaskOption.ReplicationInstanceId, out var replicationInstance))
                {
                    throw new ArgumentException($"The replication instance id { dmsMigrationTaskOption.ReplicationInstanceId } of the dms migration task { dmsMigrationTaskOption.Name } was not found");
                }
                else
                {
                    replicationInstanceARN = replicationInstance.Ref;
                }
            }
            else
            {
                throw new ArgumentException($"The dms migration task { dmsMigrationTaskOption.Name } must have a replication instance id");
            }

            return replicationInstanceARN;
        }
    }
}
