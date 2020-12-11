using System;

namespace Excalibur.Common.Extensions
{
    /// <summary>
    /// This class contains a few useful extensions on long. 
    /// </summary>
    public static class LongExtensions
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Returns a DateTime from milliseconds since Epoch
        /// </summary>
        /// <param name="unixLong">The milliseconds from epoch</param>
        /// <returns>A parsed DateTime</returns>
        public static DateTime FromUnixTimeInMilliseconds(this long unixLong)
        {
            return Epoch.AddMilliseconds(unixLong);
        }

        /// <summary>
        /// Returns a DateTime from seconds since Epoch
        /// </summary>
        /// <param name="unixLong">The seconds from epoch</param>
        /// <returns>A parsed DateTime</returns>
        public static DateTime FromUnixTimeInSeconds(this long unixLong)
        {
            return Epoch.AddSeconds(unixLong);
        }
    }
}