namespace Devon4Net.Infrastructure.Common.Options.AnsibleTower
{
    public class AnsibleTowerOptions
    {
        public bool EnableAnsible { get; set; }
        public string Name { get; set; }
        public string CircuitBreakerName { get; set; }
        public string ApiUrlBase { get; set; }
        public string Version { get; set; }
    }
}
