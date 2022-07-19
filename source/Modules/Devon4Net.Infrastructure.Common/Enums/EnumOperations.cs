using System.Runtime.Serialization;

namespace Devon4Net.Infrastructure.Common.Enums
{
    public static class EnumOperations
    {
        public static string GetEnumMemberAttrValue(Type enumType, object enumVal)
        {
            var memInfo = enumType.GetMember(enumVal.ToString());
            var attr = memInfo[0].GetCustomAttributes(false).OfType<EnumMemberAttribute>().FirstOrDefault();
            if (attr != null)
            {
                return attr.Value;
            }

            return null;
        }
    }
}
