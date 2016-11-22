using System;

namespace EmployeeManagement.Core.Models
{
    public class TimeOff
    { 
        public TimeOff() { }

        public TimeOff(DateTime date, int hoursTaken)
        {
            Date = date;
            HoursTaken = hoursTaken;
        }

        public DateTime Date { get; set; }
        public int HoursTaken { get; set; }
    }
}
