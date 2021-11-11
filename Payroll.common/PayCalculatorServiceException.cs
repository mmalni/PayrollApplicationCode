using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Common
{
    public class PayCalculatorServiceException : Exception
    {
        public PayCalculatorServiceException(string message) : base(message)
        {
        }
    }
}