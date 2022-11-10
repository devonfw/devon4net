using Devon4Net.Infrastructure.Common.Application.ApplicationTypes.Console;
using Devon4Net.Infrastructure.Logger;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devon4Net.Application.Console
{
    public class ConsoleExtension : DevonfwConsole
    {
        protected override void ConfigureServices(IServiceCollection services)
        {
            services.SetupLog(DevonfwConfigurationBuilder.Configuration);
        }
    }
}
