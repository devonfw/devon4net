using Devon4Net.Infrastructure.Nexus.Model.Repositories.Base;
using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.Nexus.Model.Repositories.HostedRepositories
{
    public class DockerHostedRepository : NexusRepositoryHosted
    {
        [JsonPropertyName("docker")]
        public Docker Docker { get; set; }
    }
}