using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using EmployeeManagement.Core.Services;
using EmployeeManagement.Core.Models;
using EmployeeManagement.Tests.Helpers;
using EmploymentManagement.Web.Controllers;
using static EmployeeManagement.Tests.Constants;
using System;

namespace EmployeeManagement.Tests.Web
{
    [TestClass]
    public class EmployeeControllerTests
    {
        private Mock<IEmployeeService> _employeeServiceMock;

        [TestInitialize]
        public void BeforeEach()
        {
            _employeeServiceMock = new Mock<IEmployeeService>();

            _employeeServiceMock
                .Setup(x => x.GetAllEmployees())
                .Returns(Data.Employees);

            _employeeServiceMock
                .Setup(x => x.GetEmployee(It.IsAny<Guid>()))
                .Returns((Guid input) => {
                    return Data.Employees.Find(x => x.Id == input);
                });
        }

        [TestMethod, TestCategory(UnitTest)]
        public void Get_Returns_All_Employees()
        {
            var employeeController = new EmployeeController(_employeeServiceMock.Object);
            var result = employeeController.Get();

            Assert.AreEqual(2, result.Count());
        }

        [TestMethod, TestCategory(UnitTest)]
        public void Get_With_Parameter_Returns_Employee()
        {
            var employeeController = new EmployeeController(_employeeServiceMock.Object);
            var result = employeeController.Get(Data.User1Id);

            Assert.IsInstanceOfType(result, typeof(Engineer));
        }
    }
}
