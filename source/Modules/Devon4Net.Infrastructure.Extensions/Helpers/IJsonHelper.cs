using System.Text.Json;
using System.Threading.Tasks;

namespace Devon4Net.Infrastructure.Extensions.Helpers
{
    public interface IJsonHelper
    {
        Task<string> SerializeAsync<T>(T input);
        string Serialize<T>(T input);
        Task<T> DeserializeAsync<T>(string jsonObjectDefinition);
    }
}