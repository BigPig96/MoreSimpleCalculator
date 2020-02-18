using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;

namespace RPNCalculator
{
    public class StringFunctions
    {
        private const string Pattern = @"\w+\([\d*\.*\d*+\-\*\/ ,]{0,}\)";
        private readonly Regex _regex;

        public StringFunctions()
        {
            _regex = new Regex(Pattern);
        }

        public string Replace(string input)
        {
            while (_regex.Match(input).Length > 0)
            {
                input = _regex.Replace(input, match => Convert(match.Value));
            }

            return input;
        }
        
        private string Convert(string name)
        {
            string[] values = name.Split(new [] {'(', ',', ')'}, StringSplitOptions.RemoveEmptyEntries);
            MethodInfo method = GetType().GetMethod(values[0]);
            if(method == null) return name;

            var args = new object[values.Length - 1];
            if (method.GetParameters().Length != args.Length) return name;

            for (int i = 0; i < args.Length; i++)
                args[i] = values[i + 1].HaveAnyOperators() ? Calculator.Calculate(values[i + 1]) : values[i + 1];


            return method.Invoke(this, args).ToString();
        }
        
        
        
        public static string POW(string a, string b)
        {
            if (!float.TryParse(a, NumberStyles.Float, 
                CultureInfo.InvariantCulture.NumberFormat, out float first)) 
                return $"{a} is not a float";
            if (!float.TryParse(b, NumberStyles.Float, 
                CultureInfo.InvariantCulture.NumberFormat, out float second)) 
                return $"{b} is mot a float";
            return Math.Pow(Math.Abs(first), second).ToString(CultureInfo.InvariantCulture);
        }
    }
}