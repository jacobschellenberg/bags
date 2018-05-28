using System.Linq;
using System.Text.RegularExpressions;

namespace Bags.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNumeric(this string input)
        {
            return int.TryParse(input, out int number);
        }

        public static string[] SplitByWord(this string input)
        {
            MatchCollection matches = Regex.Matches(input, @"\b(?:[a-z]{2,}|[ai])\b");

            var words = from m in matches.Cast<Match>()
                        where !string.IsNullOrEmpty(m.Value) && !m.Value.IsNumeric()
                        select m.Value.Replace("'", "");

            return words.ToArray();
        }
    }
}
