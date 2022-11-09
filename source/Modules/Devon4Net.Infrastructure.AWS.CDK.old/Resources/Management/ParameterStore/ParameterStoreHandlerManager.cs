using Amazon.CDK.AWS.SSM;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Management.ParameterStore
{
    public partial class AwsCdkHandlerManager : IParameterStoreHandlerManager
    {
        public IParameter LocateParameterFromName(string identification, string parameterName)
        {
            return HandlerResources.AwsCdkSsmParameterStoreHandler.LocateParameterFromName(identification, parameterName);
        }

        public IParameter AddParameter(string parameterId, string parameterName, string value, ParameterType type = ParameterType.STRING, ParameterTier tier = ParameterTier.STANDARD, string description = null)
        {
            return HandlerResources.AwsCdkSsmParameterStoreHandler.Create(parameterId, parameterName, value, type, tier, description);
        }
    }
}
