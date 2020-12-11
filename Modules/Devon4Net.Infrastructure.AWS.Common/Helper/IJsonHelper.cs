namespace Devon4Net.Infrastructure.AWS.Common.Helper
{
    public interface IJsonHelper
    {
        string Serialize<T>(T input);
        T Deserialize<T>(string input);
    }
}