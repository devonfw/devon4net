namespace Devon4Net.Infrastructure.Common.Options.Devon
{
    public class DevonfwOptions
    {
        public bool UseDetailedErrorsKey { get; set; }
        public bool UseIIS { get; set; }
        public bool UseSwagger { get; set; }
        public string Environment { get; set; }
        public Killswitch KillSwitch { get; set; }
        public Kestrel Kestrel { get; set; }
    }
}