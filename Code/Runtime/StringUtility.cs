using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShizoGames.ShizoUtility
{
    public static class StringUtility
    {
        public static string DivideIntoWordsByCapitalLetters(string input, params KeyValuePair<string, string>[] replacePairs)
        {
            if (input == null) return null;
            
            var words = new List<string>();
            var currentWord = new StringBuilder();

            foreach (var @char in input)
            {
                if (char.IsUpper(@char))
                {
                    if (currentWord.Length > 0 && !char.IsUpper(currentWord[currentWord.Length - 1]))
                    {
                        words.Add(currentWord.ToString());
                        currentWord.Clear();
                    }
                }

                currentWord.Append(@char);
            }

            if (currentWord.Length > 0)
            {
                words.Add(currentWord.ToString());
            }

            var result = string.Join(" ", words);
            
            if (replacePairs == null || replacePairs.Length == 0) return result;
            
            return replacePairs.Aggregate(result, (current, pair) => current.Replace(pair.Key, pair.Value));
        }
    }
}