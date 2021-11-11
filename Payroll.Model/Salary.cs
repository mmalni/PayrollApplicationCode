namespace Payroll.Model
{
    public class Salary
    {


        public string EmployeeName { get; set; }

        public string PayPeriod { get; set; }

        public int GrossIncome { get; set; }

        public int IncomeTax { get; set; }

        public int NetIncome { get; set; }

        public int Super { get; set; }
    }
}