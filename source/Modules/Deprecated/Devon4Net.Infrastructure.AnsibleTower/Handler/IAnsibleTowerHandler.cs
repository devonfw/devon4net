using Devon4Net.Infrastructure.AnsibleTower.Dto;
using Devon4Net.Infrastructure.AnsibleTower.Dto.Applications;
using Devon4Net.Infrastructure.AnsibleTower.Dto.Common;
using Devon4Net.Infrastructure.AnsibleTower.Dto.Credentials;
using Devon4Net.Infrastructure.AnsibleTower.Dto.Inventories;
using Devon4Net.Infrastructure.AnsibleTower.Dto.Jobs;
using Devon4Net.Infrastructure.AnsibleTower.Dto.JobTemplates;
using Devon4Net.Infrastructure.AnsibleTower.Dto.Organizations;
using Devon4Net.Infrastructure.AnsibleTower.Dto.Projects;

namespace Devon4Net.Infrastructure.AnsibleTower.Handler
{
    public interface IAnsibleTowerHandler
    {
        Task<PingResponseDto> Ping();
        Task<LoginRequestDto> Login(string userName, string password);
        Task<PaginatedResultDto<ResultApplication>> GetApplications(string authenticationToken);
        Task<ApplicationsResponseDto> CreateApplication(ApplicationsRequestDto applicationstRequest, string authenticationToken);
        Task<PaginatedResultDto<ResultOrganizationDto>> GetOrganizations(string authenticationToken, string searchCriteria = null);
        Task<ResultOrganizationDto> GetOrganizationById(string authenticationToken, string organizationId);
        Task<ResultOrganizationDto> CreateOrganization(string authenticationToken, CreateOrganizationRequestDto organizationRequest);
        Task<PaginatedResultDto<ResultInventoryDto>> GetInventories(string authenticationToken);
        Task<ResultInventoryDto> GetInventoryById(string authenticationToken, string inventoryId);
        Task<ResultInventoryDto> CreateInventory(string authenticationToken, CreateInventoryRequestDto inventoryRequest);
        Task<PaginatedResultDto<GetJobTemplatesResponseDto>> GetJobTemplates(string authenticationToken);
        Task<GetJobTemplatesResponseDto> GetJobTemplate(string authenticationToken, string jobTemplateId);
        Task<GetJobTemplatesResponseDto> CreateJobTemplate(string authenticationToken, CreateJobTemplateRequestDto createJobTemplateRequest);
        Task<PaginatedResultDto<GetCredentialsResponseDto>> GetCredentials(string authenticationToken, string searchCriteria = null);
        Task<GetCredentialsResponseDto> CreateCredential(string authenticationToken, CreateCredentialRequestDto credentialRequest);
        Task<PaginatedResultDto<GetProjectsRequestDto>> GetProjects(string authenticationToken, string searchCriteria = null);
        Task<GetProjectsRequestDto> CreateProject(string authenticationToken, CreateProjectRequestDto credentialRequest);
        Task<PaginatedResultDto<GetJobResponseDto>> GetJobs(string authenticationToken, string searchCriteria = null);
        Task<string> CancelJob(string authenticationToken, int idJob);
        Task<PaginatedResultDto<GetJobEventsResponseDto>> GetJobEvents(string authenticationToken, int idJob);
        Task<GetCanCancelResponseDto> CanCancelJob(string authenticationToken, int idJob);
        Task<GetCanCancelResponseDto> GetCanJobSchedule(string authenticationToken, int jobId);
        Task<string> DeleteProject(string authenticationToken, string projectId);
        Task<string> DeleteJobTemplate(string authenticationToken, string jobTemplateId);
    }
}