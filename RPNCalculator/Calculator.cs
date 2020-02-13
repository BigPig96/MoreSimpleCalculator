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
            var digitBuilder = new StringBuilder();
            
            for (int i = 0; i < input.Length; i++)
            {
                digitBuilder.Clear();
                if(input[i] == ' ') continue;

                if (char.IsDigit(input[i]))
                {

                    while (input[i] != ' ')
                    {
                        digitBuilder.Append(input[i]);
                        i++;
                        if(i == input.Length) break;
                    }

                    if (!float.TryParse(digitBuilder.ToString(), out float result))
                    {
                        Application.Log($"Некорректный операнд {digitBuilder}");
                        return "0";
                    }
                        
                    stack.Push(result);
                    i--;
                }

                else if (input[i].IsOperator())
                {
                    if (stack.Count < 2)
                    {
                        Application.Log("Пропущено число перед оператором");
                        return "0";
                    }
                    
                    float a = stack.Pop();
                    float b = stack.Pop();
                    stack.Push((Operation(a, b, input[i])));
                    Application.Log($"= {stack.Peek()}");
                }
            }

            return stack.Pop().ToString();
        }

        private static float Operation(float a, float b, char oper)
        {
            Application.Log($"{b} {oper} {a}");
            
            switch (oper)
            {
                case '+' : return b + a;
                case '-' : return b - a;
                case '*' : return b * a;
                case '/' : return b / a;
                case '^' : 
                    return (float) Math.Pow(Math.Abs(b), a);
            }
            
            return 0;
        }
    }
}