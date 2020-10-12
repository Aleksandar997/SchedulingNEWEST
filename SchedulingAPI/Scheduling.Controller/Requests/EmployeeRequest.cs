using Common.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Scheduling.Controller.Requests
{
    public class EmployeeRequest
    {
        public int EmployeeId { get; set; }
        [ConditionalRequired("IsEmployee;true", "identificationNumber_required")]
        public string IdentificationNumber { get; set; }
        [Required(ErrorMessage = "active_required")]
        public bool Active { get; set; }
        [ConditionalRequired("IsEmployee;true", "organization_units_required")]
        public IEnumerable<int> OrganizationUnits { get; set; } 
        [ConditionalRequired("IsEmployee;true", "products_required")]
        public IEnumerable<long> Products { get; set; }
        public bool IsEmployee { get; set; }
        public EmployeeRequest()
        {
            OrganizationUnits = new List<int>();
            Products = new List<long>();
        }
    }
}
