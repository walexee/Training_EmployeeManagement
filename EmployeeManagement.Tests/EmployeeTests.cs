using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeManagement.Core.Models;

namespace EmployeeManagement.Tests
{
    [TestClass]
    public class EmployeeTests
    {
        [TestMethod]
        public void Should_Be_Able_To_Create_Engineer_Instance()
        {
            var engineer = new Engineer();
        }

        [TestMethod]
        public void Should_Be_Able_To_Create_Accountant_Instance()
        {
            var account = new Accountant();
        }

        [TestMethod]
        public void Should_Return_10_As_VacationHourPerMonth_For_Engineers()
        {
            var engineer = new Engineer();

            Assert.AreEqual(10, engineer.VacationHoursPerMonth);
        }

        [TestMethod]
        public void Should_Return_8_As_VacationHourPerMonth_For_Accountants()
        {
            var accountant = new Accountant();

            Assert.AreEqual(8, accountant.VacationHoursPerMonth);
        }
    }
}
