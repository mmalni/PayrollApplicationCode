using Payroll.Common;
using Payroll.Model;

namespace Payroll.Service
{
   public class TaxRateService : ITaxRateService
    {
        private static readonly IList<Tax> TaxRules = new List<Tax>()
            {
                new Tax 
                {
                    AnnualSalaryFrom = 0,
                    AnnualSalaryTo = 18200,
                    TaxRate = 0M,
                    BaseIncome = 0,
                    BaseTax = 0
                },
                new Tax 
                {
                    AnnualSalaryFrom = 18201,
                    AnnualSalaryTo = 37000,
                    TaxRate = 0.19M,
                    BaseIncome = 18200,
                    BaseTax = 0
                },
                new Tax 
                {
                    AnnualSalaryFrom = 37001,
                    AnnualSalaryTo = 80000,
                    TaxRate = 0.325M,
                    BaseIncome = 37000,
                    BaseTax = 3572
                },
                new Tax 
                {
                    AnnualSalaryFrom = 80001,
                    AnnualSalaryTo = 180000,
                    TaxRate = 0.37M,
                    BaseIncome = 80000,
                    BaseTax = 17547
                },
                new Tax 
                {
                    AnnualSalaryFrom = 180001,
                    AnnualSalaryTo = int.MaxValue,
                    TaxRate = 0.45M,
                    BaseIncome = 180000,
                    BaseTax = 54547
                },
            };

        public TaxRateService()
        {

        }
       /// <summary>
       /// Calculate the Tax Rate based on the annual salary
       /// </summary>
       /// <param name="annualSalary"></param>
       /// <returns></returns>
        public int CalculateTaxRate(int annualSalary)
        {
            var res=0;
            try
            {
                Tax taxRule = TaxRules.Single(x => x.AnnualSalaryFrom <= annualSalary
                    && annualSalary <= x.AnnualSalaryTo);

                var tax = (taxRule.BaseTax + ((annualSalary - taxRule.BaseIncome) * taxRule.TaxRate)) / 12;

               res = (int)Math.Round(tax, System.MidpointRounding.AwayFromZero);
            }catch(Exception ex)
            {
                throw new TaxRateServiceException(ex.Message);
            }
            return res;

        }
    }
}