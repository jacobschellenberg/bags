using System;
using Bags.Utilities;
using BagsEngine;

namespace Bags
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Bags!");

            var rootPath = IOUtilities.GetExecutionPath() + "/";
            var loadPath = rootPath + "queue/";
            var savePath = rootPath + "processed/";

            WordProcessor.CountWordsInFiles(rootPath, loadPath, savePath);

            Console.ReadKey();
        }
    }
}
