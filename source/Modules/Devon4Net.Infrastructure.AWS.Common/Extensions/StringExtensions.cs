namespace Devon4Net.Infrastructure.AWS.Common.Extensions
{
    public static class StringExtensions
    {
        public static byte[] ToByteArrayFromHexBinary(this string hexBinary)
        {
            if (hexBinary.Length % 2 != 0)
            {
                throw new ArgumentException("The hexBinary parameter must have an even length");
            }

            var result = new byte[hexBinary.Length / 2];

            for (var i = 0; i < hexBinary.Length; i += 2)
            {
                result[i / 2] = Convert.ToByte($"{hexBinary[i]}{hexBinary[i + 1]}", 16);
            }

            return result;
        }
    }
}
