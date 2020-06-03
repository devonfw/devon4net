using System.Threading.Tasks;
using Devon4Net.Infrastructure.AnsibleTower.Dto;
using Devon4Net.Infrastructure.AnsibleTower.Dto.Applications;
using Devon4Net.Infrastructure.AnsibleTower.Dto.Organizations;

namespace Devon4Net.Infrastructure.AnsibleTower.Handler
{
    public interface IAnsibleTowerHandler
    {
        Task<LoginRequestDto> Login(string userName, string password);
        Task<ApplicationsResponseDto> Applications(ApplicationsRequestDto applicationstRequest, string authenticationToken);
        Task<OrganizationsResponseDto> GetOrganizations(string authenticationToken);
    }
}