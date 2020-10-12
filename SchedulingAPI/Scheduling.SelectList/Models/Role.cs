namespace Scheduling.SelectList.Models
{
    public class Role : ISelectListModel
    {
        public long Id => RoleId;
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
