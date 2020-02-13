using System;

namespace RPNCalculator
{
    public static class Application
    {
        public static bool IsRunning => _isRunning;
        
        private static bool _isRunning;
        
        public static void StartRun()
        {
            Log(Messages.Welcome);
            _isRunning = true;
            while (_isRunning)
                Actions.ExecuteCommands();
        }

        public static void StopRun()
        {
            _isRunning = false;
        }

        public static void Log(string message, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}