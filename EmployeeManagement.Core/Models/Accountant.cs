using System;
using static EmployeeManagement.Core.Models.JobTitle;

namespace EmployeeManagement.Core.Models
{
    public class Accountant : Employee
    {
        public Accountant()
            :base(8)
        {
        }

        public override JobTitle JobTitle
        {
            get
            {
                return base.JobTitle;
            }

            set
            {
                var jobTitleCode = (int)value;
                var minJobCodeRange = 4000; //TODO: move into config
                var maxJobCodeRange = 5000; //TODO: move into config

                if (jobTitleCode < minJobCodeRange || jobTitleCode >= maxJobCodeRange)
                    throw new InvalidOperationException("Invalid Job title");

                base.JobTitle = value;
            }
        }
    }
}
