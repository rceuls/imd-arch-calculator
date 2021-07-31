namespace Calculator.Program
{
    public class CalculatorResult
    {
        // This seems like a pretty silly class, but if you want to change from double to int (for one reason or another)
        // it's easier to adjust this as the concept of "CalculatorResultResult" lies solely in this class. 
        public CalculatorResult(double result)
        {
            Result = result;
        }

        // Also read only. This means that this class is immutable (https://en.wikipedia.org/wiki/Immutable_object)
        public double Result { get; }
    }
}
