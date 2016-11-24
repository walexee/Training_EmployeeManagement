using EmployeeManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Data.Db.Mappings
{
    public class EmployeeEntityMap: EntityTypeConfiguration<EmployeeEntity>
    {
        public EmployeeEntityMap()
        {
            ToTable("Employees");
            HasKey(x => x.Id);

            Property(x => x.Gender).HasColumnName("GenderId");
            Property(x => x.JobTitle).HasColumnName("JobTitleId");
        }
    }
}
