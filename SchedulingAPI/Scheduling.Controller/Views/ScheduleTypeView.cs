namespace Scheduling.Controller.Views
{
    public class ScheduleTypeView
    {
        public int ScheduleTypeId { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public bool Selected { get; set; }
        public UserView User { get; set; }

        public int Id { get; set; }
    }
}
