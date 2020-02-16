using System;
using System.Collections.Generic;
using System.Linq;

namespace RPNCalculator
{
    public static class Actions
    {
        private static readonly IVariables Variables = new VariablesTxt(@"{\w*}");
        
        private static readonly Dictionary<ulong, Command> Commands = new Dictionary<ulong, Command>()
        {
            {@"\help".Hash(), new Command(@"\help", "Справка", Help)},
            {@"\calculate".Hash(), new Command(@"\calculate", "Посчитать арифметическую операцию", Calculate)},
            {@"\addVariable".Hash(), new Command(@"\addVariable", 
                "Сохранить новую переменную", AddVariable)},
            {@"\readVariables".Hash(), new Command(@"\readVariables", 
                "Вывести все сохраненные переменные", ShowAllVariables)},
            {@"\readVariable".Hash(), new Command(@"\readVariable", 
                "Вывести сохраненную переменную по ключу", ShowVariable)},
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
            Application.Log("");
        }

        private static void Calculate()
        {
            Application.Log(Messages.Calculate);
            Application.Log(Messages.VariablesExample);
            string content = Console.ReadLine();
            Calculator.Calculate(Variables.Replace(content));
        }

        private static void AddVariable()
        {
            Application.Log(Messages.EnterKey);
            string key = Console.ReadLine();
            Application.Log(Messages.EnterValue);
            string value = Console.ReadLine();
            Application.Log(Variables.Write(key, value) ? Messages.VariableSaved : Messages.VariableNotSaved);
        }

        private static void ShowAllVariables()
        {
            var variables = Variables.ReadAll();
            foreach (var variable in variables)
                Application.Log(variable);
        }

        private static void ShowVariable()
        {
            Application.Log(Messages.EnterKey);
            Application.Log(Variables.Read(Console.ReadLine()));
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