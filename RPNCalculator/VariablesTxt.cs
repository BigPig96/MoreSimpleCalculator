using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace RPNCalculator
{
    public class VariablesTxt : IVariables
    {
        private const string Error404 = "Переменной не существует";

        private readonly string _path = Directory.GetCurrentDirectory() + "/variables.txt";
        private readonly Regex _regex;

        public VariablesTxt(string findPattern)
        {
            _regex = new Regex(findPattern);
        }
        
        public bool Write(string key, string value)
        {
            if(Read(key) != Error404) return false;
            
            string line = $"{key}={value}\n";
            File.AppendAllText(_path, line);
            return true;
        }

        public IEnumerable<string> ReadAll()
        {
            return File.Exists(_path) ? File.ReadLines(_path) : new []{Messages.EmptyVariables};
        }

        public string Replace(string input)
        {
            return _regex.Replace(input, match => 
                Read(match.Value.Substring(1, match.Length - 2)));
        }
        
        public string Read(string key)
        {
            if (!File.Exists(_path)) return Error404;
            
            var lines = File.ReadLines(_path);
            
            foreach (var line in lines)
            {
                string[] variable = line.Split('=');
                if (variable[0] == key) return variable[1];
            }

            return Error404;
        }
    }
}