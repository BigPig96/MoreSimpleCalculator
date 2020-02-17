using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace RPNCalculator
{
    public class StringFunctions
    {
        private const string Pattern = @"\w*\(.*\)";
        private readonly Regex _regex;

        public StringFunctions()
        {
            _regex = new Regex(Pattern);
        }

        public string Replace(string input)
        {
            return _regex.Replace(input, match => Convert(match.Value));
        }
        
        private string Convert(string name)
        {
            string[] values = name.Split(new [] {'(', ',', ')'}, StringSplitOptions.RemoveEmptyEntries);
            MethodInfo method = GetType().GetMethod(values[0]);
            if(method == null) return name;

            var args = new object[values.Length - 1];
            if (method.GetParameters().Length != args.Length) return name;
            
            for (int i = 0; i < args.Length; i++)
                args[i] = values[i + 1];

            return method.Invoke(this, args).ToString();
        }
        
        public static string POW(string a, string b)
        {
            return $"{a}^{b}";
        }
    }
}