using CodebookManagement.Models;
using CompanyManagement.Models;

namespace Scheduling.Contract.Models
{
    public class OrganizationUnit
    {
        public int OrganizationUnitId { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public bool BindScheduleToEmployee { get; set; }
    }
    public class OrganizationUnitPaging : CodebookPaging
    {
        public long OrganizationUnitId { get; set; }
    }
}