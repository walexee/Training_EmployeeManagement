using System;

namespace EmployeeManagement.Core.Models
{
    public class TimeOff
    {
        private DateTime _date;

        public TimeOff() { }

        public TimeOff(DateTime date, int hoursTaken)
        {
            Date = date;
            HoursTaken = hoursTaken;
        }

        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value.Date;
            }
        }
        public int HoursTaken { get; set; }
    }
}
