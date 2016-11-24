using EmployeeManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Data.Db.Mappings
{
    public class TimeOffMap: EntityTypeConfiguration<TimeOff>
    {
        public TimeOffMap()
        {
            ToTable("TimeOffs");
            HasKey(x => x.Id);

            Property(x => x.EmployeeId).HasColumnName("EmployeeId");
        }
    }
}
