using System.Threading.Tasks;
using Devon4Net.Application.WebAPI.Configuration.Application;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Devon4Net.Application.WebAPI
{
    /// <summary>
    /// devonfw template
    /// </summary>
    public class Program
    {
        public static Task Main(string[] args)
        {
            return CreateHostBuilder(args).Build().RunAsync();
        }

        /// <summary>
        /// Main devonfw program. You can use the dotnet way adding .InitializeDevonFw() to the default WebHost.CreateDefaultBuilder
        /// or use directly Devonfw.Configure<Startup>(args); to create automatically the webhost
        /// </summary>
        /// <param name="args"></param>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            // Please use
            // Devonfw.Configure<Startup>(args);
            // Or : 
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.InitializeDevonFw();
                });
    }
}