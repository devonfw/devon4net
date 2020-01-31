using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using OASP4Net.Application.Configuration;
using Serilog;

namespace OASP4Net.Application.WebAPI
{
    public static class Program
    {
        private static ConfigurationManager ConfigurationManager { get; set; }
        public static void Main(string[] args)
        {
            ConfigurationManager = new ConfigurationManager();
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                            .UseStartup<Startup>()
                            .UseKestrel()
                            .UseContentRoot(Directory.GetCurrentDirectory())
                            .UseApplicationInsights()
                            .UseUrls(ConfigurationManager.LocalKestrelUrl)
                            .UseSerilog()
                            .Build();
        }
    }
}
