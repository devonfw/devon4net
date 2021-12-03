namespace Devon4Net.Infrastructure.AnsibleTower.Dto.Common
{

    public class PingResponseDto
    {
        public bool ha { get; set; }
        public string version { get; set; }
        public string active_node { get; set; }
        public string install_uuid { get; set; }
        public List<Instance> instances { get; set; }
        public List<Instance_Groups> instance_groups { get; set; }
    }

    public class Instance
    {
        public string node { get; set; }
        public string uuid { get; set; }
        public DateTime heartbeat { get; set; }
        public int capacity { get; set; }
        public string version { get; set; }
    }

    public class Instance_Groups
    {
        public string name { get; set; }
        public int capacity { get; set; }
        public List<string> instances { get; set; }
    }
}
