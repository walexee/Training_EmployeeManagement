using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.Core.Models;
using Newtonsoft.Json;
using System.IO;

namespace EmployeeManagement.Core.Repositories
{
    public class FileSystemEmployeeRepository : IEmployeeRepository
    {
        private const string FILE_PATH = @"C:\EmployeeStore\employees.json";
        private List<EmployeeEntity> _employees;

        public List<EmployeeEntity> GetAllEmployees()
        {
            return GetEmployees();
        }

        public EmployeeEntity GetEmployee(int id)
        {
            var employees = GetEmployees();
            var employee = employees.FirstOrDefault(x => x.Id == id);

            return employee;
        }

        public void Save(EmployeeEntity employeeEntity)
        {
            var employees = GetEmployees();

            employees.Add(employeeEntity);

            var employeesJson = JsonConvert.SerializeObject(employees);

            File.WriteAllText(FILE_PATH, employeesJson);
        }

        private List<EmployeeEntity> GetEmployees()
        {
            if (_employees != null)
                return _employees;

            var employeesJson = File.ReadAllText(FILE_PATH);

            _employees = JsonConvert.DeserializeObject<List<EmployeeEntity>>(employeesJson);

            if (_employees == null)
                _employees = new List<EmployeeEntity>();

            return _employees;
        }
    }
}
