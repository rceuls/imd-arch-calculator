using System;
using System.Linq;

namespace Calculator.Program
{
    public static class TranslateInputToCalculatorArguments
    {
        private static Tuple<CalculatorError?, CalculatorArguments?> CreateResponse(CalculatorError? error, CalculatorArguments? result) => Tuple.Create<CalculatorError?, CalculatorArguments?>(error, result);
        private static Tuple<CalculatorError?, CalculatorAction?> CreateErrorOrAction(CalculatorError? error, CalculatorAction? action) => Tuple.Create<CalculatorError?, CalculatorAction?>(error, action);
        private static Tuple<CalculatorError?, CalculatorAction?> GetCalculatorAction(string toParse)
        {
            switch (toParse)
            {
                case "+":
                    return CreateErrorOrAction(null, CalculatorAction.Add);
                case "-":
                    return CreateErrorOrAction(null, CalculatorAction.Subtract);
                case "*":
                    return CreateErrorOrAction(null, CalculatorAction.Multiply);
                case "/":
                    return CreateErrorOrAction(null, CalculatorAction.Divide);
                default:
                    return CreateErrorOrAction(CalculatorError.INVALID_INPUT, null);
            }
        }
        public static Tuple<CalculatorError?, CalculatorArguments?> Translate(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return CreateResponse(CalculatorError.NOT_ENOUGH_INPUT_PASSED, null);
            }
            var parsedInput = input.Split(" ");
            // we want at least three params => the actual command and two or more arguments
            if (parsedInput.Length <= 2)
            {
                // This is some pre-filtering. This input will not throw an unexpected error as explicitly handle this state.
                return CreateResponse(CalculatorError.NOT_ENOUGH_INPUT_PASSED, null);
            }
            (CalculatorError? error, CalculatorAction? parsedCalculatorAction) = GetCalculatorAction(parsedInput[0]);
            if (error.HasValue)
            {
                return CreateResponse(error.Value, null);
            }
            try
            {
                if (parsedCalculatorAction.HasValue)
                {
                    return CreateResponse(null,
                    new CalculatorArguments(
                        parsedInput.Skip(1).Select(double.Parse).ToArray(),
                        parsedCalculatorAction.Value
                    ));
                }
                else
                {
                    // this state is unreachable
                    return CreateResponse(CalculatorError.INVALID_INPUT, null);
                }
            }
            catch
            {
                // any parsing error is handled here.
                return CreateResponse(CalculatorError.INVALID_INPUT, null);
            }
        }

    }
}
