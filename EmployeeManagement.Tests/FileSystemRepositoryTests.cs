using EmployeeManagement.Core.Models;
using EmployeeManagement.Core.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Tests
{
    [TestClass]
    public class FileSystemRepositoryTests
    {
        [TestMethod]
        public void Should_Be_Able_To_Create_FileSystemRepository_Instance()
        {
            var repo = new FileSystemEmployeeRepository();

            Assert.IsNotNull(repo);
        }

        [TestMethod]
        public void Should_Be_Able_To_Save_Employee()
        {
            var repo = new FileSystemEmployeeRepository();
            var employee = new EmployeeEntity();

            employee.Id = 78;
            employee.Firstname = "Ngozi";
            employee.Lastname = "Adekola";
            employee.Salary = 100000;
            employee.SkillLevel = 5;
            employee.JobTitle = "Software Developer";

            repo.Save(employee);
        }
    }
}
