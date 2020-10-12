using System.ComponentModel.DataAnnotations;

namespace Scheduling.Controller.Requests
{
    public class ScheduleTypeRequest
    {
        public int ScheduleTypeId { get; set; }
        [Required(ErrorMessage = "name_required")]
        public string Name { get; set; }
        public bool Selected { get; set; }
    }
}
