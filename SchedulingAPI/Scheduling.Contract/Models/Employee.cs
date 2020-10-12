using System.Collections.Generic;

namespace Scheduling.Contract.Models
{
    public class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? EmployeeId { get; set; }
        public string IdentificationNumber { get; set; }
        public bool Active { get; set; }
        public IEnumerable<int> OrganizationUnits { get; set; } 
        public IEnumerable<long> Products { get; set; }

        public Employee()
        {
            OrganizationUnits = new List<int>();
            Products = new List<long>();
        }
    }
}
