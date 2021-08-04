using System;
using Calculator.Program;
using Xunit;

namespace Calculator.Tests
{
    public class CalculatorArgumentTests
    {
        [Fact]
        public void MalformedInput_NoInputs()
        {
            foreach (var data in new[] { Array.Empty<double>(), new[] { 1d } })
            {
                foreach (var op in Enum.GetValues<CalculatorAction>())
                {
                    // ARRANGE and ACT and ASSERT
                    // we don't want to immediately execute this action in the test itself, so we let our 
                    // assert handle it. (https://ardalis.com/testing-exceptions-with-xunit-and-actions/)
                    Assert.Throws<Exception>(() => new CalculatorArguments(data, op));
                }
            }
        }
    }
}
