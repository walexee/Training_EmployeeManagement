using EmployeeManagement.Core.Repositories;
using EmployeeManagement.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmploymentManagement.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public HomeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public ActionResult Index()
        {
            var employee = _employeeService.GetAllEmployees().First();

            ViewBag.Title = "Home Page";

            return View(employee);
        }
    }
}
