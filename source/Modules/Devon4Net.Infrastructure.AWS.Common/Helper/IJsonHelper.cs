namespace Devon4Net.Infrastructure.AWS.Common.Helper
{
    public interface IJsonHelper
    {
        Task<string> SerializeAsync<T>(T input);
        string Serialize<T>(T input);
        T Deserialize<T>(string input);
    }
}