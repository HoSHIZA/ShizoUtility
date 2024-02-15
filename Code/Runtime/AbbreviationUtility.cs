namespace KDebugger.Plugins.ShizoGames.ShizoUtility
{
    /// <summary>
    /// Provides utility methods for abbreviating numeric values.
    /// </summary>
    public static class AbbreviationUtility
    {
        private static readonly string[] _abbreviations = { "", "k", "m", "b", "t", "q", "Q", "s", "S", "o", "n", "d", "U", "D" };
        private static readonly string[] _fullAbbreviations = { "", 
            "thousand", "million", "billion", "trillion", "quadrillion", 
            "quintillion", "sextillion", "septillion", "octillion", 
            "nonillion", "decillion", "undecillion", "duodecillion" 
        };
        
        /// <summary>
        /// Converts a large number to a more readable format with a metric abbreviation.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="useFullAbbreviation">If true, full abbreviations such as "thousand" and "million" are used instead of single letters.</param>
        /// <param name="decimalPlaces">The number of decimal places to display in the result.</param>
        /// <returns>The formatted string representation of the given value with a metric abbreviation.</returns>
        public static string AbbreviateNumber(double value, bool useFullAbbreviation = false, int decimalPlaces = 2)
        {
            var abbreviations = useFullAbbreviation ? _fullAbbreviations : _abbreviations;
            
            var suffixIndex = 0;
            while (value >= 1000 && suffixIndex < abbreviations.Length - 1)
            {
                value /= 1000;
                suffixIndex++;
            }
            
            var suffix = abbreviations[suffixIndex];
            var formatString = decimalPlaces > 0 ? $"F{decimalPlaces.ToString()}" : "F0";
            
            return $"{value.ToString(formatString)}{suffix}";
        }
    }
}