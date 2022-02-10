namespace Devon4Net.Infrastructure.Extensions
{
    using System;

    public static class GuidExtension
    {
        public static bool IsNullOrEmptyGuid(this Guid? guid )
        {
            return guid == null || guid == default(Guid) || guid == Guid.Empty;
        }

        public static bool IsNullOrEmptyGuid(this Guid guid)
        {
            return guid == default(Guid) || guid == Guid.Empty;
        }

        public static string GuidToString(this Guid guid)
        {
            if (guid.IsNullOrEmptyGuid()) throw new ArgumentException("Guid can't be null");

            return guid.ToString();
        }

        public static Guid StringToGuid(this string guid)
        {
            if (string.IsNullOrEmpty(guid)) throw new ArgumentException("Guid can't be null");
            Guid.TryParse(guid, out Guid result);
            if (result.IsNullOrEmptyGuid()) throw new ArgumentException("Parse error");
            return result;
        }

        public static bool IsValid(this string guid)
        {
            if (string.IsNullOrEmpty(guid)) return false;
            return Guid.TryParse(guid, out Guid myGuid);
        }

    }
}