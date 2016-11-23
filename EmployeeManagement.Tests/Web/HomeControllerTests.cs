using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using EmployeeManagement.Core.Services;
using EmployeeManagement.Core.Models;
using EmployeeManagement.Tests.Helpers;
using EmploymentManagement.Web.Controllers;
using System.Web.Mvc;
using static EmployeeManagement.Tests.Constants;

namespace EmployeeManagement.Tests.Web
{
    [TestClass]
    public class HomeControllerTests
    {
        private Mock<IEmployeeService> _employeeServiceMock;

        [TestInitialize]
        public void BeforeEach()
        {
            _employeeServiceMock = new Mock<IEmployeeService>();

            _employeeServiceMock
                .Setup(x => x.GetAllEmployees())
                .Returns(Data.Employees);
        }

        [TestMethod, TestCategory(UnitTest)]
        public void Index_Has_The_Right_Model()
        {
            var homeController = new HomeController(_employeeServiceMock.Object);
            var result = homeController.Index() as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(Employee));
        }

        [TestMethod, TestCategory(UnitTest)]
        public void Index_Has_The_Right_View()
        {
            var homeController = new HomeController(_employeeServiceMock.Object);
            var result = homeController.Index() as ViewResult;

            Assert.AreEqual(string.Empty, result.ViewName);
        }
    }
}
