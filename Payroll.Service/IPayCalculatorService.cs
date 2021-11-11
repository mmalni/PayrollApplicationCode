using Payroll.Model;

namespace Payroll.Service
{
    public interface IPayCalculatorService
    {

        /// <summary>
        /// Calculate the monthly Payment
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        Salary CalculateMonthlyPay(Employee employee);
    }
}
