namespace Scheduling.SelectList.Models
{
    public class Employee : ISelectListModel
    {
        public long Id => EmployeeId;
        public string Name { get => FirstName + " " + LastName; set { return; } }
        public int OrganizationUnitId { get; set; }
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Code { get; set; }

    }
}
