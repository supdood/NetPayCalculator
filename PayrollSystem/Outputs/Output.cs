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
            var comparisonOutput = (Output)obj;

            var grossPayIsEqual = GrossPay == comparisonOutput.GrossPay;
            var grossTaxableIsEqual = GrossTaxableEarnings == comparisonOutput.GrossPay;
            var netPayIsEqual = NetPay == comparisonOutput.NetPay;
            var witholdingsIsEqual = Withholdings.Equals(comparisonOutput.Withholdings);

            return grossPayIsEqual && grossTaxableIsEqual && netPayIsEqual;
        }
    }
}