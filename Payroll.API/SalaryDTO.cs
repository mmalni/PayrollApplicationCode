using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.API
{
    public  class SalaryDTO
    {
   
            public string EmployeeName { get; set; }

            public string PayPeriod { get; set; }

            public int GrossIncome { get; set; }

            public int IncomeTax { get; set; }

            public int NetIncome { get; set; }

            public int Super { get; set; }
        }
    }



