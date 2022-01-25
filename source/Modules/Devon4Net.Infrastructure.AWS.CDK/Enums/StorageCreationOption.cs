namespace Devon4Net.Infrastructure.AWS.CDK.Enums
{
    public enum StorageCreationOption
    {
        /// <summary>
        /// Creates a new Elastic Block Storage device.
        /// </summary>
        EBS = 0,
        /// <summary>
        /// Creates a new Elastic Block Storage device from an existing snapshot.
        /// </summary>
        EBS_FROM_SNAPSHOT_ID = 1,
        /// <summary>
        /// Creates a virtual, ephemeral device.
        /// </summary>
        EPHEMERAL = 2,
        /// <summary>
        /// Supresses a volume mapping.
        /// </summary>
        NO_DEVICE = 3
    }
}
