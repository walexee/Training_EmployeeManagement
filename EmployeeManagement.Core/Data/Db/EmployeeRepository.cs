using EmployeeManagement.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.Core.Models;
using System.Data.Entity;

namespace EmployeeManagement.Core.Data.Db
{
    public class EmployeeRepository : IEmployeeRepository, IDisposable
    {
        private readonly EmployeeManagementContext _context;
        private readonly bool _externalContext;

        public EmployeeRepository()
        {
            _context = new EmployeeManagementContext();
        }

        public EmployeeRepository(EmployeeManagementContext context)
        {
            _context = context;
            _externalContext = true;
        }

        public void Create(EmployeeEntity employeeEntity)
        {
            _context.Employees.Add(employeeEntity);

            _context.SaveChanges();
        }

        public List<EmployeeEntity> GetAllEmployees()
        {
            return _context.Employees.ToList();
        }

        public EmployeeEntity GetEmployee(Guid id)
        {
            return _context.Employees.Find(id);
        }

        public void Update(EmployeeEntity employeeEntity)
        {
            if(!_context.Employees.Local.Contains(employeeEntity))
            {
                _context.Employees.Attach(employeeEntity);
                _context.Entry(employeeEntity).State = EntityState.Modified;
            }

            _context.SaveChanges();
        }

        public void Dispose()
        {
            if (_externalContext || _context == null)
                return;

            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
