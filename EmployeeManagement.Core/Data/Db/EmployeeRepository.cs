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
            var dbEntity = GetEmployee(employeeEntity.Id);

            dbEntity.Firstname = employeeEntity.Firstname;
            dbEntity.Lastname = employeeEntity.Lastname;
            dbEntity.JobTitle = employeeEntity.JobTitle;
            dbEntity.Gender = employeeEntity.Gender;
            dbEntity.Salary = employeeEntity.Salary;
            dbEntity.SkillLevel = employeeEntity.SkillLevel;

            foreach(var timeoff in employeeEntity.TimeOffs)
            {
                if(timeoff.Id == 0)
                {
                    dbEntity.TimeOffs.Add(timeoff);
                    continue;
                }

                var dbTimeoff = dbEntity.TimeOffs.FirstOrDefault(x => x.Id == timeoff.Id);

                if(dbTimeoff != null)
                {
                    dbTimeoff.HoursTaken = timeoff.HoursTaken;
                    dbTimeoff.Date = timeoff.Date;
                }
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
