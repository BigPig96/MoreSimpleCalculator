using System;
using System.Collections.Generic;
using System.Text;

namespace RPNCalculator
{
    public static class ReversePolishNotation
    {
        public static string Convert(string input)
        {
            AutoCorrect(ref input);
            
            var output = new StringBuilder();
            var stack = new Stack<char>();
            
            int openBrackets = 0;
            int closeBrackets = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if(input[i] == ' ') continue;
                

                if (char.IsDigit(input[i]))
                {
                    while (input[i] != ' ' && !input[i].IsOperator())
                    {
                        output.Append(input[i]);
                        i++;
                        if(i == input.Length) break;
                    }

                    output.Append(' ');
                    i--;
                }

                else if (input[i].IsOperator())
                {

                    if (input[i] == '(')
                    {
                        openBrackets++;
                        stack.Push(input[i]);
                    }

                    else if (input[i] == ')')
                    {
                        closeBrackets++;
                        char chr = stack.Pop();
                    
                        while (chr != '(')
                        {
                            output.Append(chr + " ");
                            chr = stack.Pop();
                        }
                    }

                    else
                    {
                        if (stack.Count > 0 && input[i].GetPriority() <= stack.Peek().GetPriority())
                            output.Append(stack.Pop() + " ");
                        
                        stack.Push(input[i]);
                    }
                }
                else
                {
                    Application.Log($"Некорретный символ {input[i]}");
                    return "0";
                }
            }

            if (stack.Count == 0)
            {
                Application.Log("Операции не найдены");
                return "0";
            }
            
            if (openBrackets != closeBrackets)
            {
                Application.Log("Количество скобок не совпадает!");
                return "0";
            }

            while (stack.Count > 0)
                output.Append(stack.Pop() + "");
            
            return output.ToString();
        }

        private static void AutoCorrect(ref string input)
        {
            var builder = new StringBuilder(input);
            
            builder.Replace('.', ',');
            builder.Replace(" ", "");

            for (int i = builder.Length - 1; i > - 1; i--)
            {
                if (i > 0 && builder[i] == '(' && !builder[i - 1].IsOperator())
                    builder.Insert(i, "*");
            }

            input = builder.ToString();
        }
    }
}
