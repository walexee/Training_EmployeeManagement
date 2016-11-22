using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Models
{
    public abstract class Employee
    {
        private readonly int _vacationHoursPerMonth;

        public Employee(int vacationHoursPerMonth)
        {
            _vacationHoursPerMonth = vacationHoursPerMonth;
        }

        public int VacationHoursPerMonth
        {
            get
            {
                return _vacationHoursPerMonth;
            }
        }

        public string Firstname { get; set; }

        public int Id { get; set; }

        public string Lastname { get; set; }

        public virtual JobTitle JobTitle { get; set; }

        public int SkillLevel { get; set; }

        public double Salary { get; set; }

        public Gender Gender { get; set; }

        public void TakeVacation(int hours)
        {
            //
        }
    }
}
