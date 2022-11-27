using System.Text;
using System.Text.Json;
using Devon4Net.Infrastructure.Common.Helpers.Interfaces;

namespace Devon4Net.Infrastructure.Common.Helpers
{
    public class JsonHelper : IJsonHelper
    {
        private const string BuiltInTypes = "String, DateTime, DateTimeKind, DateTimeOffset, AsyncCallback, AttributeTargets, AttributeUsageAttribute, Boolean, Byte, Char, CharEnumerator, Base64FormattingOptions, DayOfWeek, DBNull, Decimal, Double, EnvironmentVariableTarget, EventHandler, GCCollectionMode, Guid, Int16, Int32, Int64, IntPtr, SByte, Single, TimeSpan, TimeZoneInfo, TypeCode, UInt16, UInt32, UInt64, UIntPtr";
        private readonly JsonSerializerOptions CamelJsonSerializerOptions = new () { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull };

        private JsonSerializerOptions JsonSerializerOptions { get; }

        public JsonHelper()
        {
            JsonSerializerOptions = null;
        }

        public JsonHelper(JsonSerializerOptions jsonSerializerOptions)
        {
            JsonSerializerOptions = jsonSerializerOptions;
        }

        public T Deserialize<T>(string input, bool useCamelCase= false)
        {
            if (string.IsNullOrEmpty(input))
            {
                return default;
            }

            if (BuiltInTypes.Contains(typeof(T).Name))
            {
                return (T)Convert.ChangeType(input, typeof(T));
            }

            return JsonSerializer.Deserialize<T>(input, useCamelCase ? CamelJsonSerializerOptions : null);
        }

        public List<T> Deserialize<T>(List<string> input, bool useCamelCase = false)
        {
            if (input == null || input.Count == 0)
            {
                return default;
            }

            var result = new List<T>();

            foreach (var item in input)
            {
                result.Add(Deserialize<T>(item, useCamelCase));
            }

            return result;
        }

        public async Task<string> Serialize<T>(T input)
        {
            var stream = new MemoryStream();
            await JsonSerializer.SerializeAsync<T>(stream, input, JsonSerializerOptions).ConfigureAwait(false);

            stream.Position = 0;
            using var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync().ConfigureAwait(false);
        }
        public string Serialize(object toPrint, bool useCamelCase = false)
        {
            return JsonSerializer.Serialize(toPrint, useCamelCase ? CamelJsonSerializerOptions : null);
        }

        public async Task<string> SerializeAsync<T>(T input)
        {
            using var stream = new MemoryStream();
            await JsonSerializer.SerializeAsync(stream, input, input.GetType()).ConfigureAwait(false);
            stream.Position = 0;
            using var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync().ConfigureAwait(false);
        }
    }
}
