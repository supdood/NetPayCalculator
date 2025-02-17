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