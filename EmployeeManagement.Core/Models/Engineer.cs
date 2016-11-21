using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Models
{
    public class Engineer : Employee
    {
        public override int VacationHoursPerMonth { get; protected set; }

        public Engineer()
        {
            VacationHoursPerMonth = 10;
        }
    }
}
