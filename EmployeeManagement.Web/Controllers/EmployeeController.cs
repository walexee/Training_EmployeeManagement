using EmployeeManagement.Core.Models;
using EmployeeManagement.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmploymentManagement.Web.Controllers
{
    [Authorize]
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public IEnumerable<Employee> Get()
        {
            return _employeeService.GetAllEmployees();
        }

        public Employee Get(Guid id)
        {
            return _employeeService.GetEmployee(id);
        }

        public void Post([FromBody]EmployeeEntity employee)
        {
            _employeeService.Save(employee);
        }

        public void Put(Guid id, [FromBody]EmployeeEntity value)
        {
        }

        public void Delete(Guid id)
        {
        }
    }
}
