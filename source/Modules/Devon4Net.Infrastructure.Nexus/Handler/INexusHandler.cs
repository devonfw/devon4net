using Devon4Net.Infrastructure.Nexus.Model.Assets;
using Devon4Net.Infrastructure.Nexus.Model.Components;
using Devon4Net.Infrastructure.Nexus.Model.Components.UploadComponent;
using Devon4Net.Infrastructure.Nexus.Model.Repositories.Base;

namespace Devon4Net.Infrastructure.Nexus.Handler
{
    public interface INexusHandler
    {
        #region Get

        /// <summary>
        /// Get components by repository name
        /// </summary>
        /// <param name="repositoryName"></param>
        Task<IList<NexusComponent>> GetComponents(string repositoryName);

        /// <summary>
        /// Get components by repository name and component group
        /// </summary>
        /// <param name="repositoryName"></param>
        /// <param name="componentGroup"></param>
        Task<IList<NexusComponent>> GetComponents(string repositoryName, string componentGroup);

        /// <summary>
        /// Get component by repository name and component name
        /// </summary>
        /// <param name="repositoryName"></param>
        /// <param name="componentName"></param>
        Task<NexusComponent> GetComponent(string repositoryName, string componentName);

        /// <summary>
        /// Get component by component id
        /// </summary>
        /// <param name="componentId"></param>
        Task<NexusComponent> GetComponent(string componentId);

        /// <summary>
        /// Get assets by repository name
        /// </summary>
        /// <param name="repositoryName"></param>
        Task<IList<Asset>> GetAssets(string repositoryName);

        /// <summary>
        /// Get assets by repository name and asset group
        /// </summary>
        /// <param name="repositoryName"></param>
        /// <param name="assetGroup"></param>
        Task<IList<Asset>> GetAssets(string repositoryName, string assetGroup);

        /// <summary>
        /// Get asset by repository name and asset name
        /// </summary>
        /// <param name="repositoryName"></param>
        /// <param name="assetName"></param>
        Task<Asset> GetAsset(string repositoryName, string assetName);

        /// <summary>
        /// Get asset by asset id
        /// </summary>
        /// <param name="assetId"></param>
        Task<Asset> GetAsset(string assetId);

        #endregion

        #region Download

        /// <summary>
        /// Download asset
        /// </summary>
        /// <param name="repositoryName"></param>
        /// <param name="assetName"></param>
        Task<string> DownloadAsset(string repositoryName, string assetName);

        #endregion

        #region Upload

        /// <summary>
        /// Upload new component
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uploadComponent"></param>
        Task UploadComponent<T>(T uploadComponent) where T : UploadComponent;

        #endregion

        #region Delete
        /// <summary>
        /// Delete component
        /// </summary>
        /// <param name="componentId"></param>
        Task DeleteComponent(string componentId);

        /// <summary>
        /// Delete asset
        /// </summary>
        /// <param name="assetId"></param>
        Task DeleteAsset(string assetId);

        /// <summary>
        /// Delete repository by repository name
        /// </summary>
        /// <param name="repositoryName"></param>
        Task DeleteRepository(string repositoryName);
        #endregion

        #region Create Repository

        /// <summary>
        /// Create new repository
        /// </summary>
        /// <param name="repositoryDto"></param>
        Task CreateRepository<T>(T repositoryDto) where T : NexusRepository;

        #endregion
    }
}