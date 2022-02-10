namespace Devon4Net.Infrastructure.Common.Options.KillSwitch
{
    public class KillSwitchOptions
    {
        public bool UseKillSwitch { get; set; }
        public bool EnableRequests { get; set; }
        public int HttpStatusCode { get; set; }
    }
}