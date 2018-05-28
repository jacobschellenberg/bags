using System.IO;

namespace Bags.Utilities
{
    public static class IOUtilities
    {
        public static void Write(string value, string filePath)
        {
            using(var streamWriter = new StreamWriter(filePath))
            {
                streamWriter.Write(value);
            }
        }

        public static string Read(string filePath)
        {
            using(var streamReader = new StreamReader(filePath))
            {
                return streamReader.ReadToEnd();
            }
        }

        public static string GetExecutionPath()
        {
            return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }
    }
}
