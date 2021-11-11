namespace Payroll.Model
{

    public class Tax
    {
        public int BaseIncome { get; set; }

        public int BaseTax { get; set; }

        public decimal TaxRate { get; set; }

        public int AnnualSalaryFrom { get; set; }

        public int AnnualSalaryTo { get; set; }
    }
}