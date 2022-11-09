using Amazon.CDK.AWS.SSM;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.ParameterStore
{
    public interface IAwsCdkSsmParameterStoreHandler
    {
        IParameter Create(string parameterId, string parameterName, string value, ParameterDataType type = ParameterDataType.TEXT, ParameterTier tier = ParameterTier.STANDARD, string description = null);
        IParameter LocateParameterFromName(string identification, string paramterName);
    }
}