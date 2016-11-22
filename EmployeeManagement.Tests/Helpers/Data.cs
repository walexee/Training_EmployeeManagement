using EmployeeManagement.Core.Models;
using System.Collections.Generic;

namespace EmployeeManagement.Tests.Helpers
{
    public class Data
    {
        public static List<EmployeeEntity> Employees
        {
            get
            {
                return new List<EmployeeEntity>
                {
                    new EmployeeEntity { Id = 1, Firstname = "James", Lastname = "Don", JobTitle = JobTitle.ProgrammerI, Salary = 50000, SkillLevel = 2 },
                    new EmployeeEntity { Id = 2, Firstname = "Lola", Lastname = "Igwe", JobTitle = JobTitle.TaxAccountant, Salary = 52000, SkillLevel = 4 }
                };
            }
        }
    }
}
