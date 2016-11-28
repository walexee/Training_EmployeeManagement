using System.Collections.Generic;
using EmployeeManagement.Core.Models;
using System;

namespace EmployeeManagement.Core.Services
{
    public interface IEmployeeService
    {
        List<Employee> GetAllEmployees();
        Employee GetEmployee(Guid id);
        void TakeTimeOff(TimeOff timeOff);
        void Save(EmployeeEntity employee);
    }
}