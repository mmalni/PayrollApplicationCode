
namespace Payroll.API
{
    public interface IPayrollAPI
    {
        public SalaryDTO generatePaySlip(EmployeeDTO employeeData,PayType payType);
    }
}