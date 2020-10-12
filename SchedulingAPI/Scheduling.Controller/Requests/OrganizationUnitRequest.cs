using CodebookManagement.Models;
using CompanyManagement.Models;
using System.ComponentModel.DataAnnotations;
using Web.Attributes;

namespace Scheduling.Controller.Requests
{
    public class OrganizationUnitRequest
    {
        [ChildValidation]
        public int OrganizationUnitId { get; set; }
        public Company Company { get; set; }
        [Required(ErrorMessage = "code_required")]
        public string Code { get; set; }
        [Required(ErrorMessage = "name_required")]
        public string Name { get; set; }
        public bool Active { get; set; }
        public bool BindScheduleToEmployee { get; set; }
    }
}