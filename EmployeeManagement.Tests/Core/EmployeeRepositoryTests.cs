using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeManagement.Core.Data.Db;
using EmployeeManagement.Tests.Helpers;
using EmployeeManagement.Core.Models;

namespace EmployeeManagement.Tests.Core
{
    [TestClass]
    public class EmployeeRepositoryTests
    {
        [TestMethod, TestCategory(Constants.IntegrationTest)]
        public void Should_Be_Able_To_Get_Add_Employee_To_DB()
        {
            using (var context = new EmployeeManagementContext())
            using (var repo = new EmployeeRepository(context))
            using (var txn = context.Database.BeginTransaction())
            {
                var employee = Data.EmployeeEntities[0];

                repo.Create(employee);

                var dbEmployee = context.Set<EmployeeEntity>().Find(Data.User1Id);

                txn.Rollback();

                Assert.IsNotNull(dbEmployee);
            }
        }

        [TestMethod, TestCategory(Constants.IntegrationTest)]
        public void Should_Be_Able_To_Retrieve_Employee_From_DB()
        {
            using (var context = new EmployeeManagementContext())
            using (var repo = new EmployeeRepository(context))
            using (var txn = context.Database.BeginTransaction())
            {
                context.Set<EmployeeEntity>().AddRange(Data.EmployeeEntities);
                context.SaveChanges();

                var employee = repo.GetEmployee(Data.User1Id);

                txn.Rollback();

                Assert.IsNotNull(employee);
            }
        }

        [TestMethod, TestCategory(Constants.IntegrationTest)]
        public void Should_Be_Able_To_Retrieve_All_Employees_From_DB()
        {
            using (var context = new EmployeeManagementContext())
            using (var repo = new EmployeeRepository(context))
            using (var txn = context.Database.BeginTransaction())
            {
                context.Set<EmployeeEntity>().AddRange(Data.EmployeeEntities);
                context.SaveChanges();

                var employees = repo.GetAllEmployees();

                txn.Rollback();

                Assert.AreEqual(2, employees.Count);
            }
        }
    }
}
