using CsvHelper.Configuration;
using Payroll.API;

namespace Payroll.client
{
    public sealed class SalaryDTOMap : CsvClassMap<SalaryDTO>
    {
        public override void CreateMap()
        {
            Map(m => m.EmployeeName).Index(0);
            Map(m => m.PayPeriod).Index(1);
            Map(m => m.GrossIncome).Index(2);
            Map(m => m.IncomeTax).Index(3);
            Map(m => m.NetIncome).Index(4);
            Map(m => m.Super).Index(5);
        }
    }
}