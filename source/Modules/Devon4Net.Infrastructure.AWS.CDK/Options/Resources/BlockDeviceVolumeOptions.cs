using Devon4Net.Infrastructure.AWS.CDK.Enums;

namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{
    public class BlockDeviceVolumeOptions
    {
        public EbsDevicePropsOptions EbsDevice { get; set; }
        public StorageCreationOption CreationOption { get; set; }
    }
}
