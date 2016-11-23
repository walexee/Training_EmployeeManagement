using EmployeeManagement.Core.Models;
using System.Collections.Generic;

namespace EmployeeManagement.Tests.Helpers
{
    public class Data
    {
        public static List<EmployeeEntity> EmployeeEntities
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

        public static List<Employee> Employees
        {
            get
            {
                return new List<Employee>
                {
                    new Engineer { Id = 1, Firstname = "James", Lastname = "Don", JobTitle = JobTitle.ProgrammerI, Salary = 50000, SkillLevel = 2 },
                    new Accountant { Id = 2, Firstname = "Lola", Lastname = "Igwe", JobTitle = JobTitle.TaxAccountant, Salary = 52000, SkillLevel = 4 }
                };
            }
        }
    }
}
