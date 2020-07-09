using Devon4Net.Infrastructure.AnsibleTower.Dto;

namespace Devon4Net.Infrastructure.AnsibleTower.Common
{
    /// <summary>
    /// Ansible handler manager
    /// </summary>
    public class AnsibleTowerInstance : IAnsibleTowerInstance
    {
        public ApiRequestDto ApiDefinition { get; set; }
        public string Name { get; }
        public string CircuitBreakerName { get; }
        public string ApiUrlBase { get; }
        public string Version { get; }
        public string Username { get; set; }
        public string Password { get; set; }

        public AnsibleTowerInstance(string name, string circuitBreakerName, string apiUrlBase, string version, ApiRequestDto apiRequestDto, string username, string password)
        {
            ApiDefinition = apiRequestDto;
            Name = name;
            CircuitBreakerName = circuitBreakerName;
            ApiUrlBase = apiUrlBase;
            Version = version;
            Username = username;
            Password = password;
        }
    }
}
