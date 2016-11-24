using System;
using System.Collections.Generic;

namespace EmployeeManagement.Core.Models
{
    public class EmployeeEntity
    {
        public EmployeeEntity()
        {
            TimeOffs = new List<TimeOff>();
        }

        public string Firstname { get; set; }

        public Guid Id { get; set; }

        public string Lastname { get; set; }

        public JobTitle JobTitle { get; set; }

        public int SkillLevel { get; set; }

        public decimal Salary { get; set; }

        public Gender Gender { get; set; }

        public virtual ICollection<TimeOff> TimeOffs { get; set; }
    }
}
