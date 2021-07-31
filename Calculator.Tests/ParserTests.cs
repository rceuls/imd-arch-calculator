using System;
using Calculator.Program;
using Xunit;

namespace Calculator.Tests
{
    public class ParserTests
    {
        [Fact]
        public void MalformedInput_NoArguments()
        {
            foreach (var invalidInput in new[] { "", null, "                   " })
            {
                // ARRANGE
                // our invalid input
                // ACT
                (CalculatorError? error, CalculatorArguments? parsed) = TranslateInputToCalculatorArguments.Translate(invalidInput);
                // ASSERT
                Assert.NotNull(error);
                Assert.Null(parsed);
                Assert.Equal(CalculatorError.NOT_ENOUGH_INPUT_PASSED, error);
            }
        }

        [Fact]
        public void MalformedInput_WrongActionArgument()
        {
            // ARRANGE
            var invalidInput = "blabla 1 2 3";
            // ACT
            (CalculatorError? error, CalculatorArguments? parsed) = TranslateInputToCalculatorArguments.Translate(invalidInput);
            // ASSERT
            Assert.NotNull(error);
            Assert.Null(parsed);
            Assert.Equal(CalculatorError.INVALID_INPUT, error);
        }

        [Fact]
        public void MalformedInput_OnlyActionArgument()
        {
            // ARRANGE
            var invalidInput = "+";
            // ACT
            (CalculatorError? error, CalculatorArguments? parsed) = TranslateInputToCalculatorArguments.Translate(invalidInput);
            // ASSERT
            Assert.NotNull(error);
            Assert.Null(parsed);
            Assert.Equal(CalculatorError.NOT_ENOUGH_INPUT_PASSED, error);
        }

        [Fact]
        public void MalformedInput_OnlyActionAndOneNumberArgument()
        {
            // ARRANGE
            var invalidInput = "+ 1";
            // ACT
            (CalculatorError? error, CalculatorArguments? parsed) = TranslateInputToCalculatorArguments.Translate(invalidInput);
            // ASSERT
            Assert.NotNull(error);
            Assert.Null(parsed);
            Assert.Equal(CalculatorError.NOT_ENOUGH_INPUT_PASSED, error);
        }

        [Fact]
        public void MalformedInput_MalformedNumberArgument()
        {
            // ARRANGE
            var invalidInput = "+ 1 potato";
            // ACT
            (CalculatorError? error, CalculatorArguments? parsed) = TranslateInputToCalculatorArguments.Translate(invalidInput);
            // ASSERT
            Assert.NotNull(error);
            Assert.Null(parsed);
            Assert.Equal(CalculatorError.INVALID_INPUT, error);
        }

        [Theory]
        [InlineData("+ 1 2 3", CalculatorAction.Add, new[] { 1d, 2d, 3d })]
        [InlineData("* 4 5 6", CalculatorAction.Multiply, new[] { 4d, 5d, 6d })]
        [InlineData("/ 7 8 9", CalculatorAction.Divide, new[] { 7d, 8d, 9d })]
        [InlineData("- 10 11 12", CalculatorAction.Subtract, new[] { 10d, 11d, 12d })]
        public void HappyPath(string input, CalculatorAction expectedAction, double[] expectedInput)
        {
            // ARRANGE
            // see input parameter
            // ACT
            (CalculatorError? error, CalculatorArguments? parsed) = TranslateInputToCalculatorArguments.Translate(input);
            // ASSERT
            Assert.Null(error);
            Assert.NotNull(parsed);
            Assert.Equal(expectedAction, parsed?.CalculatorAction);
            Assert.Equal(expectedInput, parsed?.Inputs);
        }
    }
}
