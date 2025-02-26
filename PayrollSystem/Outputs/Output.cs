namespace PayrollSystem
{
    public class Output
    {
        public decimal GrossPay { get; set; }
        public decimal GrossTaxableEarnings { get; set; }
        public List<Withholding> Withholdings { get; set; }
        public decimal NetPay { get; set; }

        public override bool Equals(object? obj)
        {
            Output comparisonOutput = obj as Output;

            if (comparisonOutput == null)
            {
                return false;
            }

            var grossPayIsEqual = GrossPay == comparisonOutput.GrossPay;
            var grossTaxableIsEqual = GrossTaxableEarnings == comparisonOutput.GrossTaxableEarnings;
            var netPayIsEqual = NetPay == comparisonOutput.NetPay;
            var witholdingsIsEqual = Withholdings.SequenceEqual(comparisonOutput.Withholdings);

            return grossPayIsEqual && grossTaxableIsEqual && netPayIsEqual && witholdingsIsEqual;
        }
    }
}