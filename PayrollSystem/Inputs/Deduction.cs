using System.Text.Json.Serialization;

namespace PayrollSystem
{
    public class Deduction
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DeductionCode Code { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DeductionType Type { get; set; }
        public int Priority { get; set; }
        public decimal Value { get; set; }
        public bool IsPreTax { get; set; }
    }

    public enum DeductionCode
    {
        HealthInsurance,
        Retirement401k,
        Uniform
    }

    public enum DeductionType
    {
        Percentage,
        Flat
    }
}