namespace Payroll.Service
{
    public interface ITaxRateService
    {
        
     /// <summary>
     /// Calculate TaxRate
     /// </summary>
     /// <param name="annualSalary"></param>
     /// <returns></returns>     
      int CalculateTaxRate(int annualSalary) ;
    }
}
