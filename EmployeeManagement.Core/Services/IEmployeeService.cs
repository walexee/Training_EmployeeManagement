using System.Collections.Generic;
using EmployeeManagement.Core.Models;

namespace EmployeeManagement.Core.Services
{
    public interface IEmployeeService
    {
        List<Employee> GetAllEmployees();
        Employee GetEmployee(int id);
    }
}