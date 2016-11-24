using EmployeeManagement.Core.Models;
using System;
using System.Collections.Generic;

namespace EmployeeManagement.Tests.Helpers
{
    public class Data
    {
        public static Guid User1Id
        {
            get
            {
                return new Guid("ab0db2b1-2b62-4aef-8781-454fe93e7f85");
            }
        }

        public static Guid User2Id
        {
            get
            {
                return new Guid("a40d9803-b4fc-4ddc-9fbb-6dd99c41760f");
            }
        }

        public static List<EmployeeEntity> EmployeeEntities
        {
            get
            {
                return new List<EmployeeEntity>
                {
                    new EmployeeEntity { Id = User1Id, Firstname = "James", Lastname = "Don", JobTitle = JobTitle.ProgrammerI, Salary = 50000, SkillLevel = 2 },
                    new EmployeeEntity { Id = User2Id, Firstname = "Lola", Lastname = "Igwe", JobTitle = JobTitle.TaxAccountant, Salary = 52000, SkillLevel = 4 }
                };
            }
        }

        public static List<Employee> Employees
        {
            get
            {
                return new List<Employee>
                {
                    new Engineer { Id = User1Id, Firstname = "James", Lastname = "Don", JobTitle = JobTitle.ProgrammerI, Salary = 50000, SkillLevel = 2 },
                    new Accountant { Id = User2Id, Firstname = "Lola", Lastname = "Igwe", JobTitle = JobTitle.TaxAccountant, Salary = 52000, SkillLevel = 4 }
                };
            }
        }
    }
}
