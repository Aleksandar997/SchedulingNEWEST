using CompanyManagement.Models;

namespace Scheduling.Controller.Views
{
    public class OrganizationUnitView
    {
        public int OrganizationUnitId { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public bool BindScheduleToEmployee { get; set; }
    }
}