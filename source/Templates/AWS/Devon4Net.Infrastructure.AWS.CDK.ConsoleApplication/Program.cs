using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System;
using System.Linq;
using Devon4Net.Infrastructure.AWS.CDK.Options.Global;
using Devon4Net.Infrastructure.AWS.CDK.Stack;
using Devon4Net.Infrastructure.Common.Configuration;
using Devon4Net.Infrastructure.AWS.Common.Constants;
using Devon4Net.Infrastructure.Common.Constants;

namespace Cdk
{
    public static class Program
    {
        private static string AwsAccount { get; set; }
        private static string AwsRegion { get; set; }
        private static List<CdkOptions> CdkOptions { get; set; }
        private static readonly DevonfwConfigurationBuilder DevonfwConfigurationBuilder = new();

        public static void Main(string[] args)
        {
            if (args?.Any() == true)
            {
                AwsAccount = args.Length >= 1 ? args[0] : Environment.GetEnvironmentVariable(AwsConstants.CDK_DEFAULT_ACCOUNT);
                AwsRegion = args.Length == 2 ? args[1] : Environment.GetEnvironmentVariable(AwsConstants.CDK_DEFAULT_REGION);
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
            CdkOptions = DevonfwConfigurationBuilder.Configuration.GetSection(OptionsDefinition.CdkOptions).Get<List<CdkOptions>>();
        }
    }
}
