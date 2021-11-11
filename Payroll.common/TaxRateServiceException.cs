using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Common
{

    public class TaxRateServiceException : Exception
    {
        public TaxRateServiceException(string message) : base(message)
        {
        }
    }
}
