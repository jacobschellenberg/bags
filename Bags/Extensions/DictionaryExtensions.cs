using System.Collections.Generic;
using System.Linq;

namespace Bags.Extensions
{
    public static class DictionaryExtensions
    {
        public static IOrderedEnumerable<KeyValuePair<T1, T2>> SortByValue<T1, T2>(this Dictionary<T1, T2> dictionary)
        {
            return from pair in dictionary
                   orderby pair.Value descending
                   select pair;
        }

        public static void SetOrIncrement<T>(this Dictionary<T, int> dictionary, T key)
        {
            if (dictionary.TryGetValue(key, out int value))
            {
                dictionary[key] = value + 1;
            }
            else
            {
                dictionary[key] = 1;
            }
        }
    }
}
