using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace OASP4Net.Application.Configuration.Startup
{
    public static class DataBaseConfiguration
    {
        public static void ConfigureDataBase(this IServiceCollection services, Dictionary<string,string> connectionStringDictionary)
        {
        }

        private static string GetDictionaryValue(Dictionary<string, string> connectionStringDictionary, string dictionaryKey)
        {
            var dictionaryValue = string.Empty;
            connectionStringDictionary.TryGetValue(dictionaryKey, out dictionaryValue);
            return dictionaryValue;
        }
    }
}
