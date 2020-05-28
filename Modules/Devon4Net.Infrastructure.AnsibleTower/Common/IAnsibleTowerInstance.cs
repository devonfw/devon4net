using System.Threading.Tasks;
using Devon4Net.Infrastructure.AnsibleTower.Dto;

namespace Devon4Net.Infrastructure.AnsibleTower.Common
{
    public interface IAnsibleTowerInstance
    {
        void Setup(ApiRequestDto apiRequestDto);
    }
}