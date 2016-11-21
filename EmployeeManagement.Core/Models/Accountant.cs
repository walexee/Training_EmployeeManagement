using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Models
{
    public class Accountant : Employee
    {
        public override int VacationHoursPerMonth { get; protected set; }

        public Accountant()
        {
            VacationHoursPerMonth = 8;
        }
    }
}
