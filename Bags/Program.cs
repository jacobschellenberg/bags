using System;
using System.IO;
using System.Linq;
using BagsEngine;

namespace Bags
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bags!");

            var rootPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var folderPath = rootPath + Path.PathSeparator + "queue" + Path.PathSeparator;
            var savePath = rootPath + Path.PathSeparator + "processed" + Path.PathSeparator;

            var files = Directory.EnumerateFiles(folderPath).Where(file => Path.GetExtension(file).Equals(".txt", StringComparison.InvariantCultureIgnoreCase)).ToList();

            WordProcessor.ProcessFiles(folderPath, savePath, files);

            Console.ReadKey();
        }
    }
}
