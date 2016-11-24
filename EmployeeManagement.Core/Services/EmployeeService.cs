using EmployeeManagement.Core.Models;
using EmployeeManagement.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using static EmployeeManagement.Core.Models.JobTitle;

namespace EmployeeManagement.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Employee GetEmployee(Guid id)
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
                case SoftwareDeveloper:
                case ProgrammerI:
                case ProgrammerII:
                    employee = new Engineer();
                    break;

                case JuniorCpa:
                case SeniorCpa:
                case TaxAccountant:
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
            employee.Gender = employeeEntity.Gender;

            return employee;
        }
    }
}
