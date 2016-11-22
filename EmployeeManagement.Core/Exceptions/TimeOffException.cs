using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Exceptions
{
    public class TimeOffException : Exception
    {
        public TimeOffException()
            :base("Time off not allowed.")
        {

        }

        public TimeOffException(string message)
            :base(message)
        {
        }
    }
}
