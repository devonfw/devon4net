using System.IO;
using fyiReporting.CRI;
using fyiReporting.RDL;
using ICSharpCode.SharpZipLib.GZip;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace OASP4Net.Application.WebApi
{
    public class Program
    {
        private static ConfigManager ConfigurationManager{ get; set; }
        public static void Main(string[] args)
        {
            ConfigurationManager = new ConfigManager();
            RdlEngineConfig.RdlEngineConfigInit();
            var a = new QrCode();
            var b = GZipConstants.FCOMMENT;
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
