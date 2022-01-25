using Amazon.CDK.AWS.AutoScaling;

namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{
    public class EbsDevicePropsOptions
    {
        /// <summary>
        /// Indicates whether to delete the volume when the instance is terminated.
        /// </summary>
        public bool? DeleteOnTermination { get; set; }
        /// <summary>
        /// Specifies whether the EBS volume is encrypted.
        /// </summary>
        public bool? Encrypted { get; set; }
        /// <summary>
        /// The number of I/O operations per second (IOPS) to provision for the volume.
        /// </summary>
        public uint? Iops { get; set; }
        /// <summary>
        /// The snapshot ID of the volume to use.
        /// </summary>
        public string SnapshotId { get; set; }
        /// <summary>
        /// The volume size, in Gibibytes (GiB).
        /// </summary>
        public uint? VolumeSize { get; set; }
        public uint? VolumeIndex { get; set; }
        /// <summary>
        /// The EBS volume type.
        /// </summary>
        public EbsDeviceVolumeType VolumeType { get; set; }
    }
}
