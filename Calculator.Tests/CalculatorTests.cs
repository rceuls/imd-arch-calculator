using System;
using Calculator.Program;
using Xunit;

namespace Calculator.Tests
{
    public class CalculatorTests
    {
        [Fact]
        public void MalformedInput_ZeroInDivide()
        {
            // ARRANGE
            var inputForDivide = new CalculatorArguments(new[] { 0d, 1d, 2d }, CalculatorAction.Divide);
            // ACT
            (CalculatorError? errorOnDiv, CalculatorResult? outputDiv) = PocketCalculator.Calculate(inputForDivide);
            // ASSERT
            Assert.NotNull(errorOnDiv);
            Assert.Null(outputDiv);
            Assert.Equal(CalculatorError.DIVIDE_BY_ZERO, errorOnDiv);
        }

        [Theory]
        [InlineData(CalculatorAction.Add, new[] { 1d, 2d, 3d }, 6d)]
        [InlineData(CalculatorAction.Multiply, new[] { 4d, 5d, 6d }, 120d)]
        [InlineData(CalculatorAction.Divide, new[] { 10d, 5d, 2d }, 1d)]
        [InlineData(CalculatorAction.Subtract, new[] { 10d, 11d, 12d }, -13d)]
        public void HappyPath(CalculatorAction action, double[] inputAction, double expectedOutput)
        {
            // ARRANGE
            // see input parameter
            // ACT
            (CalculatorError? error, CalculatorResult? parsed) = PocketCalculator.Calculate(new CalculatorArguments(inputAction, action));
            // ASSERT
            Assert.Null(error);
            Assert.NotNull(parsed);
            Assert.Equal(expectedOutput, parsed?.Result);
        }
    }
}
