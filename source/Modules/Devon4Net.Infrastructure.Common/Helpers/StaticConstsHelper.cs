namespace Devon4Net.Infrastructure.Common.Helpers
{
    public static class StaticConstsHelper
    {
        public static string GetValue(Type type, string value)
        {
            return type.GetField(value).GetValue(null).ToString();
        }
    }
}