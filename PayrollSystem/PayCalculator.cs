using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace PayrollSystem
{
    public class PayCalculator
    {
        public static Output CalculateNetPay(Input input)
        {
            var taxableAmount = 0M;
            var taxExemptAmount = 0M;

            //Calculate Gross Pay
            foreach(var earning in input.Earnings)
            {
                if(earning.IsTaxable)
                {
                    taxableAmount += CalculateGrossPay(earning);
                }
                else
                {
                    taxExemptAmount += CalculateGrossPay(earning);
                }
            }

            //Withholdings
            List<Withholding> allWithholdings = new List<Withholding>();

            //Pre-tax Deductions
            var preTaxDeductions = input.Deductions.Where(x => x.IsPreTax);
            PriorityQueue<Withholding, int> preTaxWithholdingQueue = CalculateDeductionWithholdings(taxableAmount, preTaxDeductions);
            
            var postPreTaxDeductionsAmount = ApplyWithholdings(taxableAmount, preTaxWithholdingQueue, allWithholdings);

            //Tax Withholdings
            PriorityQueue<Withholding, int> taxWithholdings = CalculateTaxWithholdings(postPreTaxDeductionsAmount, input.Taxes);

            var postTaxTaxableAmount = ApplyWithholdings(postPreTaxDeductionsAmount, taxWithholdings, allWithholdings);

            //Post-tax Deductions
            var postTaxDeductions = input.Deductions.Where(x => !x.IsPreTax);
            PriorityQueue<Withholding, int> postTaxWithholdings = CalculateDeductionWithholdings(postTaxTaxableAmount, postTaxDeductions);

            var netTaxableAmount = ApplyWithholdings(postTaxTaxableAmount, postTaxWithholdings, allWithholdings);
            var netPay = netTaxableAmount + taxExemptAmount;

            return new Output { GrossPay = taxableAmount + taxExemptAmount, GrossTaxableEarnings = taxableAmount, Withholdings = allWithholdings, NetPay = netPay };
        }

        private static decimal CalculateGrossPay(Earning earning)
        {
            if(earning.Type == EarningType.Hourly)
            {
                return earning.Rate * earning.Hours;
            }
            
            return earning.Amount;
        }

        private static PriorityQueue<Withholding, int> CalculateDeductionWithholdings(decimal taxableIncome, IEnumerable<Deduction> deductions)
        {

            var withholdingQueue = new PriorityQueue<Withholding, int>();

            foreach(Deduction deduction in deductions)
            {
                var withholding = new Withholding();

                if(deduction.Type == DeductionType.Flat)
                {
                    withholding.AmountWithheld = deduction.Value;
                }
                else
                {
                    withholding.AmountWithheld = CalculatePercentage(taxableIncome, deduction.Value);
                }

                withholding.Type = WithholdingType.Deduction;
                withholding.Code = Enum.Parse<WithholdingCode>(deduction.Code.ToString());
                
                withholdingQueue.Enqueue(withholding, deduction.Priority * -1);
            }

            return withholdingQueue;
        }

        private static PriorityQueue<Withholding, int> CalculateTaxWithholdings(decimal taxableIncome, IEnumerable<Tax> taxes)
        {

            var withholdingQueue = new PriorityQueue<Withholding, int>();

            foreach(Tax tax in taxes)
            {
                var withholding = new Withholding();

                if(tax.Type == TaxType.CappedPercentage)
                {
                    var calculatedTax = CalculatePercentage(taxableIncome, tax.Value);
                    withholding.AmountWithheld = calculatedTax > tax.Cap ? (decimal)tax.Cap : calculatedTax;
                }
                else
                {
                    withholding.AmountWithheld = CalculatePercentage(taxableIncome, tax.Value);
                }

                withholding.Type = WithholdingType.Tax;
                withholding.Code = Enum.Parse<WithholdingCode>(tax.Code.ToString());
                
                withholdingQueue.Enqueue(withholding, tax.Priority * -1);
            }

            return withholdingQueue;
        }

        private static decimal ApplyWithholdings(decimal taxableIncome, PriorityQueue<Withholding, int> withholdings, List<Withholding> allWithHoldings)
        {
            var resultingIncome = taxableIncome;

            while(withholdings.Count > 0)
            {
                var withholding = withholdings.Dequeue();
                resultingIncome -= withholding.AmountWithheld;

                if(resultingIncome < 0)
                {
                    withholding.Deficit = resultingIncome * -1;
                    withholding.AmountWithheld -= withholding.Deficit;
                    resultingIncome = 0;
                }

                allWithHoldings.Add(withholding);
            }

            return resultingIncome;
        }

        private static decimal CalculatePercentage(decimal amount, decimal percentage)
        {
            return amount * (percentage/100);
        }
    }
}