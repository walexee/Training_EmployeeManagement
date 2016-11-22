using EmployeeManagement.Core.Models;
using EmployeeManagement.Core.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using static EmployeeManagement.Tests.Constants;

namespace EmployeeManagement.Tests
{
    [TestClass]
    public class FileSystemRepositoryTests
    {
        private readonly static string _filepath = ConfigurationManager.AppSettings["EmployeeFilePath"];

        [TestInitialize]
        public void InitTest()
        {
            var employees = new List<EmployeeEntity>
            {
                new EmployeeEntity { Id = 1, Firstname = "James", Lastname = "Don", JobTitle = "Programmer I", Salary = 50000, SkillLevel = 2 },
                new EmployeeEntity { Id = 2, Firstname = "Lola", Lastname = "Igwe", JobTitle = "Tax Accountant", Salary = 52000, SkillLevel = 4 }
            };

            File.WriteAllText(_filepath, JsonConvert.SerializeObject(employees, Formatting.Indented));
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            File.WriteAllText(_filepath, string.Empty);
        }

        [TestMethod, TestCategory(IntegrationTest)]
        public void Should_Be_Able_To_Create_FileSystemRepository_Instance()
        {
            var repo = new FileSystemEmployeeRepository();

            Assert.IsNotNull(repo);
        }

        [TestMethod, TestCategory(IntegrationTest)]
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

            repo.Create(employee);
        }

        [TestMethod, TestCategory(IntegrationTest)]
        public void Should_Be_Able_To_Read_All_Employees()
        {
            var repo = new FileSystemEmployeeRepository();
            var employees = repo.GetAllEmployees();

            Assert.AreEqual(2, employees.Count);
            Assert.AreEqual(1, employees.First().Id);
            Assert.AreEqual(2, employees[1].Id);
        }
    }
}
