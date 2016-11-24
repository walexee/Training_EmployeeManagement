using EmployeeManagement.Core.Exceptions;
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
            TimeOffs = new List<TimeOff>();
        }

        public int VacationHoursPerMonth
        {
            get
            {
                return _vacationHoursPerMonth;
            }
        }

        public string Firstname { get; set; }

        public Guid Id { get; set; }

        public string Lastname { get; set; }

        public virtual JobTitle JobTitle { get; set; }

        public int SkillLevel { get; set; }

        public double Salary { get; set; }

        public Gender Gender { get; set; }

        public List<TimeOff> TimeOffs { get; set; }

        public void TakeVacation(int hours, DateTime date)
        {
            if (date.Year > DateTime.Now.Year)
                throw new TimeOffException("Can only reserve time off for the current calendar year.");

            if (!CanTakeTimeOff(hours, date))
                throw new TimeOffException();

            var timeOff = new TimeOff(date, hours);
            TimeOffs.Add(timeOff);
        }

        private bool CanTakeTimeOff(int hours, DateTime date)
        {
            var hoursTakenSoFar = GetHoursTakenSoFar(date);
            var hoursAccumulatedSoFar = GetHourAccumulatedSoFar(date);
            var canTakeTimeOff = hoursTakenSoFar + hours < hoursAccumulatedSoFar;

            return canTakeTimeOff;
        }

        private int GetHourAccumulatedSoFar(DateTime date)
        {
            var monthsSoFar = DateTime.Now.Month;

            return monthsSoFar * VacationHoursPerMonth;
        }

        private int GetHoursTakenSoFar(DateTime date)
        {
            var thisYear = DateTime.Now.Year;
            var hoursTaken =
                    TimeOffs
                        .Where(x => x.Date.Year == thisYear)
                        .Select(x => x.HoursTaken)
                        .Sum();

            return hoursTaken;
        }
    }
}
