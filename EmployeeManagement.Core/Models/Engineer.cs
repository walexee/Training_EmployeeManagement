using System;

namespace EmployeeManagement.Core.Models
{
    public class Engineer : Employee
    {
        public Engineer()
            : base(10)
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
                var minJobCodeRange = 3000; //TODO: move into config
                var maxJobCodeRange = 4000; //TODO: move into config

                if (jobTitleCode < minJobCodeRange || jobTitleCode >= maxJobCodeRange)
                    throw new InvalidOperationException("Invalid Job title");

                base.JobTitle = value;
            }
        }
    }
}
