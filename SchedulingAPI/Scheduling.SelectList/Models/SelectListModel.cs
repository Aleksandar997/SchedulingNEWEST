namespace Scheduling.SelectList.Models
{
    public interface ISelectListModel
    {
        public long Id { get; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
