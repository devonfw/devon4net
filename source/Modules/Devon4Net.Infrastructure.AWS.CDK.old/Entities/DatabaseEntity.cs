using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.RDS;
using Amazon.JSII.Runtime.Deputy;

namespace Devon4Net.Infrastructure.AWS.CDK.Entities
{
    public class DatabaseEntity
    {
        public DeputyBase DatabaseVersion { get; set; }
        public string Identification { get; set; }
        public string DatabaseName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public StorageType StorageType { get; set; }
        public InstanceClass InstanceClass { get; set; }
        public InstanceSize InstanceSize { get; set; }
        public double? AllocatedStorageGb { get; set; }
        public RemovalPolicy RemovalPolicy { get; set; }
        public bool DeleteAutomatedBackups { get; set; }
        public int BackupRetentionDays { get; set; }
        public bool DeletionProtection { get; set; }
        public SubnetType SubnetType { get; set; }
        public string VpcId { get; set; }
        public string SecurityId { get; set; }
        public string SecurityGroupId { get; set; }
        public bool AllowGroupAllOutbound { get; set; }
        public IPeer IngressPeer { get; set; }
        public Port IngressPort { get; set; }
        public IPeer EgressPeer { get; set; }
        public Port EgressPort { get; set; }
    }
}
