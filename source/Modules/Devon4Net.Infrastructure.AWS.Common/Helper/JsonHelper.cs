using System.Text.Json;

namespace Devon4Net.Infrastructure.AWS.Common.Helper
{
    public class JsonHelper : IJsonHelper
    {
        private const string BuiltInTypeObjectNames = "String, DateTime, DateTimeKind, DateTimeOffset, AsyncCallback, AttributeTargets, AttributeUsageAttribute, Boolean, Byte, Char, CharEnumerator, Base64FormattingOptions, DayOfWeek, DBNull, Decimal, Double, EnvironmentVariableTarget, EventHandler, GCCollectionMode, Guid, Int16, Int32, Int64, IntPtr, SByte, Single, TimeSpan, TimeZoneInfo, TypeCode, UInt16, UInt32, UInt64, UIntPtr";

        public T Deserialize<T>(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return default;
            }

            if (BuiltInTypeObjectNames.Contains(typeof(T).Name))
            {
                return (T)Convert.ChangeType(input, typeof(T));
            }

            return JsonSerializer.Deserialize<T>(input);
        }

        public async Task<string> SerializeAsync<T>(T input)
        {
            using var stream = new MemoryStream();
            await JsonSerializer.SerializeAsync(stream, input, input.GetType()).ConfigureAwait(false);
            stream.Position = 0;
            using var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync().ConfigureAwait(false);
        }

        public string Serialize<T>(T input)
        {
            return JsonSerializer.Serialize(input);
        }
    }
}
