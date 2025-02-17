using System.Text.Json.Serialization;

namespace PayrollSystem
{
    public class Tax
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TaxCode Code { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TaxType Type { get; set; }
        public int Priority { get; set; }
        public decimal Value { get; set; }
        public decimal? Cap { get; set; }
    }

    public enum TaxCode
    {
        FederalIncome,
        FICA
    }

    public enum TaxType
    {
        Percentage,
        CappedPercentage
    }
}