namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Designer
{
    public class CreateDesignVersionRequestDto
    {
        public string containerId { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
        public string name { get; set; }
        public bool published { get; set; }
        public string type { get; set; }
        public object[] upgrades_from { get; set; }
        public object[] upgrades_to { get; set; }
        public string url { get; set; }
        public string version { get; set; }
    }
}
