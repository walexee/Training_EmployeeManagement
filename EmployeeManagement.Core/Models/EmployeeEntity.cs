using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Models
{
    public class EmployeeEntity
    {
        public string Firstname { get; set; }

        public int Id { get; set; }

        public string Lastname { get; set; }

        public JobTitle JobTitle { get; set; }

        public int SkillLevel { get; set; }

        public double Salary { get; set; }

        public Gender Gender { get; set; }
    }
}
