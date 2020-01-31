using System.Threading.Tasks;
using Excalibur.Shared.Configuration;
using XLabs.Ioc;

namespace Excalibur.Shared.State
{
    /// <summary>
    /// Base class for managing state. 
    /// This class will initialize a <see cref="ConfigurationManager"/> and will implement certain methods from <see cref="IBaseState"/> to contain
    /// a default implementation.
    /// </summary>
    /// <typeparam name="TConfig">A config class that contains the configuration that contains persistable state</typeparam>
    public abstract class BaseState<TConfig> : IBaseState
        where TConfig : new()
    {
        /// <summary>
        /// The configuration manager that manages the Config
        /// </summary>
        protected IConfigurationManager ConfigurationManager { get; set; }

        /// <summary>
        /// Initializes the BaseState.
        /// </summary>
        protected BaseState()
        {
            ConfigurationManager = Resolver.Resolve<IConfigurationManager>();
        }

        /// <summary>
        /// The config that contains persistable state. 
        /// </summary>
        protected TConfig Config { get; set; } = new TConfig();

        /// <summary>
        /// Initialize and load the state
        /// </summary>
        /// <returns>An await-able task</returns>
        public virtual async Task InitAndLoadAsync()
        {
            Config = await ConfigurationManager.LoadAsync<TConfig>().ConfigureAwait(false);

            await Initialize().ConfigureAwait(false);
        }

        /// <summary>
        /// Initialize method that will be used for loading default thing if needed.
        /// </summary>
        /// <returns>An awaitable task</returns>
        protected virtual Task Initialize()
        {
            // Add custom things here
            // Like default images
            return Task.CompletedTask;
        }

        /// <summary>
        /// Save the state.
        /// </summary>
        /// <returns>An await-able task</returns>
        public virtual async Task SaveAsync()
        {
            await ConfigurationManager.SaveAsync(Config).ConfigureAwait(false);
        }
    }
}
