using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System;
using ADC.PostNL.BuildingBlocks.AWSInitCDK.Operations;
using System.Linq;
using Devon4Net.Infrastructure.AWS.CDK.Options.Global;
using Devon4Net.Infrastructure.AWS.CDK.Stack;

namespace Cdk
{
    public static class Program
    {
        private static string AwsAccount { get; set; }
        private static string AwsRegion { get; set; }
        private static IConfiguration Configuration { get; set; }
        private static List<CdkOptions> CdkOptions { get; set; }

        public static void Main(string[] args)
        {
            if (args?.Any() == true)
            {
                AwsAccount = args.Length >= 1 ? args[0] : Environment.GetEnvironmentVariable("CDK_DEFAULT_ACCOUNT");
                AwsRegion = args.Length == 2 ? args[1] : Environment.GetEnvironmentVariable("CDK_DEFAULT_REGION");
            }

            #region AppSettings

            LoadConfigurationFiles();

            foreach (var stack in CdkOptions)
            {
                if (string.IsNullOrWhiteSpace(AwsAccount))
                {
                    AwsAccount = stack.ProvisionStack.AwsAccount;
                }

                if (string.IsNullOrWhiteSpace(AwsRegion))
                {
                    AwsRegion = stack.ProvisionStack.AwsRegion;
                }

                var defaultPsrovisionStack = new ProvisionStack(AwsAccount, AwsRegion, stack);
                defaultPsrovisionStack.Process();
            }

            #endregion
        }

        private static void LoadConfigurationFiles()
        {
            var file = FileOperationsHelper.GetFilesFromPath("appsettings.json")?.FirstOrDefault();
            if (file == null) throw new ArgumentException("No appsettings.json was provided");

            Configuration = new ConfigurationBuilder().AddJsonFile(file, true, true).Build();

            var environmentFileName = Configuration.GetSection("Environment").Value;

            var environmentFile = FileOperationsHelper.GetFilesFromPath($"appsettings.{environmentFileName}.json")?.FirstOrDefault();
            Configuration = new ConfigurationBuilder().AddConfiguration(Configuration).AddJsonFile(environmentFile, true, true).Build();

            CdkOptions = Configuration.GetSection("CdkOptions").Get<List<CdkOptions>>();
        }
    }
}
