using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPNCalculator
{
    class Program
    {
        
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Введите операцию и нажмите Enter");
                string rawInput = Console.ReadLine();
                if (rawInput == "exit") break;
                Console.WriteLine(Calculator.Calculate(rawInput));
            }
        }
    }
}
