using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bags.Extensions;
using Bags.Utilities;
using Newtonsoft.Json;

namespace BagsEngine
{
    public class WordProcessor
    {
        private static readonly int _numberOfWords = 50;
        private static Dictionary<string, int> _totalWordCount = new Dictionary<string, int>();
        private static IEnumerable<string> _stopWords = new List<string>();

        public static void CountWordsInFiles(string rootPath, string loadPath, string savePath)
        {
            _stopWords = IOUtilities.Read(rootPath + "StopWords.txt").Split('\n');

            var filePaths = Directory.EnumerateFiles(loadPath).Where(file => Path.GetExtension(file).Equals(".txt", StringComparison.InvariantCultureIgnoreCase)).ToList();

            filePaths.ForEach(file => {
                CountWordsInFile(file, savePath);
            });

            WriteResults(_totalWordCount, _numberOfWords, "total", savePath);
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

            WriteResults(wordCount, _numberOfWords, Path.GetFileNameWithoutExtension(filePath), savePath);

            Logger.WriteLine(filePath + " Processing Completed.");
        }

        private static void WriteResults<T1, T2>(Dictionary<T1, T2> dictionary, int maxWords, string fileName, string savePath)
        {
            var sortedTotalWords = dictionary.SortByValue().Take(maxWords);
            IOUtilities.Write(JsonConvert.SerializeObject(sortedTotalWords, Formatting.Indented), savePath + fileName + ".json");
        }
    }
}
