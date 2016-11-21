using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Models
{
    public abstract class Employee
    {
        private string _jobTitle;

        public abstract int VacationHoursPerMonth { get; protected set; }

        public string Firstname { get; set; }

        public int Id { get; set; }

        public string Lastname { get; set; }

        public string JobTitle
        {
            get
            {
                return _jobTitle;
            }
            set
            {
                _jobTitle = value;
            }
        }

        public int SkillLevel { get; set; }

        public double Salary { get; set; }

        public void TakeVacation(int hours)
        {
            //
        }
    }
}
