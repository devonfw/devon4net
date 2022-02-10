using System.Collections.Generic;
using System.Threading.Tasks;

namespace Devon4Net.Infrastructure.AWS.DynamoDb.Extensions
{
    public interface IJsonHelper
    {
        Task<string> Serialize<T>(T input);
        string Serialize(object toPrint, bool useCamelCase = false);
        T Deserialize<T>(string input, bool useCamelCase = false);
        List<T> Deserialize<T>(List<string> input, bool useCamelCase = false);
    }
}