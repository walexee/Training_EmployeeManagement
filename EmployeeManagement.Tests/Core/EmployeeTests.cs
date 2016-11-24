using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeManagement.Core.Models;
using static EmployeeManagement.Tests.Constants;
using EmployeeManagement.Core.Exceptions;

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

        [TestMethod, TestCategory(UnitTest)]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Accountant_Should_Not_Accept_Invalid_JobTitles()
        {
            var accountant = new Accountant();

            accountant.JobTitle = JobTitle.ProgrammerI;
        }

        [TestMethod, TestCategory(UnitTest)]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Engineer_Should_Not_Accept_Invalid_JobTitles()
        {
            var engineer = new Engineer();

            engineer.JobTitle = JobTitle.SeniorCpa;
        }

        [TestMethod, TestCategory(UnitTest)]
        public void Should_Allow_Vacation_When_Available()
        {
            var engineer = new Engineer();
            var date = DateTime.Now.AddMonths(1);

            engineer.Id = Guid.NewGuid();
            engineer.JobTitle = JobTitle.ProgrammerI;
            engineer.Firstname = "Taylor";

            engineer.TakeVacation(8, date);

            Assert.AreEqual(1, engineer.TimeOffs.Count);
            Assert.AreEqual(8, engineer.TimeOffs.First().HoursTaken);
            Assert.AreEqual(date.Date, engineer.TimeOffs.First().Date);
        }

        [TestMethod, TestCategory(UnitTest)]
        [ExpectedException(typeof(TimeOffException))]
        public void Should_Not_Allow_Vacation_When_Not_Available()
        {
            var engineer = new Engineer();
            var date = DateTime.Now.AddMonths(1);

            engineer.Id = Guid.NewGuid();
            engineer.JobTitle = JobTitle.ProgrammerI;
            engineer.Firstname = "Taylor";

            engineer.TimeOffs.AddRange(new TimeOff[] {
                new TimeOff(DateTime.Now.AddMonths(-9), 20),
                new TimeOff(DateTime.Now.AddMonths(-7), 20),
                new TimeOff(DateTime.Now.AddMonths(-5), 20),
                new TimeOff(DateTime.Now.AddMonths(-3), 20),
                new TimeOff(DateTime.Now.AddMonths(-1), 20)
            });

            engineer.TakeVacation(10, date);

            //Assert.AreEqual(1, engineer.TimeOffs.Count);
            //Assert.AreEqual(8, engineer.TimeOffs.First().HoursTaken);
            //Assert.AreEqual(date.Date, engineer.TimeOffs.First().Date);
        }
    }
}
