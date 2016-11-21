using EmployeeManagement.Core.Models;
using EmployeeManagement.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.Core.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Employee GetEmployee(int id)
        {
            var employeeEntity = _employeeRepository.GetEmployee(id);
            var employee = ToEmployee(employeeEntity);
            
            return employee;
        }

        public List<Employee> GetAllEmployees()
        {
            var employeeEntities = _employeeRepository.GetAllEmployees();

            return employeeEntities.Select(x => ToEmployee(x)).ToList();
        }

        private Employee ToEmployee(EmployeeEntity employeeEntity)
        {
            var employee = default(Employee);

            switch(employeeEntity.JobTitle)
            {
                case "Software Developer":
                case "Programmer I":
                case "Programmer II":
                    employee = new Engineer();
                    break;

                case "Jr. CPA":
                case "Sr. CPA":
                case "Tax Accountant":
                    employee = new Accountant();
                    break;

                default:
                    throw new InvalidOperationException("Invalid job type");
            }

            employee.Id = employeeEntity.Id;
            employee.Firstname = employeeEntity.Firstname;
            employee.Lastname = employeeEntity.Lastname;
            employee.JobTitle = employeeEntity.JobTitle;
            employee.Salary = employeeEntity.Salary;
            employee.SkillLevel = employeeEntity.SkillLevel;

            return employee;
        }
    }
}
