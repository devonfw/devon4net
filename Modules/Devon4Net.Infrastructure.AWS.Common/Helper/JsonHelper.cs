using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Devon4Net.Infrastructure.AWS.Common.Helper
{
    public class JsonHelper : IJsonHelper
    {
        private const string BuiltInTypeObjectNames = "String, DateTime, DateTimeKind, DateTimeOffset, AsyncCallback, AttributeTargets, AttributeUsageAttribute, Boolean, Byte, Char, CharEnumerator, Base64FormattingOptions, DayOfWeek, DBNull, Decimal, Double, EnvironmentVariableTarget, EventHandler, GCCollectionMode, Guid, Int16, Int32, Int64, IntPtr, SByte, Single, TimeSpan, TimeZoneInfo, TypeCode, UInt16, UInt32, UInt64, UIntPtr";

        public T Deserialize<T>(string input)
        {
            return string.IsNullOrEmpty(input)
                ? default
                : BuiltInTypeObjectNames.Contains(typeof(T).Name) ? (T)Convert.ChangeType(input, typeof(T)) : JsonSerializer.Deserialize<T>(input);
        }

        public async Task<string> SerializeAsync<T>(T input)
        {
            using var stream = new MemoryStream();
            await JsonSerializer.SerializeAsync(stream, input, input.GetType());
            stream.Position = 0;
            using var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync();
        }

        public string Serialize<T>(T input)
        {
            return JsonSerializer.Serialize(input);
        }
    }
}
