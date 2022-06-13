using Devon4Net.Infrastructure.Nexus.Model.Repositories.Base;
using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.Nexus.Model.Repositories.ProxyRepositories
{
    public class DockerProxyRepository : NexusRepositoryProxy
    {
        [JsonPropertyName("docker")]
        public Docker Docker { get; set; }
        [JsonPropertyName("dockerProxy")]
        public DockerProxy DockerProxy { get; set; }
    }
}