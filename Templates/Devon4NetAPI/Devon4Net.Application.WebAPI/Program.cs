using System.Threading.Tasks;
using Devon4Net.Application.WebAPI.Configuration.Application;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Devon4Net.Application.WebAPI
{
    /// <summary>
    /// devonfw template
    /// </summary>
    public static class Program
    {
        public static Task Main(string[] args)
        {
            return CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.InitializeDevonFw();
                });
    }
}