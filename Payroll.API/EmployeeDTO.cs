﻿

namespace Payroll.API
{
    public class EmployeeDTO
    {
     
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int AnnualSalary { get; set; }

        public decimal SuperRate { get; set; }

        public string PaymentStartDate { get; set; }
    }
}
