using System;
using System.Linq;

namespace Calculator.Program
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputData = "/ 12 13 15";
            // if you want to make it interactive comment the previous line and uncomment the next three
            // Console.WriteLine("Input?");
            // var inputData = Console.ReadLine();
            // Console.WriteLine($"Your input was {inputData}");
            var splitted = inputData.Split(" ");
            var command = splitted[0];
            var inputAsStrings = splitted.Skip(1);
            var numbers = inputAsStrings.Select(double.Parse).ToArray();
            switch (command)
            {
                case "+":
                    Console.WriteLine($"You added {string.Join(", ", numbers)} and the result is {numbers.Sum()}");
                    break;
                case "-":
                    Console.WriteLine($"You subtracted {string.Join(", ", numbers)} and the result is {numbers.Aggregate((x, y) => x - y)}");
                    break;
                case "*":
                    Console.WriteLine($"You multiplied {string.Join(", ", numbers)} and the result is {numbers.Aggregate((x, y) => x * y)}");
                    break;
                case "/":
                    Console.WriteLine($"You divided {string.Join(", ", numbers)} and the result is {numbers.Aggregate((x, y) => x / y)}");
                    break;
                default:
                    Console.WriteLine("Oh no, invalid input");
                    break;
            }
        }
    }
}
