using System.Text.Json.Serialization;

namespace PayrollSystem
{
    public class Earning
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EarningCode Code { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EarningType Type { get; set; }
        public int Hours { get; set; }
        public decimal Rate { get; set; }
        public bool IsTaxable { get; set; }
        public decimal Amount { get; set; }
    }

    public enum EarningCode
    {
        Regular,
        Overtime,
        Bonus
    }

    public enum EarningType
    {
        Hourly,
        Salary
    }      
}