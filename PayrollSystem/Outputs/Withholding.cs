using System.Text.Json.Serialization;

namespace PayrollSystem
{
    public class Withholding
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public WithholdingType Type { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public WithholdingCode Code { get; set; }
        public decimal AmountWithheld { get; set; }
        public decimal Deficit { get; set; }

        public override bool Equals(object? obj)
        {
            Withholding comparisonWitholding = obj as Withholding;

            if (comparisonWitholding == null) {
                return false;
            }
            
            var typeIsEqual = this.Type == comparisonWitholding.Type;
            var codeIsEqual = this.Code == comparisonWitholding.Code;
            var amountWithheldIsEqual = this.AmountWithheld == comparisonWitholding.AmountWithheld;
            var deficitIsEqual = this.Deficit == comparisonWitholding.Deficit;

            return typeIsEqual && codeIsEqual && amountWithheldIsEqual && deficitIsEqual;
        }
    }

    public enum WithholdingType
    {
        Deduction,
        Tax
    }

    public enum WithholdingCode
    {
        HealthInsurance,
        FICA,
        FederalIncome,
        Retirement401k,
        Uniform
    }
}