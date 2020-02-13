using System;
using System.Collections.Generic;
using System.Linq;

namespace RPNCalculator
{
    public static class Actions
    {
        private static readonly Dictionary<ulong, Command> Commands = new Dictionary<ulong, Command>()
        {
            {@"\help".Hash(), new Command(@"\help", "Справка", Help)},
            {@"\calculate".Hash(), new Command(@"\calculate", "Посчитать арифметическую операцию", Calculate)},
            {@"\clear".Hash(), new Command(@"\clear", "Очистить лог", ClearLog)},
            {@"\exit".Hash(), new Command(@"\exit", "Завершить работу приложения", Application.StopRun)}
        };
        
        private static void Help()
        {
            Application.Log("\nСправка");
            //todo Check memory allocation by delegate vs local function in iterator
            bool Condition(KeyValuePair<ulong, Command> command) => command.Key != @"\help".Hash(); 
            foreach (var command in Commands.Where(Condition))
                Console.WriteLine($"{command.Value.Name} - {command.Value.Description}");
        }

        private static void Calculate()
        {
            Application.Log(Messages.Calculate);
            Calculator.Calculate(Console.ReadLine());
        }

        private static void ClearLog()
        {
            Console.Clear();
            Application.Log(Messages.Welcome);
        }

        public static void ExecuteCommands()
        {
            string input = Console.ReadLine();
            if(string.IsNullOrEmpty(input)) return;
                
            if (!Commands.TryGetValue(input.Hash(), out var command))
            {
                Application.Log(Messages.UnknownCommand);
                return;
            }

            command.Invoke();
        }
    }
}