using System.Collections.Generic;

namespace Scheduling.Controller.Views
{
    public class EmployeeView
    {
        public int EmployeeId { get; set; }
        public string IdentificationNumber { get; set; }
        public bool Active { get; set; }
        public IEnumerable<int> OrganizationUnits { get; set; } 
        public IEnumerable<long> Products { get; set; }
        public bool IsEmployee { get; set; }

        public EmployeeView()
        {
            OrganizationUnits = new List<int>();
            Products = new List<long>();
        }
    }
}
