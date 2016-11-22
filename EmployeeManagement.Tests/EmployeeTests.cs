using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeManagement.Core.Models;
using static EmployeeManagement.Tests.Constants;

namespace EmployeeManagement.Tests
{
    [TestClass]
    public class EmployeeTests
    {
        [TestMethod, TestCategory(UnitTest)]
        public void Should_Be_Able_To_Create_Engineer_Instance()
        {
            var engineer = new Engineer();
        }

        [TestMethod, TestCategory(UnitTest)]
        public void Should_Be_Able_To_Create_Accountant_Instance()
        {
            var account = new Accountant();
        }

        [TestMethod, TestCategory(UnitTest)]
        public void Should_Return_10_As_VacationHourPerMonth_For_Engineers()
        {
            var engineer = new Engineer();

            Assert.AreEqual(10, engineer.VacationHoursPerMonth);
        }

        [TestMethod, TestCategory(UnitTest)]
        public void Should_Return_8_As_VacationHourPerMonth_For_Accountants()
        {
            var accountant = new Accountant();

            Assert.AreEqual(8, accountant.VacationHoursPerMonth);
        }
    }
}
