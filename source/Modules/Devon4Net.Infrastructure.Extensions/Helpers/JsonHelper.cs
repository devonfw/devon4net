using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Devon4Net.Infrastructure.Extensions.Helpers
{
    public class JsonHelper : IJsonHelper
    {
        private JsonSerializerOptions JsonSerializerOptions { get; set; }

        public JsonHelper()
        {
            JsonSerializerOptions = null;
        }

        public void SetJsonSerializerOptions(JsonSerializerOptions jsonSerializerOptions)
        {
            JsonSerializerOptions = jsonSerializerOptions;
        }

        public async Task<string> SerializeAsync<T>(T input)
        {
            var stream = new MemoryStream();
            await JsonSerializer.SerializeAsync<T>(stream, input, JsonSerializerOptions);
            
            stream.Position = 0;
            using var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync();
        }

        public string Serialize<T>(T input)
        {
            var stream = new MemoryStream();
            return JsonSerializer.Serialize<T>(input, JsonSerializerOptions);
        }

        public async Task<T> DeserializeAsync<T>(string jsonObjectDefinition)
        {
            var reader = new MemoryStream(Encoding.UTF8.GetBytes(jsonObjectDefinition));
            return await JsonSerializer.DeserializeAsync<T>(reader, JsonSerializerOptions);
        }
    }
}
