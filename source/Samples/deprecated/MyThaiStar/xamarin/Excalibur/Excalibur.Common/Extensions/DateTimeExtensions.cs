using System;

namespace Excalibur.Common.Extensions
{
    /// <summary>
    /// This class contains a few useful extensions on Datetime. 
    /// </summary>
    public static class DateTimeExtensions
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Returns the time in milliseconds from Epoch
        /// </summary>
        /// <param name="time">The time to parse to milliseconds</param>
        /// <returns>The time in milliseconds since epoch</returns>
        public static long ToUnixTimeInMilliseconds(this DateTime time)
        {
            return (long)time.Subtract(Epoch).TotalMilliseconds;
        }

        /// <summary>
        /// Returns the time in seconds from Epoch
        /// </summary>
        /// <param name="time">The time to parse to seconds</param>
        /// <returns>The time in seconds since epoch</returns>
        public static long ToUnixTimeInSeconds(this DateTime time)
        {
            return (long)time.Subtract(Epoch).TotalSeconds;
        }
    }
}
