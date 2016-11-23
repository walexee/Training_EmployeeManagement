using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeManagement.Core.Services;
using EmployeeManagement.Core.Repositories;
using Moq;
using EmployeeManagement.Core.Models;
using System.Collections.Generic;
using System.Linq;
using EmployeeManagement.Tests.Helpers;

namespace EmployeeManagement.Tests
{
    [TestClass]
    public class EmployeeServiceTests
    {
        private Mock<IEmployeeRepository> _employeeRepoMock;

        [TestInitialize]
        public void BeforeEach()
        {
            var employees = Data.EmployeeEntities;

            _employeeRepoMock = new Mock<IEmployeeRepository>();
            _employeeRepoMock.Setup(x => x.GetAllEmployees()).Returns(employees);
            _employeeRepoMock
                .Setup(x => x.GetEmployee(It.IsAny<int>()))
                .Returns((int input) => {
                return employees.FirstOrDefault(y => y.Id == input);
            });
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void Should_Be_Able_To_Create_EmployeeService_Instance()
        {
            var employeeService = new EmployeeService(_employeeRepoMock.Object);

            Assert.IsNotNull(employeeService);
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void Should_Be_Able_To_Retrieve_An_Employee()
        {
            var employeeService = new EmployeeService(_employeeRepoMock.Object);
            var employee = employeeService.GetEmployee(2);

            Assert.IsNotNull(employee);
            Assert.AreEqual("Lola", employee.Firstname);
        }
    }
}
