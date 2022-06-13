using Devon4Net.Infrastructure.Nexus.Model.Repositories.Base;
using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.Nexus.Model.Repositories.GroupRepositories
{
    public class DockerGroupRepository : NexusRepositoryGroup
    {
        [JsonPropertyName("docker")]
        public Docker Docker { get; set; }
    }
}