using System;
using System.IO;
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

            WordProcessor.ProcessFiles(folderPath, savePath);

            Console.ReadKey();
        }
    }
}
