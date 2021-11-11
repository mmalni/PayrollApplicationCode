using Payroll.Model;
using Payroll.Common;

namespace Payroll.Service
{
   
    public class PayCalculatorService : IPayCalculatorService
    {


        private ITaxRateService taxRateService;
       

        /// <summary>
        /// Constructor
        /// Constructor
        /// </summary>
        /// <param name="taxRateService"></param>
        public PayCalculatorService(ITaxRateService taxRateService)
        {
           this.taxRateService = taxRateService;
        }

        /// <summary>
        /// Calculate the monthly pay
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public Salary CalculateMonthlyPay(Employee employee)  
        {
            Salary salary = new Salary();
            try { 
                this.CheckSuperRate(employee);
                this.CheckAnnualSalary(employee);
                 this.getName(employee, salary);
                this.getPayPeriod(employee, salary);
                this.CalcGrossIncome(employee, salary);
                this.CalcIncomeTax(employee, salary);
                this.CalcNetIncome(salary);
                this.CalcSuper(employee,salary);
             }catch(Exception exp)
            {
                throw new PayCalculatorServiceException(exp.Message);
            }
            return salary;
        }

        /// <summary>
        /// Validate the Annual Salary
        /// </summary>
        /// <param name="employee"></param>
        /// <exception cref="OverflowException"></exception>
        private void CheckAnnualSalary(Employee employee)
        {
            if (employee.AnnualSalary < 0)
            {
                throw new PayCalculatorServiceException("AnnualSalary not Valid");
            }
        }

        /// <summary>
        /// Validate the Super Rate
        /// </summary>
        /// <param name="employee"></param>
        /// <exception cref="OverflowException"></exception>
        private void CheckSuperRate(Employee employee)
        {
            if (employee.SuperRate > 0.5M || employee.SuperRate < 0M)
            {
                throw new PayCalculatorServiceException("SuperRate not Valid");
            }
        }

        /// <summary>
        /// getName of the Employee
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="salary"></param>
        private void getName(Employee employee, Salary salary)
        {
            salary.EmployeeName = string.Format("{0} {1}", employee.FirstName, employee.LastName);
        }

        /// <summary>
        /// getPayPeriod of the Employee
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="salary"></param>
        private void getPayPeriod(Employee employee, Salary salary)
        {
           salary.PayPeriod = employee.PaymentStartDate;
        }

        /// <summary>
        /// Calculate the Gross income
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="salary"></param>

        private void CalcGrossIncome(Employee employee, Salary salary)
        {
            salary.GrossIncome = (int)Math.Round((double)(employee.AnnualSalary / 12),MidpointRounding.AwayFromZero);
          
        }

        /// <summary>
        /// Calculate the Income Tax
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="payslip"></param>

        private void CalcIncomeTax(Employee employee, Salary payslip)
        {
            payslip.IncomeTax = taxRateService.CalculateTaxRate(employee.AnnualSalary);
        }

       /// <summary>
       /// Calculate net income
       /// </summary>
       /// <param name="payslip"></param>
        private void CalcNetIncome(Salary payslip)
        {
            payslip.NetIncome = payslip.GrossIncome - payslip.IncomeTax;
        }

        /// <summary>
        /// Calculate Super Income
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="payslip"></param>
        private void CalcSuper(Employee employee, Salary payslip)
        {
           payslip.Super = (int)Math.Round((double)(payslip.GrossIncome * employee.SuperRate), MidpointRounding.AwayFromZero);
        }
    }
}