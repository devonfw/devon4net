using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.RDS;
using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{
    public class DatabaseOptions
    {
        public string Id { get; set; }
        public string DatabaseName { get; set; }
        public string InstanceIdentifier { get; set; }
        public string Port { get; set; }
        public string UserName { get; set; }
        public Dictionary<string, string> Secrets { get; set; }
        public Dictionary<string, string> SsmParameters { get; set; }
        public string VpcId { get; set; }
        public string SecurityGroupId { get; set; }
        public List<string> SecurityGroupsIds { get; set; }
        public string SubnetGroupId { get; set; }
        public string[] LogTypes { get; set; }
        public bool? StorageEncrypted { get; set; }
        public bool? EnableIamAuthentication { get; set; }
        public bool? DeletionProtection { get; set; }
        public bool? MultiAvailabilityZoneEnabled { get; set; }
        public int? PasswordRotationDaysPeriod { get; set; }
        public string RotationLambdaId { get; set; }
        public bool? AutoMinorVersionUpgrade { get; set; }
        public string ParameterGroupId { get; set; }
        public DmsEndpointOptions[] DmsEndpoints { get; set; }
        public string DatabaseType { get; set; }
        public string Edition { get; set; }
        public double? AllocatedStorageGb { get; set; }
        public string LicenseOption { get; set; }
        public string InstanceSize { get; set; }
        public string Password { get; set; }
        public int BackupRetentionPeriod { get; set; }
        public StorageType StorageType { get; set; }
        public string DatabaseEngineVersion { get; set; }
        public InstanceClass InstanceType { get; set; }
        public int? EnhancedMonitoringIntervalSeconds { get; set; }
        public string? MonitoringRoleName { get; set; }
        public bool? EnablePerformanceInsights { get; set; }
        public int? PerformanceInsightsRetentionPeriod { get; set; }
    }
}
