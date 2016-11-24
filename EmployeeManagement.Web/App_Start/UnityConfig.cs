using EmployeeManagement.Core.Data;
using EmployeeManagement.Core.Data.Db;
using EmployeeManagement.Core.Data.FileSystem;
using EmployeeManagement.Core.Services;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace EmploymentManagement.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            container.RegisterType<IEmployeeService, EmployeeService>();
            container.RegisterType<IEmployeeRepository, EmployeeRepository>(new InjectionConstructor());
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}