using Devon4Net.Infrastructure.Common.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devon4Net.Infrastructure.Common.Application.ApplicationTypes.Console
{
    public abstract class DevonfwConsole
    {
        protected abstract void ConfigureServices(IServiceCollection services);

        protected DevonfwConfigurationBuilder DevonfwConfigurationBuilder { get; set; }
        protected IServiceProvider ServiceProvider { get; set; }
        protected IServiceCollection _serviceCollection = new ServiceCollection();

        protected DevonfwConsole()
        {
            DevonfwConfigurationBuilder = new DevonfwConfigurationBuilder();
            SetupServiceActions();
            FinalizeSetupServiceProviderActions();
        }

        public void GetConfigurationObjects(out IConfigurationRoot configuration, out IServiceCollection serviceCollection)
        {
            configuration = DevonfwConfigurationBuilder.ConfigurationBuilder.Build();
            serviceCollection = _serviceCollection;
        }

        private void SetupServiceActions()
        {
            ConfigureServices(_serviceCollection);
            _serviceCollection.AddOptions();
            _serviceCollection.AddSingleton(DevonfwConfigurationBuilder.ConfigurationBuilder.Build());
            _serviceCollection.AddSingleton(_serviceCollection);
        }


        private void FinalizeSetupServiceProviderActions()
        {
            ServiceProvider = _serviceCollection.BuildServiceProvider();
        }
    }
}
