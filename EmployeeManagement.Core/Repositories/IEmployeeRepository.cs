using EmployeeManagement.Core.Models;
using System.Collections.Generic;

namespace EmployeeManagement.Core.Repositories
{
    public interface IEmployeeRepository
    {
        EmployeeEntity GetEmployee(int id);

        List<EmployeeEntity> GetAllEmployees();

        void Create(EmployeeEntity employeeEntity);

        void Update(EmployeeEntity employeeEntity);
    }
}