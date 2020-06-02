using System.Threading.Tasks;
using Devon4Net.Infrastructure.AnsibleTower.Dto;

namespace Devon4Net.Infrastructure.AnsibleTower.Common
{
    public interface IAnsibleTowerInstance
    {
        string Name { get; }
        string CircuitBreakerName { get; }
        string ApiUrlBase { get; }
        string Version { get; }
        ApiRequestDto ApiDefinition { get; }
    }
}