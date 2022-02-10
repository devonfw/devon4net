namespace Devon4Net.Infrastructure.Extensions
{
    using System;

    public static class GuidExtension
    {
        /// <summary>
        /// Returns true if it is a null or empty guid
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static bool IsNullOrEmptyGuid(this Guid guid)
        {
            return guid.Equals(Guid.Empty) || guid.Equals(default);  //NOSONAR false positive
        }

        public static string GuidToString(this Guid guid)
        {
            if (guid.IsNullOrEmptyGuid()) throw new ArgumentException("Guid can't be null");

            return guid.ToString();
        }

        public static Guid StringToGuid(this string guid)
        {
            if (string.IsNullOrEmpty(guid)) throw new ArgumentException("Guid can't be null");
            _ = Guid.TryParse(guid, out Guid result);
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