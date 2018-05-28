using System;
namespace Bags.Utilities
{
    public class Logger
    {
        private static readonly bool _showLogs = true;
        private static readonly bool _showVerboseLogs = false;

        public static void WriteLine(string message = "", bool isVerbose = true)
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
