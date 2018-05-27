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

            var rootPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/";
            var loadPath = rootPath + "queue/";
            var savePath = rootPath + "processed/";

            WordProcessor.ProcessFiles(rootPath, loadPath, savePath);

            Console.ReadKey();
        }
    }
}
