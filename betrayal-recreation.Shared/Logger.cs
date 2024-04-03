using System;

namespace betrayal_recreation_shared
{
    public static class Logger
    {
        private enum LogType { INFO, ERROR, WARNING }
        private static ConsoleColor _consoleColor = ConsoleColor.Cyan;
        public static void Initialize(ConsoleColor consoleColor)
        {
            _consoleColor = consoleColor;
        }

        private static void Log(string message, LogType logType)
        {

            string prefix = "[INFO]:";
            if (logType == LogType.ERROR)
            {
                Console.ForegroundColor = ConsoleColor.Red ;
                prefix = "[ERROR]:";
            } 
            else if (logType == LogType.WARNING)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                prefix = "[WARNING]:";
            }
            else
            {
                Console.ForegroundColor = _consoleColor;
            }

            Console.WriteLine(prefix + " " + message);
            Console.ResetColor();
        }

        public static void LogInfo (string info)
        {
            Log(info, LogType.INFO);
        }
    }
}
