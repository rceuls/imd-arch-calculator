using System;
using System.Linq;

namespace Calculator.Program
{
    // This class is responsible for being a calculator and handling numeric inputs 
    // and returning numeric outputs (or an errorstate)
    public static class PocketCalculator
    {
        // crappy boilerplate.
        private static Tuple<CalculatorError?, CalculatorResult?> CreateResponse(CalculatorError? error, CalculatorResult? result)
        => Tuple.Create(error, result);

        // Notice that this is the only public method; we dispatch it to more specific submethods. We could inline 
        // everything here but that would lead to some messy code. It's also easier to read this way, and we keep the
        // SRP in mind => https://en.wikipedia.org/wiki/Single-responsibility_principle
        public static Tuple<CalculatorError?, CalculatorResult?> Calculate(CalculatorArguments arguments)
        {
            switch (arguments.CalculatorAction)
            {
                case CalculatorAction.Add:
                    return Add(arguments.Inputs);
                case CalculatorAction.Divide:
                    return Divide(arguments.Inputs);
                case CalculatorAction.Multiply:
                    return Multiply(arguments.Inputs);
                case CalculatorAction.Subtract:
                    return Subtract(arguments.Inputs);
                default:
                    // We have this default as when we add a new method it fails and shows up in our unit-tests. 
                    return CreateResponse(CalculatorError.UNKNOWN_ERROR, null);
            }
        }

        private static Tuple<CalculatorError?, CalculatorResult?> Add(double[] inputs)
        {
            return CreateResponse(null, new CalculatorResult(inputs.Aggregate((x, y) => x + y)));
        }

        private static Tuple<CalculatorError?, CalculatorResult?> Subtract(double[] inputs)
        {
            return CreateResponse(null, new CalculatorResult(inputs.Aggregate((x, y) => x - y)));
        }

        private static Tuple<CalculatorError?, CalculatorResult?> Multiply(double[] inputs)
        {
            if (inputs.Any(x => x == 0))
            {
                // saves us an aggregate
                return CreateResponse(null, new CalculatorResult(0d));
            }
            return CreateResponse(null, new CalculatorResult(inputs.Aggregate((x, y) => x * y)));
        }

        private static Tuple<CalculatorError?, CalculatorResult?> Divide(double[] inputs)
        {
            if (inputs.Any(x => x == 0))
            {
                // saves us an aggregate
                return CreateResponse(CalculatorError.DIVIDE_BY_ZERO, null);
            }
            return CreateResponse(null, new CalculatorResult(inputs.Aggregate((x, y) => x / y)));
        }
    }
}
