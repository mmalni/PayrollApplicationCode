
using Payroll.Service;
using Payroll.Common;
using Payroll.Model;

namespace Payroll.API
{
   public class PayRollAPI:IPayrollAPI
    {

        private IPayCalculatorService payCalculatorService;
       
        public PayRollAPI()
        {

            payCalculatorService = new PayCalculatorService(new TaxRateService());
        }
        public SalaryDTO generatePaySlip(EmployeeDTO employeeData, PayType payType)
        {
            SalaryDTO salaryDTO = new SalaryDTO();
            if (payType.Equals(PayType.MONTHLY)) {
                Employee employee = new Employee();
                Transformer.Transform(employeeData, ref employee);
                Salary salary = payCalculatorService.CalculateMonthlyPay(employee);
                Transformer.Transform(salary, ref salaryDTO);
            }
            return salaryDTO;
        }

       
    }
}
