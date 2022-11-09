using System.Text.Json;

namespace Devon4Net.Infrastructure.Common.Helpers.Interfaces
{
    public interface IJsonHelper
    {
        Task<string> Serialize<T>(T input);
        string Serialize(object toPrint, bool useCamelCase = false);
        Task<string> SerializeAsync<T>(T input);
        T Deserialize<T>(string input, bool useCamelCase = false);
        List<T> Deserialize<T>(List<string> input, bool useCamelCase = false);
    }
}