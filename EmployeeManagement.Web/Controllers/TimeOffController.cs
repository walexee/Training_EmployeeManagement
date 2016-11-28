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
    public class TimeOffController : ApiController
    {
        private readonly IEmployeeService _employeeService;

        public TimeOffController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: api/TimeOff
        public IEnumerable<TimeOff> Get()
        {
            return _employeeService.GetAllEmployees().SelectMany(x => x.TimeOffs);
        }

        // GET: api/TimeOff/5
        public TimeOff Get(int id)
        {
            throw new NotImplementedException();
        }

        // POST: api/TimeOff
        public void Post([FromBody]TimeOff timeOff)
        {
            if (!ModelState.IsValid)
                return;

            _employeeService.TakeTimeOff(timeOff);
        }

        // PUT: api/TimeOff/5
        public void Put(int id, [FromBody]string value)
        {
            throw new NotImplementedException();
        }
    }
}
