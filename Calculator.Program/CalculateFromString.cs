using System;

namespace Calculator.Program
{
    // The responsibility of this class is providing us with methods to take a nullable string as input,
    // parse and validate the input and then use that data to calculate the expected result.
    public static class CalculateFromString
    {
        // you can also pass functions as arguments. Those functions are not executed unless they are called.
        public static Tuple<CalculatorError?, CalculatorResult?> DoCalculate(
            // A nullable string because we might be getting a null
            string? inputData,
            // We expect a Func(tion) with input string and output Tuple<...> as an argument.
            // https://docs.microsoft.com/en-us/dotnet/api/system.func-2?view=net-5.0
            Func<string?, Tuple<CalculatorError?, CalculatorArguments?>> TranslateStringToInput,
            // Notice that the input of this function is the rightmost output of the previous one.
            Func<CalculatorArguments, Tuple<CalculatorError?, CalculatorResult?>> DoCalculate
            )
        {
            (CalculatorError? argumentError, CalculatorArguments? calculatorArguments) = TranslateStringToInput(inputData);
            // We got a nullable reference type here (https://docs.microsoft.com/en-us/dotnet/csharp/nullable-references). 
            // This makes our checks easier as it enforces the (non-)nullability of both reference and value types.
            // (meaning you don't have do to "ourObject== null" everywhere). C# doesn't do this by default, you have to enable it in your csproj.
            if (argumentError.HasValue)
            {
                // We got an error, just return this one as it makes no sense to further execute the calculation.
                return Tuple.Create<CalculatorError?, CalculatorResult?>(argumentError, null);
            }
            // Notice that if we remove this condition check DoCalculate complains as it might be that the calculatorArgument is null
            else if (calculatorArguments != null)
            {
                // Do the calculation. Notice we don't care what method actually does the calculation; this makes it easier to
                // inject business logic here.
                return DoCalculate(calculatorArguments);
            }
            // This should not happen (you either have an error or result) but we still handle it as otherwise we don't have 
            // 100% coverage of potential exit conditions.
            return Tuple.Create<CalculatorError?, CalculatorResult?>(CalculatorError.UNKNOWN_ERROR, null);
        }
    }
}
