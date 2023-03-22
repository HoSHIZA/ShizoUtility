using System;
using System.Globalization;

namespace ShizoGames.Utilities
{
    /// <summary>
    /// Provides methods to format file sizes into human-readable strings with appropriate size suffixes.
    /// </summary>
    public static class SizeFormatter
    {
        private static readonly string[] _sizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

        /// <summary>
        /// Returns the formatted size in bytes with the appropriate suffix.
        /// </summary>
        /// <param name="value">The value in bytes to format.</param>
        /// <param name="decimalPlaces">The number of decimal places to round the formatted string to.</param>
        /// <remarks>https://stackoverflow.com/questions/14488796/does-net-provide-an-easy-way-convert-bytes-to-kb-mb-gb-etc</remarks>
        /// <returns>The formatted string with the appropriate size suffix (KB, MB, GB, etc.).</returns>
        public static string FromBytes(long value, int decimalPlaces = 1)
        {
            if (value < 0)
            {
                return $"-{FromBytes(-value, decimalPlaces)}";
            }

            var i = 0;
            var dValue = (decimal)value;

            while (Math.Round(dValue, decimalPlaces) >= 1000)
            {
                dValue /= 1024;
                i++;
            }

            return $"{dValue.ToString($"N{decimalPlaces.ToString()}", CultureInfo.CurrentCulture)} {_sizeSuffixes[i]}";
        }

        /// <summary>
        /// Returns the formatted size in kilobytes with the appropriate suffix.
        /// </summary>
        /// <param name="value">The value in kilobytes to format.</param>
        /// <param name="decimalPlaces">The number of decimal places to round the formatted string to.</param>
        /// <returns>The formatted string with the appropriate size suffix (KB, MB, GB, etc.).</returns>
        public static string FromKilobytes(long value, int decimalPlaces = 1)
        {
            return FromBytes(value * 1024, decimalPlaces);
        }

        /// <summary>
        /// Returns the formatted size in megabytes with the appropriate suffix.
        /// </summary>
        /// <param name="value">The value in megabytes to format.</param>
        /// <param name="decimalPlaces">The number of decimal places to round the formatted string to.</param>
        /// <returns>The formatted string with the appropriate size suffix (KB, MB, GB, etc.).</returns>
        public static string FromMegabytes(long value, int decimalPlaces = 1)
        {
            return FromBytes(value * 1024 * 1024, decimalPlaces);
        }

        /// <summary>
        /// Returns the formatted size in gigabytes with the appropriate suffix.
        /// </summary>
        /// <param name="value">The value in gigabytes to format.</param>
        /// <param name="decimalPlaces">The number of decimal places to round the formatted string to.</param>
        /// <returns>The formatted string with the appropriate size suffix (KB, MB, GB, etc.).</returns>
        public static string FromGigabytes(long value, int decimalPlaces = 1)
        {
            return FromBytes(value * 1024 * 1024 * 1024, decimalPlaces);
        }
    }
}