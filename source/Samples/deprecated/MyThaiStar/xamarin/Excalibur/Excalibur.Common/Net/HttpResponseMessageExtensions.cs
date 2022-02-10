using System.Threading.Tasks;
using Newtonsoft.Json;

// ReSharper disable once CheckNamespace
namespace System.Net.Http
{
    /// <summary>
    /// Contains extensions on HttpResponseMessage
    /// </summary>
    public static class HttpResponseMessageExtensions
    {
        /// <summary>
        /// <see cref="JsonSerializerSettings"/> that are used when converting
        /// </summary>
        public static JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };

        /// <summary>
        /// Allows converting some json response to an actual type T
        /// </summary>
        /// <typeparam name="T">The type to deserialize to</typeparam>
        /// <param name="response">An instance of <see cref="HttpResponseMessage"/></param>
        /// <returns>An instance of T that has been serialized from the response</returns>
        public static async Task<T> ConvertFromJsonResponse<T>(this HttpResponseMessage response)
        {
            var resultAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(resultAsString, JsonSerializerSettings);
        }
    }
}