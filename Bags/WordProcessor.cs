using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bags;
using Bags.Extensions;
using Bags.Utilities;
using Newtonsoft.Json;

namespace BagsEngine
{
    public class WordProcessor
    {
        private static Dictionary<string, int> _totalWordCount = new Dictionary<string, int>();
        private static IEnumerable<string> _stopWords = new List<string>();
        private static Config _config = new Config();

        public static void CountWordsInFiles(string rootPath, string loadPath, string savePath)
        {
            try
            {
                _config = JsonConvert.DeserializeObject<Config>(IOUtilities.Read(rootPath + "config.json"));
            }
            catch(Exception e)
            {
                Logger.WriteLine("Custom config.json not found, using default settings. " + e.Message, false);
            }

            try
            {
                _stopWords = IOUtilities.Read(rootPath + "StopWords.txt").Split('\n');
            }
            catch(Exception e)
            {
                Logger.WriteLine("Custom StopWords.txt not found, counting all the things. " + e.Message, false);
            }

            var filePaths = new List<string>();
            try
            {
                filePaths = Directory.EnumerateFiles(loadPath).Where(file => Path.GetExtension(file).Equals(".txt", StringComparison.InvariantCultureIgnoreCase)).ToList();
            }
            catch(Exception e)
            {
                Logger.WriteLine("No files found in /processed/. Aborting mission. " + e.Message, false);
            }

            filePaths.ForEach(file => {
                CountWordsInFile(file, savePath);
            });

            WriteResults(_totalWordCount, _config.NumberOfWords, "total", savePath);
        }

        private static void CountWordsInFile(string filePath, string savePath)
        {
            Logger.WriteLine("Processing File: " + filePath + " ...", false);

            var wordCount = new Dictionary<string, int>();
            string[] words = IOUtilities.Read(filePath).SplitByWord();

            words.ToList().ForEach(word => {
                if (!_stopWords.Contains(word))
                {
                    wordCount.SetOrIncrement(word);
                    _totalWordCount.SetOrIncrement(word);
                }
            });

            WriteResults(wordCount, _config.NumberOfWords, Path.GetFileNameWithoutExtension(filePath), savePath);

            Logger.WriteLine(filePath + " Processing Completed.");
        }

        private static void WriteResults<T1, T2>(Dictionary<T1, T2> dictionary, int maxWords, string fileName, string savePath)
        {
            var sortedTotalWords = dictionary.SortByValue().Take(maxWords);
            IOUtilities.Write(JsonConvert.SerializeObject(sortedTotalWords, Formatting.Indented), savePath + fileName + ".json");
        }
    }
}
