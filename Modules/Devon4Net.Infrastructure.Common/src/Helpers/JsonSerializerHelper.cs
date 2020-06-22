using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Devon4Net.Infrastructure.Common.Helpers
{
    public static class JsonSerializerHelper
    {
        public static async Task<string> Serialize<T>(T input)
        {
            string result;
            await using (var stream = new MemoryStream())
            { 
                await JsonSerializer.SerializeAsync(stream, input);
                
                stream.Position = 0;

                using var reader = new StreamReader(stream);
                result = await reader.ReadToEndAsync();
            }

            return result;
        }
    }
}
