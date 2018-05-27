using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace BagsEngine
{
    public class WordProcessor
    {
        private static Dictionary<string, int> _totalWordCount = new Dictionary<string, int>();
        private static Dictionary<string, bool> _stops = new Dictionary<string, bool>
        {
            { "20Settings", true },
            { "20hodnett", true },
            { "desktop", true },
            { "c", true },
            { "file", true },
            { "documents", true },
            { "20and", true },
            { "20settings", true },
            { "20confessor", true },
            { "html", true },
            { "pm", true },
            { "bookl", true },
            { "eng_1", true },
            { "l", true },
            { "ci", true },
            { "program", true },
            { "20files", true },
            { "20of", true },
            { "20truth", true },
            { "20phantom", true },
            { "emule", true },
            { "s", true },
            { "t", true },

            { "a", true },
            { "about", true },
            { "above", true },
            { "across", true },
            { "after", true },
            { "afterwards", true },
            { "again", true },
            { "against", true },
            { "all", true },
            { "almost", true },
            { "alone", true },
            { "along", true },
            { "already", true },
            { "also", true },
            { "although", true },
            { "always", true },
            { "am", true },
            { "among", true },
            { "amongst", true },
            { "amount", true },
            { "an", true },
            { "and", true },
            { "another", true },
            { "any", true },
            { "anyhow", true },
            { "anyone", true },
            { "anything", true },
            { "anyway", true },
            { "anywhere", true },
            { "are", true },
            { "around", true },
            { "as", true },
            { "at", true },
            { "back", true },
            { "be", true },
            { "became", true },
            { "because", true },
            { "become", true },
            { "becomes", true },
            { "becoming", true },
            { "been", true },
            { "before", true },
            { "beforehand", true },
            { "behind", true },
            { "being", true },
            { "below", true },
            { "beside", true },
            { "besides", true },
            { "between", true },
            { "beyond", true },
            { "bill", true },
            { "both", true },
            { "bottom", true },
            { "but", true },
            { "by", true },
            { "call", true },
            { "can", true },
            { "cannot", true },
            { "cant", true },
            { "co", true },
            { "computer", true },
            { "con", true },
            { "could", true },
            { "couldnt", true },
            { "cry", true },
            { "de", true },
            { "describe", true },
            { "detail", true },
            { "did", true },
            { "didn", true },
            { "didnt", true },
            { "do", true },
            { "don", true },
            { "done", true },
            { "dont", true },
            { "down", true },
            { "due", true },
            { "during", true },
            { "each", true },
            { "eg", true },
            { "eight", true },
            { "either", true },
            { "eleven", true },
            { "else", true },
            { "elsewhere", true },
            { "empty", true },
            { "enough", true },
            { "etc", true },
            { "even", true },
            { "ever", true },
            { "every", true },
            { "everyone", true },
            { "everything", true },
            { "everywhere", true },
            { "except", true },
            { "few", true },
            { "fifteen", true },
            { "fify", true },
            { "fill", true },
            { "find", true },
            { "fire", true },
            { "first", true },
            { "five", true },
            { "for", true },
            { "former", true },
            { "formerly", true },
            { "forty", true },
            { "found", true },
            { "four", true },
            { "from", true },
            { "front", true },
            { "full", true },
            { "further", true },
            { "get", true },
            { "give", true },
            { "go", true },
            { "had", true },
            { "has", true },
            { "have", true },
            { "he", true },
            { "hence", true },
            { "her", true },
            { "here", true },
            { "hereafter", true },
            { "hereby", true },
            { "herein", true },
            { "hereupon", true },
            { "hers", true },
            { "herself", true },
            { "him", true },
            { "himself", true },
            { "his", true },
            { "how", true },
            { "however", true },
            { "hundred", true },
            { "i", true },
            { "ie", true },
            { "if", true },
            { "in", true },
            { "inc", true },
            { "indeed", true },
            { "interest", true },
            { "into", true },
            { "is", true },
            { "it", true },
            { "its", true },
            { "itself", true },
            { "keep", true },
            { "last", true },
            { "latter", true },
            { "latterly", true },
            { "least", true },
            { "less", true },
            { "like", true },
            { "ltd", true },
            { "made", true },
            { "many", true },
            { "may", true },
            { "me", true },
            { "meanwhile", true },
            { "might", true },
            { "mill", true },
            { "mine", true },
            { "more", true },
            { "moreover", true },
            { "most", true },
            { "mostly", true },
            { "move", true },
            { "much", true },
            { "must", true },
            { "my", true },
            { "myself", true },
            { "name", true },
            { "namely", true },
            { "neither", true },
            { "never", true },
            { "nevertheless", true },
            { "next", true },
            { "nine", true },
            { "no", true },
            { "nobody", true },
            { "none", true },
            { "nor", true },
            { "not", true },
            { "nothing", true },
            { "now", true },
            { "nowhere", true },
            { "of", true },
            { "off", true },
            { "often", true },
            { "on", true },
            { "once", true },
            { "one", true },
            { "only", true },
            { "onto", true },
            { "or", true },
            { "other", true },
            { "others", true },
            { "otherwise", true },
            { "our", true },
            { "ours", true },
            { "ourselves", true },
            { "out", true },
            { "over", true },
            { "own", true },
            { "part", true },
            { "per", true },
            { "perhaps", true },
            { "please", true },
            { "put", true },
            { "rather", true },
            { "re", true },
            { "same", true },
            { "see", true },
            { "seem", true },
            { "seemed", true },
            { "seeming", true },
            { "seems", true },
            { "serious", true },
            { "several", true },
            { "she", true },
            { "should", true },
            { "show", true },
            { "side", true },
            { "since", true },
            { "sincere", true },
            { "six", true },
            { "sixty", true },
            { "so", true },
            { "some", true },
            { "somehow", true },
            { "someone", true },
            { "something", true },
            { "sometime", true },
            { "sometimes", true },
            { "somewhere", true },
            { "still", true },
            { "such", true },
            { "system", true },
            { "take", true },
            { "ten", true },
            { "than", true },
            { "that", true },
            { "the", true },
            { "their", true },
            { "them", true },
            { "themselves", true },
            { "then", true },
            { "thence", true },
            { "there", true },
            { "thereafter", true },
            { "thereby", true },
            { "therefore", true },
            { "therein", true },
            { "thereupon", true },
            { "these", true },
            { "they", true },
            { "thick", true },
            { "thin", true },
            { "third", true },
            { "this", true },
            { "those", true },
            { "though", true },
            { "three", true },
            { "through", true },
            { "throughout", true },
            { "thru", true },
            { "thus", true },
            { "to", true },
            { "together", true },
            { "too", true },
            { "top", true },
            { "toward", true },
            { "towards", true },
            { "twelve", true },
            { "twenty", true },
            { "two", true },
            { "un", true },
            { "under", true },
            { "until", true },
            { "up", true },
            { "upon", true },
            { "us", true },
            { "very", true },
            { "via", true },
            { "was", true },
            { "we", true },
            { "well", true },
            { "were", true },
            { "what", true },
            { "whatever", true },
            { "when", true },
            { "whence", true },
            { "whenever", true },
            { "where", true },
            { "whereafter", true },
            { "whereas", true },
            { "whereby", true },
            { "wherein", true },
            { "whereupon", true },
            { "wherever", true },
            { "whether", true },
            { "which", true },
            { "while", true },
            { "whither", true },
            { "who", true },
            { "whoever", true },
            { "whole", true },
            { "whom", true },
            { "whose", true },
            { "why", true },
            { "will", true },
            { "with", true },
            { "within", true },
            { "without", true },
            { "would", true },
            { "yet", true },
            { "you", true },
            { "your", true },
            { "yours", true },
            { "yourself", true },
            { "yourselves", true }
        };

        private static bool _showLogs = true;
        private static bool _showVerboseLogs = false;

        public static void ProcessFiles(string loadPath, string savePath, List<string> filePaths)
        {
            Log("Processing Files...", false);
            Log(isVerbose: false);

            for (int i = 0; i < filePaths.Count; i++)
            {
                ProcessFile(filePaths[i], savePath);
            }

            Log();
            Log("File Processing Complete.");

            Log("Processing Total Words...");
            WriteProcessedFile(_totalWordCount, 50, "total", savePath);

            Log();
            Log("Processing Total Words Complete.");

            Log(isVerbose: false);
            Log("File Processing Complete.", false);
        }

        private static void ProcessFile(string filePath, string savePath)
        {
            Log("Processing File: " + filePath + "...", false);

            var wordCount = new Dictionary<string, int>();
            var reader = new StreamReader(filePath);
            var text = reader.ReadToEnd();

            reader.Close();

            var words = GetWords(text);

            Log("Character Count: " + text.Length);
            Log("Word Count: " + words.Length);

            for (int i = 0; i < words.Length; i++)
            {
                Log("Processing Word: " + words[i]);

                ProcessWord(wordCount, words[i]);
                ProcessWord(_totalWordCount, words[i]);
            }

            WriteProcessedFile(wordCount, 50, Path.GetFileNameWithoutExtension(filePath), savePath);

            Log(filePath + " Processing Completed.");
        }

        private static void ProcessWord(Dictionary<string, int> dictionary, string word)
        {
            if (dictionary.ContainsKey(word))
            {
                dictionary[word]++;
            }
            else
            {
                if (!_stops.ContainsKey(word.ToLower()))
                {
                    dictionary.Add(word, 1);
                }
            }
        }

        private static IOrderedEnumerable<KeyValuePair<T1, T2>> SortDictionary<T1, T2>(Dictionary<T1, T2> dictionary)
        {
            Log("Sorting dictionary...");

            return from pair in dictionary
                   orderby pair.Value descending
                   select pair;
        }

        private static void WriteProcessedFile<T1, T2>(Dictionary<T1, T2> dictionary, int maxWords, string fileName, string savePath)
        {
            Log("Writing Processed File: " + fileName);

            var sortedTotalWords = SortDictionary(dictionary);

            var totalWriter = new StreamWriter(savePath + fileName + ".json");
            totalWriter.Write(JsonConvert.SerializeObject(sortedTotalWords.Take(maxWords), Formatting.Indented));
            totalWriter.Close();

            Log(fileName + " written.");
        }

        private static string[] GetWords(string input)
        {
            Log("Getting words...");

            MatchCollection matches = Regex.Matches(input, @"\b(?:[a-z]{2,}|[ai])\b");

            var words = from m in matches.Cast<Match>()
                        where !string.IsNullOrEmpty(m.Value) && !IsNumeric(m.Value)
                        select TrimSuffix(m.Value);

            Log("Found: " + words.Count().ToString() + " words.");

            return words.ToArray();
        }

        private static string TrimSuffix(string word)
        {
            //Log("Trimming suffix from: " + word);

            int apostropheLocation = word.IndexOf('\'');
            if (apostropheLocation != -1)
            {
                word = word.Substring(0, apostropheLocation);
            }

            return word;
        }

        private static bool IsNumeric(string input)
        {
            //Log("Checking if: " + input + " is numeric...");

            bool isNumeric = int.TryParse(input, out int number);

            //Log(input + " is numeric: " + isNumeric.ToString());

            return isNumeric;
        }

        private static void Log(string message = "", bool isVerbose = true)
        {
            if (_showLogs && ((_showVerboseLogs && isVerbose) || !_showVerboseLogs && !isVerbose))
            {
                if (string.IsNullOrEmpty(message))
                {
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine(message);
                }
            }
        }
    }
}
