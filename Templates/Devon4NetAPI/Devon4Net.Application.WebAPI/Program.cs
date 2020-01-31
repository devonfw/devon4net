using Devon4Net.Application.WebAPI.Configuration.Application;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Devon4Net.Application.WebAPI
{
    /// <summary>
    /// devonfw template
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main devonfw program. You can use the dotnet way adding .InitializeDevonFw() to the default WebHost.CreateDefaultBuilder
        /// or use directly Devonfw.Configure<Startup>(args); to create automatically the webhost
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            // Please use
            // Devonfw.Configure<Startup>(args);
            // Or : 

            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .InitializeDevonFw()
                .Build()
                .Run();
        }
    }
}