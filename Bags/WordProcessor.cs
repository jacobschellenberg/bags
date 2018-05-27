using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace BagsEngine
{
    public class WordProcessor
    {
        private static Dictionary<string, int> _totalWordCount = new Dictionary<string, int>();

        private static bool _showLogs = true;
        private static bool _showVerboseLogs = false;
        private static List<string> _stopWords = new List<string>();

        public static void ProcessFiles(string loadPath, string savePath)
        {
            _stopWords = GetStopWords();

            Log(isVerbose: false);

            var filePaths = Directory.EnumerateFiles(loadPath).Where(file => Path.GetExtension(file).Equals(".txt", StringComparison.InvariantCultureIgnoreCase)).ToList();

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
                if (!_stopWords.Contains(word.ToLower()))
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

        private static List<string> GetStopWords()
        {
            using (StreamReader streamReader = new StreamReader("StopWords.txt"))
            {
                return streamReader.ReadToEnd().Split('\n').ToList();
            }
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
