using Devon4Net.Application.WebAPI.Configuration.Application;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Devon4Net.Application.KafkaConsumer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
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
