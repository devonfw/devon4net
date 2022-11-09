namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{
    public class BlockDevicesOptions
    {
        /// <summary>
        /// The device name exposed to the EC2 instance.
        /// </summary>
        public string BlockDeviceName { get; set; }
        public BlockDeviceVolumeOptions BlockDeviceVolume { get; set; }

    }
}
