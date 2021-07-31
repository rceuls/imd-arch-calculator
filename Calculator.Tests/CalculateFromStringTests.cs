using System;
using Calculator.Program;
using Xunit;

namespace Calculator.Tests
{
    public class CalculateFromStringTests
    {
        [Fact]
        public void HappyPath()
        {
            // ARRANGE
            var argWeWant = new CalculatorResult(12);
            var argWePass = new CalculatorArguments(new[] { 1d, 2d, 3d }, CalculatorAction.Add);
            // ACT
            var calculation = CalculateFromString.DoCalculate("abc",
                (string? input) => Tuple.Create<CalculatorError?, CalculatorArguments?>(null, argWePass),
                (CalculatorArguments input) => Tuple.Create<CalculatorError?, CalculatorResult?>(null, argWeWant)
                );
            // ASSERT
            Assert.Null(calculation.Item1);
            Assert.Equal(calculation.Item2, argWeWant);
        }

        [Fact]
        public void CalculateParseError()
        {
            // ARRANGE
            var argWePass = new CalculatorArguments(new[] { 1d, 2d, 3d }, CalculatorAction.Add);
            // ACT
            var calculation = CalculateFromString.DoCalculate("abc",
                (string? input) => Tuple.Create<CalculatorError?, CalculatorArguments?>(CalculatorError.UNKNOWN_ERROR, null),
                (CalculatorArguments input) => throw new Exception("Should not reach here")
                );
            // ASSERT
            Assert.NotNull(calculation.Item1);
            Assert.Equal(CalculatorError.UNKNOWN_ERROR, calculation.Item1);
        }

        [Fact]
        public void CalculateError()
        {
            // ARRANGE
            var argWePass = new CalculatorArguments(new[] { 1d, 2d, 3d }, CalculatorAction.Add);
            // ACT
            var calculation = CalculateFromString.DoCalculate("abc",
                (string? input) => Tuple.Create<CalculatorError?, CalculatorArguments?>(null, argWePass),
                (CalculatorArguments input) => Tuple.Create<CalculatorError?, CalculatorResult?>(CalculatorError.UNKNOWN_ERROR, null)
                );
            // ASSERT
            Assert.NotNull(calculation.Item1);
            Assert.Equal(CalculatorError.UNKNOWN_ERROR, calculation.Item1);
        }

        [Fact]
        public void ImpossibleStateOnParse()
        {
            // ARRANGE
            // ACT
            var calculation = CalculateFromString.DoCalculate("abc",
                (string? input) => Tuple.Create<CalculatorError?, CalculatorArguments?>(null, null),
                (CalculatorArguments input) => throw new Exception("Should not reach here")
                );
            // ASSERT
            Assert.NotNull(calculation.Item1);
            Assert.Equal(CalculatorError.UNKNOWN_ERROR, calculation.Item1);
        }
    }
}
