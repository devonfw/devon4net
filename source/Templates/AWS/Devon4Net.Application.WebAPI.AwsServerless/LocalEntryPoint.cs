using Devon4Net.Infrastructure.AWS.Serverless;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Devon4Net.Application.WebAPI.AwsServerless
{
    /// <summary>
    /// The Main function can be used to run the ASP.NET Core application locally using the Kestrel webserver.
    /// </summary>
    public class LocalEntryPoint
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            //var chain = new Amazon.Runtime.CredentialManagement.CredentialProfileStoreChain();
            //chain.TryGetProfile("YOUR AWS PROFILE", out var profile);
            //var credentials = profile.GetAWSCredentials(profile.CredentialProfileStore);

            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.InitializeDevonFwAws();
                });
                //.ConfigureAppConfiguration((context, configurationBuilder) =>
                //{
                //    //configurationBuilder.AddSecretsHandler(credentials,profile.Region);
                //    configurationBuilder.AddSecretsHandler();
                //});
        }
    }
}
