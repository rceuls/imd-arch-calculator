using System;

namespace Calculator.Program
{

    // Notice that this file is the only part of the program that contains I/O.
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input?");
            var inputData = Console.ReadLine();
            var output = CalculateFromString.DoCalculate(
                inputData,
                TranslateInputToCalculatorArguments.Translate,
                PocketCalculator.Calculate
            );
            PrintOutput(output);

        }

        private static void PrintOutput(Tuple<CalculatorError?, CalculatorResult?> output)
        {
            // Don't do this. Item1 and Item2 can be accessed this way but it seems pretty unreadable. 
            // Use destructuring (see the other files) instead.
            if (output.Item1 != null)
            {
                Console.Error.WriteLine($"Got an error: {output.Item1.Value}");
            }
            else if (output.Item2 != null)
            {
                Console.WriteLine($"Calculated {output.Item2?.Result}");
            }
        }
    }
}
