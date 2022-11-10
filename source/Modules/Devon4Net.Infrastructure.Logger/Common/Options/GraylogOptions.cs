namespace Devon4Net.Infrastructure.Logger.Common.Options
{
    public class GraylogOptions
    {
        public string GrayLogHost { get; set; }
        public int GrayLogPort { get; set; }
        public string GrayLogProtocol { get; set; }
        public int MaxUdpMessageSize { get; set; }
    }
}
