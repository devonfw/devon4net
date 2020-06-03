using System.Threading.Tasks;
using Devon4Net.Infrastructure.AnsibleTower.Dto;
using Devon4Net.Infrastructure.AnsibleTower.Dto.Applications;
using Devon4Net.Infrastructure.AnsibleTower.Dto.Inventories;
using Devon4Net.Infrastructure.AnsibleTower.Dto.JobTemplates;
using Devon4Net.Infrastructure.AnsibleTower.Dto.Organizations;

namespace Devon4Net.Infrastructure.AnsibleTower.Handler
{
    public interface IAnsibleTowerHandler
    {
        Task<LoginRequestDto> Login(string userName, string password);
        Task<GetApplicationsResponseDto> GetApplications(string authenticationToken);
        Task<ApplicationsResponseDto> CreateApplication(ApplicationsRequestDto applicationstRequest, string authenticationToken);
        Task<OrganizationsResponseDto> GetOrganizations(string authenticationToken);
        Task<ResultOrganizationDto> GetOrganizationById(string authenticationToken, string organizationId);
        Task<ResultOrganizationDto> CreateOrganization(string authenticationToken, CreateOrganizationRequestDto organizationRequest);
        Task<GetInventoriesResponseDto> GetInventories(string authenticationToken);
        Task<ResultInventoryDto> GetInventoryById(string authenticationToken, string inventoryId);
        Task<ResultInventoryDto> PostInventories(string authenticationToken, CreateInventoryRequestDto inventoryRequest);
        Task<GetJobTemplatesResponseDto> GetJobTemplates(string authenticationToken);
        Task<ResultJobDto> GetJobTemplate(string authenticationToken, string jobTemplateId);
        Task<ResultJobDto> CreateJobTemplate(string authenticationToken, CreateJobTemplateRequestDto createJobTemplateRequest);
    }
}