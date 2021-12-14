using Amazon.SimpleSystemsManagement.Model;

namespace Devon4Net.Infrastructure.AWS.ParameterStore
{
    public interface IAwsParameterStoreHandler : IDisposable
    {
        Task<List<ParameterMetadata>> GetAllParameters(CancellationToken cancellationToken = default);
        Task<string> GetParameterValue(string parameterName, CancellationToken cancellationToken = default);
    }
}