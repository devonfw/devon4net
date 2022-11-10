namespace Devon4Net.Infrastructure.Common.Application.Options
{
    public class KillSwitchOptions
    {
        public bool UseKillSwitch { get; set; }
        public bool EnableRequests { get; set; }
        public int HttpStatusCode { get; set; }
    }
}