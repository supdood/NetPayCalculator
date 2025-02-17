using System.ComponentModel;

namespace PayrollSystem
{
    public class Input
    {
        public List<Earning> Earnings { get; set; }
        public List<Deduction> Deductions { get; set; }
        public List<Tax> Taxes { get; set; }
    }
}