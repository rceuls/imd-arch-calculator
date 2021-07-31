namespace Calculator.Program
{
    public class CalculatorArguments
    {
        public CalculatorArguments(double[] inputs, CalculatorAction action)
        {
            // We don't want to create a calculator argument class containing invalid data. In the context of our application
            // less that two inputnumbers don't make sense so we throw an error when we try to create something like that.
            if (inputs.Length < 2)
            {
                throw new System.Exception(CalculatorError.NOT_ENOUGH_INPUT_PASSED.ToString());
            }
            Inputs = inputs;
            CalculatorAction = action;
        }

        // notice that these properties are read-only (missing set;). This means they can only be set via the
        // constructor arguments.
        public double[] Inputs { get; }
        public CalculatorAction CalculatorAction { get; }
    }
}
