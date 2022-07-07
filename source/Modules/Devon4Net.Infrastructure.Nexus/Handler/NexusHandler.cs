using Devon4Net.Infrastructure.CircuitBreaker.Common.Enums;
using Devon4Net.Infrastructure.CircuitBreaker.Handlers;
using Devon4Net.Infrastructure.Common.Enums;
using Devon4Net.Infrastructure.Nexus.Constants;
using Devon4Net.Infrastructure.Nexus.Enum;
using Devon4Net.Infrastructure.Nexus.Model.Assets;
using Devon4Net.Infrastructure.Nexus.Model.Components;
using Devon4Net.Infrastructure.Nexus.Model.Components.UploadComponent;
using Devon4Net.Infrastructure.Nexus.Model.Repositories.Base;
using Devon4Net.Infrastructure.Nexus.Model.Responses;
using Devon4Net.Infrastructure.Nexus.Options;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;

namespace Devon4Net.Infrastructure.Nexus.Handler
{
    public class NexusHandler : INexusHandler
    {
        private readonly IHttpClientHandler _httpClientHandler;
        private readonly NexusOptions NexusOptions;

        public NexusHandler(IHttpClientHandler httpClientHandler, IOptions<NexusOptions> nexusOptions)
        {
            _httpClientHandler = httpClientHandler;
            NexusOptions = nexusOptions.Value;
        }

        #region Get

        /// <summary>
        /// Get components by repository name
        /// </summary>
        /// <param name="repositoryName"></param>
        public async Task<IList<NexusComponent>> GetComponents(string repositoryName)
        {
            var endpoint = $"{NexusConst.ComponentsUrl}?repository={repositoryName}";

            var response = await _httpClientHandler.Send<NexusResponse<NexusComponent>>(HttpMethod.Get, "Nexus", endpoint, null, MediaType.ApplicationJson, GetLoginHeaders(NexusOptions.UserName, NexusOptions.Password)).ConfigureAwait(false);

            return response.Items;
        }

        /// <summary>
        /// Get components by repository name and component group
        /// </summary>
        /// <param name="repositoryName"></param>
        /// <param name="componentGroup"></param>
        public async Task<IList<NexusComponent>> GetComponents(string repositoryName, string componentGroup)
        {
            var endpoint = $"{NexusConst.SearchUrl}?repository={repositoryName}&group=/{componentGroup}";

            var response = await _httpClientHandler.Send<NexusResponse<NexusComponent>>(HttpMethod.Get, "Nexus", endpoint, null, MediaType.ApplicationJson, GetLoginHeaders(NexusOptions.UserName, NexusOptions.Password)).ConfigureAwait(false);

            return response.Items;
        }

        /// <summary>
        /// Get component by repository name and component name
        /// </summary>
        /// <param name="repositoryName"></param>
        /// <param name="componentName"></param>
        public async Task<NexusComponent> GetComponent(string repositoryName, string componentName)
        {
            var endpoint = $"{NexusConst.SearchUrl}?repository={repositoryName}&name={componentName}";

            var response = await _httpClientHandler.Send<NexusResponse<NexusComponent>>(HttpMethod.Get, "Nexus", endpoint, null, MediaType.ApplicationJson, GetLoginHeaders(NexusOptions.UserName, NexusOptions.Password)).ConfigureAwait(false);

            return response.Items.FirstOrDefault();
        }

        /// <summary>
        /// Get component by component id
        /// </summary>
        /// <param name="componentId"></param>
        public Task<NexusComponent> GetComponent(string componentId)
        {
            return _httpClientHandler.Send<NexusComponent>(HttpMethod.Get, "Nexus", string.Concat(NexusConst.ComponentsUrl, "/", componentId), null, MediaType.ApplicationJson, GetLoginHeaders(NexusOptions.UserName, NexusOptions.Password));
        }

        /// <summary>
        /// Get assets by repository name
        /// </summary>
        /// <param name="repositoryName"></param>
        public async Task<IList<Asset>> GetAssets(string repositoryName)
        {
            var endpoint = $"{NexusConst.AssetsUrl}?repository={repositoryName}";

            var response = await _httpClientHandler.Send<NexusResponse<Asset>>(HttpMethod.Get, "Nexus", endpoint, null, MediaType.ApplicationJson, GetLoginHeaders(NexusOptions.UserName, NexusOptions.Password)).ConfigureAwait(false);

            return response.Items;
        }

        /// <summary>
        /// Get assets by repository name and asset group
        /// </summary>
        /// <param name="repositoryName"></param>
        /// <param name="assetGroup"></param>
        public async Task<IList<Asset>> GetAssets(string repositoryName, string assetGroup)
        {
            var endpoint = $"{NexusConst.SearchUrl}?repository={repositoryName}&group=/{assetGroup}";

            var response = await _httpClientHandler.Send<NexusResponse<Asset>>(HttpMethod.Get, "Nexus", endpoint, null, MediaType.ApplicationJson, GetLoginHeaders(NexusOptions.UserName, NexusOptions.Password)).ConfigureAwait(false);

            return response.Items;
        }

        /// <summary>
        /// Get asset by repository name and asset name
        /// </summary>
        /// <param name="repositoryName"></param>
        /// <param name="assetName"></param>
        public async Task<Asset> GetAsset(string repositoryName, string assetName)
        {
            var endpoint = $"{NexusConst.AssetsUrl}?repository={repositoryName}&name={assetName}";

            var response = await _httpClientHandler.Send<NexusResponse<Asset>>(HttpMethod.Get, "Nexus", endpoint, null, MediaType.ApplicationJson, GetLoginHeaders(NexusOptions.UserName, NexusOptions.Password)).ConfigureAwait(false);

            return response.Items.FirstOrDefault();
        }

        /// <summary>
        /// Get asset by asset id
        /// </summary>
        /// <param name="assetId"></param>
        public Task<Asset> GetAsset(string assetId)
        {
            return _httpClientHandler.Send<Asset>(HttpMethod.Get, "Nexus", string.Concat(NexusConst.AssetsUrl, "/", assetId), null, MediaType.ApplicationJson, GetLoginHeaders(NexusOptions.UserName, NexusOptions.Password));
        }

        #endregion

        #region Download

        /// <summary>
        /// Download asset
        /// </summary>
        /// <param name="repositoryName"></param>
        /// <param name="assetName"></param>
        public Task<string> DownloadAsset(string repositoryName, string assetName)
        {
            var endpoint = $"/repository/{repositoryName}/{assetName}";

            return _httpClientHandler.Send<string>(HttpMethod.Get, "Nexus", endpoint, null, MediaType.ApplicationJson, GetLoginHeaders(NexusOptions.UserName, NexusOptions.Password));
        }

        #endregion

        #region Upload

        /// <summary>
        /// Upload new component
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uploadComponent"></param>
        public Task UploadComponent<T>(T uploadComponent) where T : UploadComponent
        {
            var endpoint = $"{NexusConst.ComponentsUrl}?repository={uploadComponent.RepositoryName}";

            var foundFileContentType = new FileExtensionContentTypeProvider().TryGetContentType(uploadComponent.AssetPath, out var contentType);
            var fileContent = new ByteArrayContent(File.ReadAllBytes(uploadComponent.AssetPath));
            fileContent.Headers.ContentType = foundFileContentType ? new MediaTypeHeaderValue(contentType) : new MediaTypeHeaderValue("application/octet-stream");

            var form = uploadComponent.GetMultiPartFormData(fileContent);

            return _httpClientHandler.Send(HttpMethod.Post, "Nexus", endpoint, form, MediaType.MultipartFormData, GetLoginHeaders(NexusOptions.UserName, NexusOptions.Password));
        }

        #endregion

        #region Delete

        /// <summary>
        /// Delete component
        /// </summary>
        /// <param name="componentId"></param>
        public Task DeleteComponent(string componentId)
        {
            return _httpClientHandler.Send<NexusResponse<NexusComponent>>(HttpMethod.Delete, "Nexus", string.Concat(NexusConst.ComponentsUrl, "/", componentId), null, MediaType.ApplicationJson, GetLoginHeaders(NexusOptions.UserName, NexusOptions.Password));
        }

        /// <summary>
        /// Delete asset
        /// </summary>
        /// <param name="assetId"></param>
        public Task DeleteAsset(string assetId)
        {
            return _httpClientHandler.Send<NexusResponse<NexusComponent>>(HttpMethod.Delete, "Nexus", string.Concat(NexusConst.AssetsUrl, "/", assetId), null, MediaType.ApplicationJson, GetLoginHeaders(NexusOptions.UserName, NexusOptions.Password));
        }

        /// <summary>
        /// Delete repository by repository name
        /// </summary>
        /// <param name="repositoryName"></param>
        public Task DeleteRepository(string repositoryName)
        {
            return _httpClientHandler.Send(HttpMethod.Delete, "Nexus", string.Concat(NexusConst.RepositoriesUrl, repositoryName), null, MediaType.ApplicationJson, GetLoginHeaders(NexusOptions.UserName, NexusOptions.Password));
        }

        #endregion

        #region Create Repository

        /// <summary>
        /// Create new repository
        /// </summary>
        /// <param name="repositoryDto"></param>
        public Task CreateRepository<T>(T repositoryDto) where T : NexusRepository
        {
            var repoType = EnumOperations.GetEnumMemberAttrValue(typeof(RepositoryType), repositoryDto.GetType().Name).Split("-");

            return _httpClientHandler.Send(HttpMethod.Post, "Nexus", string.Concat(NexusConst.RepositoriesUrl, repoType[0], "/", repoType[1]), repositoryDto, MediaType.ApplicationJson, GetLoginHeaders(NexusOptions.UserName, NexusOptions.Password));
        }

        #endregion

        private static Dictionary<string, string> GetLoginHeaders(string userName, string password)
        {
            var authCredential = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{userName}:{password}"));
            return new Dictionary<string, string> { { "Authorization", $"Basic {authCredential}" } };
        }
    }
}