namespace Devon4Net.Infrastructure.Common.Application.Options
{

    public class ExtraSettingsOptions
    {
        public int KeepAliveTimeout { get; set; }
        public int MaxConcurrentConnections { get; set; }
        public int MaxConcurrentUpgradedConnections { get; set; }
        public float MaxRequestBodySize { get; set; }
        public int Http2MaxStreamsPerConnection { get; set; }
        public int Http2InitialConnectionWindowSize { get; set; }
        public int Http2InitialStreamWindowSize { get; set; }
        public bool AllowSynchronousIO { get; set; }
    }

}