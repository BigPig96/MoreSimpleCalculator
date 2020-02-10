using System.Collections.Generic;

namespace RPNCalculator
{
    public static class CharExtensions
    {
        private const string Operators = "+-*/()^";

        public static bool IsOperator(this char chr)
        {
            return Operators.IndexOf(chr) != -1;
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