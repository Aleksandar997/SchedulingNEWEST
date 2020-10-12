using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling.SelectList.Models
{
    public class OrganizationUnit : ISelectListModel
    {
        public long Id => OrganizationUnitId;
        public int OrganizationUnitId { get; set; }
        public string Name { get; set; }
        public bool BindScheduleToEmployee { get; set; }
        public string Code { get; set; }
    }
}
