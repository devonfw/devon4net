using System;
using Amazon.CDK;

namespace Devon4Net.Infrastructure.AWS.CDK.Handlers
{
    public class AwsCdkDefaultHandler
    {
        protected Construct Scope { get; set; }
        protected string ApplicationName { get; set; }
        protected string EnvironmentName { get; set; }

        protected AwsCdkDefaultHandler(Construct scope, string applicationName, string environmentName)
        {
            if (string.IsNullOrEmpty(applicationName) || string.IsNullOrEmpty(environmentName))
            {
                throw new ArgumentException("The application name or the environment name can not be null");
            }

            Scope = scope;
            ApplicationName = applicationName;
            EnvironmentName = environmentName;
        }
    }
}