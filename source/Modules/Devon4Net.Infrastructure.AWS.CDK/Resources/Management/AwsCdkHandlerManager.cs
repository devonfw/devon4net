using Amazon.CDK;
using Constructs;
using System;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Management
{
    public partial class AwsCdkHandlerManager : Amazon.CDK.Stack
    {
        protected Construct Scope { get; set; }
        protected string ApplicationName { get; set; }
        protected string EnvironmentName { get; set; }
        protected ResourceCollectionHandler HandlerResources { get; set; }
        protected IStackProps StackProps { get; set; }

        public AwsCdkHandlerManager(Construct scope, string id, string applicationName, string environmentName, IStackProps stackProps) : base(scope, id, stackProps)
        {
            if (string.IsNullOrEmpty(applicationName) || string.IsNullOrEmpty(environmentName))
            {
                throw new ArgumentException("The application name or the environment name can not be null");
            }

            Scope = scope;
            ApplicationName = applicationName;
            EnvironmentName = environmentName;
            StackProps = stackProps;
            HandlerResources = new ResourceCollectionHandler(scope, applicationName, environmentName, stackProps);
        }
    }
}
