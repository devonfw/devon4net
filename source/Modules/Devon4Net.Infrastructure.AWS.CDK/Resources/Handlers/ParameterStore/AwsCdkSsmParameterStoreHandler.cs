using Amazon.CDK.AWS.SSM;
using Constructs;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.ParameterStore
{
    public class AwsCdkSsmParameterStoreHandler : AwsCdkBaseHandler, IAwsCdkSsmParameterStoreHandler
    {
        public AwsCdkSsmParameterStoreHandler(Construct scope, string applicationName, string enviromentName, string region) : base(scope, applicationName, enviromentName, region) { }

        public IParameter LocateParameterFromName(string identification, string paramterName)
        {
            return StringParameter.FromStringParameterName(Scope, identification, paramterName);
        }

        public IParameter Create(string parameterId, string parameterName, string value, ParameterDataType type = ParameterDataType.TEXT, ParameterTier tier = ParameterTier.STANDARD, string description = null)
        {
            var parameterProps = new StringParameterProps
            {
                DataType =  type,
                StringValue = value,
                Description = string.IsNullOrWhiteSpace(description) ? null : description,
                Tier = tier,
                ParameterName = parameterName
            };

            return new StringParameter(Scope, parameterId, parameterProps);
        }
    }
}
