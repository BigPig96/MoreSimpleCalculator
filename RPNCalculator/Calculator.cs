using System;
using System.Collections.Generic;
using System.Text;

namespace RPNCalculator
{
    public static class Calculator
    {
        public static string Calculate(string input)
        {
            input = ReversePolishNotation.Convert(input);

            var stack = new Stack<float>();
            
            for (int i = 0; i < input.Length; i++)
            {
                if(input[i] == ' ') continue;

                if (char.IsDigit(input[i]))
                {
                    var digitBuilder = new StringBuilder();
                    
                    while (input[i] != ' ')
                    {
                        digitBuilder.Append(input[i]);
                        i++;
                        if(i == input.Length) break;
                    }

                    if (!float.TryParse(digitBuilder.ToString(), out float result))
                    {
                        Console.WriteLine($"Некорректный операнд {digitBuilder}");
                        return "0";
                    }
                        
                    stack.Push(result);
                    i--;
                }

                else if (input[i].IsOperator())
                {
                    if (stack.Count < 2)
                    {
                        Console.WriteLine("Пропущено число перед оператором");
                        return "0";
                    }
                    
                    float a = stack.Pop();
                    float b = stack.Pop();
                    stack.Push(Operation(a, b, input[i]));
                }
            }

            return stack.Pop().ToString();
        }

        private static float Operation(float a, float b, char oper)
        {
            Console.WriteLine($"{b} {oper} {a}");
            
            switch (oper)
            {
                case '+' : return b + a;
                case '-' : return b - a;
                case '*' : return b * a;
                case '/' : return b / a;
            }
            
            return 0;
        }
    }
}