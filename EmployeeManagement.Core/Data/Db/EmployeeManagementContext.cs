using EmployeeManagement.Core.Data.Db.Mappings;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Data.Db
{
    public class EmployeeManagementContext : DbContext
    {
        static EmployeeManagementContext()
        {
            Database.SetInitializer<EmployeeManagementContext>(null);
        }

        public EmployeeManagementContext()
            :base("name=DefaultConnection")
        {

        }

        public DbSet<Models.EmployeeEntity> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EmployeeEntityMap());
        }
    }
}
