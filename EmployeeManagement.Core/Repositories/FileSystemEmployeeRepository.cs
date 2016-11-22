using System;
using System.Collections.Generic;
using System.Linq;
using EmployeeManagement.Core.Models;
using Newtonsoft.Json;
using static Newtonsoft.Json.JsonConvert;
using System.IO;
using System.Configuration;
using System.Threading;

namespace EmployeeManagement.Core.Repositories
{
    public class FileSystemEmployeeRepository : IEmployeeRepository
    {
        private readonly string FILE_PATH = ConfigurationManager.AppSettings["EmployeeFilePath"];
        private List<EmployeeEntity> _employees;
        private static ReaderWriterLockSlim _readerWriterLock = new ReaderWriterLockSlim();

        /// <summary>
        /// Gets the list of all the employees 
        /// </summary>
        /// <returns>
        /// List of employees
        /// </returns>
        public List<EmployeeEntity> GetAllEmployees()
        {
            if (_employees != null)
                return _employees;

            _readerWriterLock.EnterReadLock();

            var employeesJson = default(string);

            try
            {
                employeesJson = File.ReadAllText(FILE_PATH);
            }
            finally
            {
                _readerWriterLock.ExitReadLock();
            }

            _employees = DeserializeObject<List<EmployeeEntity>>(employeesJson) 
                            ?? new List<EmployeeEntity>();

            return _employees;
        }

        public EmployeeEntity GetEmployee(int id)
        {
            var employees = GetAllEmployees();
            var employee = employees.FirstOrDefault(x => x.Id == id);

            return employee;
        }

        public void Create(EmployeeEntity employeeEntity)
        {
            var employees = GetAllEmployees();

            employeeEntity.Id = employees.Max(x => x.Id) + 1;
            employees.Add(employeeEntity);
            PersistEmployees();
        }

        public void Update(EmployeeEntity employeeEntity)
        {
            var employees = GetAllEmployees();
            var employee = employees.FirstOrDefault(x => x.Id == employeeEntity.Id);

            if (employee == null)
                throw new InvalidOperationException("Unknown employee");

            employee.Firstname = employeeEntity.Firstname;
            employee.Lastname = employeeEntity.Lastname;
            employee.JobTitle = employeeEntity.JobTitle;
            employee.Salary = employeeEntity.Salary;
            employee.SkillLevel = employeeEntity.SkillLevel;
            employee.Gender = employeeEntity.Gender;

            PersistEmployees();
        }

        private void PersistEmployees()
        {
            List<EmployeeEntity> employees = GetAllEmployees();
            var employeesJson = SerializeObject(employees, Formatting.Indented);

            _readerWriterLock.EnterWriteLock();

            try
            {
                File.WriteAllText(FILE_PATH, employeesJson);
            }
            finally
            {
                _readerWriterLock.ExitWriteLock();
            }
        }
    }
}
