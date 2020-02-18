using System;
using System.Linq;
using Enumerable = System.Linq.Enumerable;

namespace RPNCalculator
{
    public static class Extensions
    {
        private const string Operators = "+-*/()^";

        public static bool IsOperator(this char chr)
        {
            return Operators.IndexOf(chr) != -1;
        }

        public static bool HaveAnyOperator(this string str)
        {
            return str.Count(chr => Operators.IndexOf(chr) != -1) > 0;
        }
        
        public static ulong Hash(this string read)
        {
            ulong hashedValue = 3074457345618258791ul;
            for(int i = 0; i < read.Length; i++)
            {
                hashedValue += read[i];
                hashedValue *= 3074457345618258799ul;
            }
            return hashedValue;
        }

        public static int GetPriority(this char chr)
        {
            switch (chr)
            {
                case '(': return 0;
                case ')': return 0;
                case '+': return 1;
                case '-': return 1;
                case '*': return 2;
                case '/': return 2;
                case '^': return 3;
                default: return 3;
            }
        }
    }
}