using CsvHelper.Configuration;
using Payroll.API;
namespace Payroll.client
{
   

    public sealed class EmployeeDTOMap : CsvClassMap<EmployeeDTO>
    {
      public override void CreateMap()
        {
            Map(m => m.FirstName).Index(0);
            Map(m => m.LastName).Index(1);
            Map(m => m.AnnualSalary).Index(2);
            Map(m => m.SuperRate).ConvertUsing(row => decimal.Parse(row.GetField<string>(3).TrimEnd(new char[] { '%', ' ' })) / 100M);
            Map(m => m.PaymentStartDate).Index(4);
        }
    }
}