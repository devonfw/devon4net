using System.Collections.Generic;

namespace Excalibur.Common.Extensions
{
    public static class DictionaryExtensions
    {
        // todo think this should be a bit more generic?
        public static void AddOrUpdate(this Dictionary<string, int> dictionary, string key, int value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
            }
            else
            {
                dictionary.Add(key, value);
            }
        }
    }
}
